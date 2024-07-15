using System;
using System.Linq;

public class StringCalculator
{
    private const int MaxNumber = 1000;

    public int Add(string numbers)
    {
        if (string.IsNullOrEmpty(numbers))
        {
            return 0;
        }

        var delimiters = new List<string> { ",", "\n" };
        numbers = ProcessCustomDelimiter(numbers, delimiters);

        var splitNumbers = SplitNumbers(numbers, delimiters);
        ValidateNumbers(splitNumbers);

        return splitNumbers.Where(n => n <= MaxNumber).Sum();
    }

    private string ProcessCustomDelimiter(string numbers, List<string> delimiters)
    {
        if (numbers.StartsWith("//"))
        {
            var delimiterEndIndex = numbers.IndexOf('\n');
            var customDelimiter = numbers.Substring(2, delimiterEndIndex - 2);
            delimiters.Add(customDelimiter);
            numbers = numbers.Substring(delimiterEndIndex + 1);
        }

        return numbers;
    }

    private int[] SplitNumbers(string numbers, List<string> delimiters)
    {
        return numbers.Split(delimiters.ToArray(), StringSplitOptions.None)
                      .Select(int.Parse)
                      .ToArray();
    }

    private void ValidateNumbers(int[] numbers)
    {
        var negativeNumbers = numbers.Where(n => n < 0).ToArray();
        if (negativeNumbers.Any())
        {
            throw new Exception("Negatives not allowed: " + string.Join(", ", negativeNumbers));
        }
    }
}
