using System.Text.RegularExpressions;

namespace Days;

public class Day5
{

    private record IdRange
    {
        public long Start { get; set; }
        public long End { get; set; }

        public IdRange(long start, long end)
        {

            Start = start;
            End = end;

        }


        public bool IsInRange(long input)
        {
            return Start <= input && input <= End;
        }

        public IEnumerable<long> GetAllPossibleValues()
        {
            var result = new List<long>();

            for (var i = Start; i <= End; i++)
            {
                result.Add(i);
            }

            return result;
        }

        public bool Intersects(IdRange otherrange)
        {

            return Start <= otherrange.End && otherrange.Start <= End;

        }

        public void Merge(IdRange otherRange)
        {
            Start = Math.Min(Start, otherRange.Start);
            End = Math.Max(End, otherRange.End);

        }

        public long Distance()
        {
            return End - Start + 1;
        }
    }

    public void PartOne()
    {

        var input = File.ReadAllText("./Inputs/Day5.txt");

        var sections = input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);

        var ranges = sections[0]
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => Regex.Match(line, @"(\d+)-(\d+)"))
            .Where(m => m.Success)
            .Select(m => new IdRange(long.Parse(m.Groups[1].Value), long.Parse(m.Groups[2].Value)))
            .ToList();

        var numbers = sections[1]
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToList();

        var spoiledIngredients = numbers.Where(id => ranges.Any(_ => _.IsInRange(id))).Count();

        Console.WriteLine($"Day5 PartOne: {spoiledIngredients}");
    }

    public void PartTwo()
    {
        var input = File.ReadAllText("./Inputs/Day5.txt");

        var sections = input.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);

        var ranges = sections[0]
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => Regex.Match(line, @"(\d+)-(\d+)"))
            .Where(m => m.Success)
            .Select(m => new IdRange(long.Parse(m.Groups[1].Value), long.Parse(m.Groups[2].Value)))
            .ToList();


        var mergedRanges = new List<IdRange>();

        foreach (var range in ranges)
        {
            var hasMerged = false;
            foreach (var mergedRange in mergedRanges)
            {
                if (mergedRange.Intersects(range))
                {
                    mergedRange.Merge(range);
                    hasMerged = true;
                    break;

                }

            }

            if (hasMerged == false)
            {
                mergedRanges.Add(range);
            }

            var changed = true;

            while (changed)
            {
                changed = false;

                for (int i = 0; i < mergedRanges.Count; i++)
                {
                    for (int j = i + 1; j < mergedRanges.Count; j++)
                    {
                        var a = mergedRanges[i];
                        var b = mergedRanges[j];

                        if (a.Intersects(b))
                        {
                            a.Merge(b);
                            mergedRanges.RemoveAt(j);
                            changed = true;
                            break;
                        }
                    }

                    if (changed)
                    {
                        break;
                    }
                }
            }

        }

        Console.WriteLine($"Day5 PartTwo: {mergedRanges.Sum(_ => _.Distance())}");

    }

}
