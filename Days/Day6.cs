namespace Days;

public class Day6
{

    public void PartOne()
    {

        var input = File.ReadAllLines("./Inputs/Day6.txt");

        var rows = input.Take(input.Length - 1).Select(_ => _.Split(' ').Where(_ => !string.IsNullOrEmpty(_)).Select(_ => int.Parse(_))).ToList();
        var multiplication = input.Last().Split(' ').Where(_ => !string.IsNullOrEmpty(_));

        var problems = rows.First()
          .Zip(rows[1])
          .Zip(rows[2])
          .Zip(rows[3])
          .Zip(multiplication)
          .Select(x =>
          {
              var ((((a, b), c), d), m) = x;        // full deconstruction

              return Calculate(a, b, c, d, m);
          }).Sum();

        Console.WriteLine($"Day6 PartOne: {problems}");

    }

    private long Calculate(int first, int second, int third, int fourth, string multiplication) => multiplication switch
    {
        "*" => (long)first * second * third * fourth,
        "+" => (long)first + second + third + fourth,
        _ => throw new InvalidOperationException($"PANIEK {multiplication}"),
    };

    private long CalculateV2(IEnumerable<long> input, string multiplication) => multiplication switch
    {
        "*" => (long)input.Aggregate(1L, (acc, x) => acc * x),
        "+" => (long)input.Sum(),
        _ => throw new InvalidOperationException($"PANIEK {multiplication}"),
    };



    public void PartTwo()
    {

        var input = File.ReadAllLines("./Inputs/Day6.txt");

        var rows = input.Take(input.Length - 1).ToList();

        var multiplication = input.Last().Split(' ').Where(_ => !string.IsNullOrEmpty(_));

        var problems =
            rows[0]
            .Zip(rows[1])
            .Zip(rows[2])
            .Zip(rows[3])
            .Select(x =>
        {
            var (((a, b), c), d) = x;
            return (a, b, c, d);
        });

        var sumResult = new List<long>();

        var total = new List<List<long>>();

        foreach (var (first, second, third, fourth) in problems)
        {
            if (first == ' ' && second == ' ' && third == ' ' && fourth == ' ')
            {
                total.Add(new List<long>(sumResult));
                sumResult.Clear();
            }
            else
            {
                var octoMathNumber = long.Parse($"{first}{second}{third}{fourth}".Trim());
                sumResult.Add(octoMathNumber);
            }
        }

        total.Add(new List<long>(sumResult));

        var totalSum = total
          .Zip(multiplication, (_, m) => CalculateV2(_, m))
          .Sum();

        Console.WriteLine($"Day6 PartTwo: {totalSum}");
    }

}

