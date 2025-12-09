using Utils;
namespace Days;


public class Day8
{
    private List<int> Parrent = new List<int>();

    public void PartOne()
    {
        var inputs = File.ReadAllLines("./Inputs/Day8.txt");

        var junctions = inputs.Select(_ =>
        {
            var splitString = _.Split(',').Select(int.Parse).ToList();
            return new CartisianCoordinate(splitString[0], splitString[1], splitString[2]);
        }).ToList();

        var edges = Enumerable.Range(0, inputs.Count())
          .SelectMany(i => Enumerable.Range(i + 1, inputs.Count() - (i + 1))
          .Select(j => (i, j)))
          .ToList();


        var sortedEdges = edges.OrderBy(_ => junctions[_.i].StraightLineDistance(junctions[_.j]));
        Parrent = Enumerable.Range(0, junctions.Count()).ToList();

        foreach (var (i, j) in sortedEdges.Take(1000))
        {
            Merge(i, j);
        }

        var sizes = Enumerable.Range(0, junctions.Count()).Select(_ => 0).ToList();
        foreach (var box in Enumerable.Range(0, junctions.Count()))
        {
            sizes[GetRoot(box)] += 1;

        }

        var size = sizes.OrderDescending().Distinct().Take(3).ToList().Aggregate(1L, (acc, x) => acc * x);
        Console.WriteLine($"Day8 PartOne: {size}");

    }

    public void PartTwo()
    {
        var inputs = File.ReadAllLines("./Inputs/Day8.txt");

        var junctions = inputs.Select(_ =>
        {
            var splitString = _.Split(',').Select(int.Parse).ToList();
            return new CartisianCoordinate(splitString[0], splitString[1], splitString[2]);
        }).ToList();

        var edges = Enumerable.Range(0, inputs.Count())
          .SelectMany(i => Enumerable.Range(i + 1, inputs.Count() - (i + 1))
          .Select(j => (i, j)))
          .ToList();


        var sortedEdges = edges.OrderBy(_ => junctions[_.i].StraightLineDistance(junctions[_.j]));
        Parrent = Enumerable.Range(0, junctions.Count()).ToList();

        var circuits = junctions.Count();

        foreach (var (x, y) in sortedEdges)
        {
            if (GetRoot(x) == GetRoot((y)))
            {
                continue;
            }

            Merge(x, y);
            circuits--;

            if (circuits == 1)
            {
                var selution = junctions[x].X * junctions[y].X;
                Console.WriteLine($"Day8 PartTwo: {selution}");
                break;
            }


        }

    }

    private int GetRoot(int index)
    {
        if (Parrent[index] == index)
        {
            return index;
        }
        Parrent[index] = GetRoot(Parrent[index]);
        return Parrent[index];
    }

    private void Merge(int root, int child)
    {
        Parrent[GetRoot(root)] = GetRoot(child);
    }
}
