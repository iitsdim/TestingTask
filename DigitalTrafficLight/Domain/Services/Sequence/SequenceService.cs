using Contracts.Observation;
using Domain.Models;
namespace Domain.Services.Sequence;

public class SequenceService : ISequenceService 
{
    private static readonly Dictionary<Guid, SequenceModel> _sequenceMap = new();

    public ObservationResponse AddObservation(Guid Id, Observation observation)
    {
        var sequence = GetSequence(Id);
        if (sequence.Color.ToLower() == "red") {
            throw new Exception("The red observation should be the last");
        }
        if (sequence.ObservationCount == 0 && observation.Color.ToLower() == "red") {
            throw new Exception("There isn't enough data");
        }
        sequence.Color = observation.Color;
        if (observation.Color.ToLower() == "red") {
            var possibleNumbers = new List<int> { sequence.ObservationCount };
            sequence.Start = possibleNumbers.Intersect(sequence.Start).ToList();
        } else {
            if (observation.Numbers == null) {
                throw new Exception("Invalid numbers list");
            }
            List<int> numbers = StringsToIntegers(observation.Numbers);
            var possibleNumbers = GeneratePossibleNumbers(numbers, sequence.ObservationCount);
            sequence.Start = possibleNumbers.Intersect(sequence.Start).ToList();
            
            int andAllFirstDigits = sequence.Start.Aggregate(127, (acc, c) =>
            {
                int x = c - sequence.ObservationCount;
                int f = x / 10;
                return acc & digitEncoding[f];
            });

            int andAllSecondDigits = sequence.Start.Aggregate(127, (acc, c) =>
            {
                int x = c - sequence.ObservationCount;
                int s = x % 10;
                return acc & digitEncoding[s];
            });

            sequence.Missing[0] |= numbers[0] ^ andAllFirstDigits;
            sequence.Missing[1] |= numbers[1] ^ andAllSecondDigits;
        }

        sequence.ObservationCount += 1;
        ModifySequence(sequence);
        return new ObservationResponse(sequence.Start, IntegersToBinaryStrings(sequence.Missing));
    }

    public void Clear()
    {
        _sequenceMap.Clear();
    }

    public void CreateSequence(SequenceModel sequence) 
    {
        _sequenceMap.Add(sequence.Id, sequence);
    }

    public void ModifySequence(SequenceModel sequence) {
        _sequenceMap[sequence.Id] = sequence;
    }

    public SequenceModel GetSequence(Guid id)
    {
        if (!_sequenceMap.ContainsKey(id))
        {
            throw new KeyNotFoundException("The sequence isn't found");
        }
        return _sequenceMap[id];
    }


    //TODO move all the below to Utils

    static Dictionary<int, int> digitEncoding = new Dictionary<int, int>
    {
        { 0, 119 },
        { 1, 18 },
        { 2, 93 },
        { 3, 91 },
        { 4, 58 },
        { 5, 107 },
        { 6, 111 },
        { 7, 82 },
        { 8, 127 },
        { 9, 123 }
    };

    static List<int> PossibleDigits(int observed)
    {
        List<int> possible = digitEncoding
            .Where(pair => (observed & pair.Value) == observed)
            .Select(pair => pair.Key)
            .ToList();

        return possible;
    }



    static List<int> GeneratePossibleNumbers(List<int> numbers, int step)
    {
        if (numbers.Count != 2)
        {
            throw new Exception("Invalid numbers list. It must contain exactly two strings.");
        }

        var possibleValues = from x in PossibleDigits(numbers[0])
                            from y in PossibleDigits(numbers[1])
                            let value = x * 10 + y + step
                            where value < 100 && value + 1 > 2 * step
                            select value;

        return possibleValues.ToList();
    }

    static List<int> StringsToIntegers(List<string> strings)
    {
        return strings.Select(binaryString => Convert.ToInt32(binaryString, 2)).ToList();
    }
    
    static List<string> IntegersToBinaryStrings(List<int> numbers)
    {
        return numbers.Select(number => Convert.ToString(number, 2).PadLeft(7, '0')).ToList();
    }
}