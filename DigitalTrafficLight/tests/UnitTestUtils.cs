using Domain.Utils;
namespace tests;

public class UnitTestUtils
{
    private readonly SequenceUtils _sequenceUtils = new SequenceUtils();

    [Fact]
    public void PossibleDigits_WithValidObserved_ReturnsCorrectDigits()
    {
        // Arrange
        int observed = 0;
        // var _sequenceUtils = new SequenceUtils();
        // Act
        List<int> result = _sequenceUtils.PossibleDigits(observed);

        // Assert
        var expected = new List<int> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GeneratePossibleNumbers_WithValidNumbersAndStep_ReturnsCorrectValues()
    {
        // Arrange
        var numbers = new List<int> { 119, 29 };
        int step = 0;

        // Act
        List<int> result = _sequenceUtils.GeneratePossibleNumbers(numbers, step);

        // Assert
        var expected = new List<int> { 2, 8, 82, 88 };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GeneratePossibleNumbers_WithInvalidNumbersList_ThrowsException()
    {
        // Arrange
        var numbers = new List<int> { 18 };

        // Act & Assert
        Assert.Throws<Exception>(() => _sequenceUtils.GeneratePossibleNumbers(numbers, 5));
    }

    [Fact]
    public void StringsToIntegers_WithValidBinaryStrings_ReturnsCorrectIntegers()
    {
        // Arrange
        var strings = new List<string> { "10010", "01010", "11011" };

        // Act
        List<int> result = _sequenceUtils.StringsToIntegers(strings);

        // Assert
        var expected = new List<int> { 18, 10, 27 };
        Assert.Equal(expected, result);
    }

    [Fact]
    public void IntegersToBinaryStrings_WithValidIntegers_ReturnsCorrectBinaryStrings()
    {
        // Arrange
        var numbers = new List<int> { 18, 10, 27 };

        // Act
        List<string> result = _sequenceUtils.IntegersToBinaryStrings(numbers);

        // Assert
        var expected = new List<string> { "0010010", "0001010", "0011011" };
        Assert.Equal(expected, result);
    }
}