namespace Days;

public class Day2
{
    private record IdRange(long start, long end)
    {
        public IEnumerable<long> GetInvalidIdsPartOne()
        {
            var range = new List<long>();
            for (var i = end; i > start - 1; i--)
            {
                if (IsValidIdPartOne(i))
                {
                    range.Add(i);
                }

            }
            return range;
        }

        public IEnumerable<long> GetInvalidIdsPartTwo()
        {
            var range = new List<long>();
            for (var i = end; i > start - 1; i--)
            {
                if (IsValidIdPartTwo(i))
                {
                    range.Add(i);
                }

            }
            return range;
        }

        private bool IsValidIdPartTwo(long id)
        {

            var idString = id.ToString();
            var halfway = idString.Length / 2;

            var invalidIds = Enumerable
              .Range(1, halfway)
              .Select(chunckSize =>
              {
                  var xx = idString.Chunk(chunckSize).Select(_ => string.Join("", _));
                  return xx;
              })
              .Where(_ => !_.Distinct().Skip(1).Any()).Select(_ => _.First()).ToList();

            return invalidIds.Any();

        }
        private bool IsValidIdPartOne(long id)
        {

            var idString = id.ToString();
            var halfway = idString.Length / 2;

            if (idString.Length % 2 != 0)
            {
                return false;
            }

            var firstPart = idString.Substring(0, halfway);
            var secondPart = idString.Substring(halfway);

            return firstPart == secondPart;

        }
    };

    public void PartOne()
    {
        var inputs = File.ReadAllText("./Inputs/Day2.txt");

        var ranges = inputs.Split(',').Select(_ =>
        {
            var range = _.Split('-');
            return new IdRange(Convert.ToInt64(range[0]), Convert.ToInt64(range[1]));
        });

        var total = ranges.Sum(_ => _.GetInvalidIdsPartOne().Sum());

        Console.WriteLine($"Day2 partOne: {total}");

    }
    public void PartTwo()
    {
        var inputs = File.ReadAllText("./Inputs/Day2.txt");

        var ranges = inputs.Split(',').Select(_ =>
        {
            var range = _.Split('-');
            return new IdRange(Convert.ToInt64(range[0]), Convert.ToInt64(range[1]));
        });

        var total = ranges.Sum(_ => _.GetInvalidIdsPartTwo().Sum());
        Console.WriteLine($"Day2 partTwo: {total}");

    }
}
