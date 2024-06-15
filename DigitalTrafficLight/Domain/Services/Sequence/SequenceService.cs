using Contracts.Observation;
using Domain.Models;
namespace Domain.Services.Sequence;

public class SequenceService : ISequenceService 
{
    private static readonly Dictionary<Guid, SequenceModel> _sequenceMap = new();

    public SequenceModel AddObservation(Guid Id, Observation observation)
    {
        var sequence = GetSequence(Id);
        if (sequence.Color.ToLower() == "red") {
            throw new Exception("The red observation should be the last");
        }
        if (sequence.ObservationCount == 0 && observation.Color.ToLower() == "red") {
            throw new Exception("There isn't enough data");
        }
        
        sequence.Color = observation.Color;
        // return sequence;
        if (observation.Color.ToLower() == "red") {
            var list2 = new List<int> { sequence.ObservationCount };
            sequence.Start = list2.Intersect(sequence.Start).ToList();
        } else {
            if (observation.Numbers == null) {
                throw new Exception("Invalid numbers list");
            }
            var list2 = GeneratePossibleNumbers(observation.Numbers, sequence.ObservationCount);
            sequence.Start = list2.Intersect(sequence.Start).ToList();
        }
        sequence.ObservationCount += 1;
        
        ModifySequence(sequence);
        return sequence;
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

    static Dictionary<int, string> digitEncoding = new Dictionary<int, string>
    {
        { 0, "1110111" },
        { 1, "0010010" },
        { 2, "1011101" },
        { 3, "1011011" },
        { 4, "0111010" },
        { 5, "1101011" },
        { 6, "1101111" },
        { 7, "1010010" },
        { 8, "1111111" },
        { 9, "1111011" }
    };

    static List<int> PossibleDigits(string observed)
    {
        List<int> possible = digitEncoding
            .Where(pair => Matches(observed, pair.Value))
            .Select(pair => pair.Key)
            .ToList();

        return possible;
    }

    static bool Matches(string observed, string encoded)
    {
        for (int i = 0; i < observed.Length; i++)
        {
            char o = observed[i];
            char e = encoded[i];

            if (o == '1' && e == '0')
            {
                return false; // If observed has '1' where encoded has '0', it's not a match
            }
        }
        
        return true; // All comparisons passed, it's a match
    }



    static List<int> GeneratePossibleNumbers(List<string> numbers, int step)
    {
        if (numbers == null || numbers.Count != 2)
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


}