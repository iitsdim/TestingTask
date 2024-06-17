namespace Domain.Utils;

public class SequenceUtils
{
    public Dictionary<int, int> digitEncoding = new Dictionary<int, int>
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

    public List<int> PossibleDigits(int observed)
    {
        List<int> possible = digitEncoding
            .Where(pair => (observed & pair.Value) == observed)
            .Select(pair => pair.Key)
            .ToList();

        return possible;
    }



    public List<int> GeneratePossibleNumbers(List<int> numbers, int step)
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

    public List<int> StringsToIntegers(List<string> strings)
    {
        return strings.Select(binaryString => Convert.ToInt32(binaryString, 2)).ToList();
    }
    
    public List<string> IntegersToBinaryStrings(List<int> numbers)
    {
        return numbers.Select(number => Convert.ToString(number, 2).PadLeft(7, '0')).ToList();
    }
}