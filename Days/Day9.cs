using Utils.Grids;
namespace Days;

public class Day9
{

    public void PartOne()
    {
        var inputs = File.ReadAllLines("./Inputs/Day9.txt");

        var cords = inputs.Select(_ =>
        {
            var num = _.Split(',').Select(int.Parse).ToList();
            return new Coordinate() { X = num[0], Y = num[1] };

        }).ToList();


        var edges = Enumerable.Range(0, inputs.Count())
          .SelectMany(i => Enumerable.Range(i + 1, inputs.Count() - (i + 1))
          .Select(j => (i, j)))
          .ToList();

        var area = edges
          .Where(_ => cords[_.i].X != cords[_.j].X && cords[_.i].Y != cords[_.j].Y)
          .Select(_ => (Area(cords[_.i], cords[_.j]), cords[_.i], cords[_.j])).OrderByDescending(_ => _.Item1).FirstOrDefault();

        Console.WriteLine($"Day9 PartOne: {area.Item1}");

    }

    private long Area(Coordinate a, Coordinate b)
    {

        var height = (long)Math.Abs(a.X - b.X) + 1;
        var width = (long)Math.Abs(a.Y - b.Y) + 1;
        return height * width;
    }

    private record Edge(Coordinate Coordinate1, Coordinate Coordinate2);


    private IEnumerable<Edge> Floor = [];

    public void PartTwo()
    {
        var inputs = File.ReadAllLines("./Inputs/Day9.txt");

        var cords = inputs.Select(_ =>
        {
            var num = _.Split(',').Select(int.Parse).ToList();
            return new Coordinate() { X = num[0], Y = num[1] };

        }).ToList();

        var edges = Enumerable.Range(0, inputs.Count()).Select(_ =>
        {
            if (_ == cords.Count() - 1)
            {
                return new Edge(cords[_], cords[0]);
            }

            return new Edge(cords[_], cords[_ + 1]);

        }).ToList();


        var allPossibelEdgesCombinations = Enumerable.Range(0, inputs.Count())
          .SelectMany(i => Enumerable.Range(i + 1, inputs.Count() - (i + 1))
          .Select(j => (i, j)))
          .ToList();

        var xPos = cords.Select(_ => _.X).Distinct().Order().ToList();
        var yPos = cords.Select(_ => _.Y).Distinct().Order().ToList();

        var compressedRows = xPos.Count() * 2 - 1;
        var compressedCollumns = yPos.Count() * 2 - 1;

        Console.WriteLine("buildEmptyMatrix");
        var matrix =
            Enumerable.Range(0, compressedRows)
              .Select(_ => Enumerable.Repeat(0, compressedCollumns).ToList())
              .ToList();


        Console.WriteLine("Matrix build");
        var grid = new Grid<int>(matrix);


        foreach (var edge in edges)
        {
            var maxX = Math.Max(xPos.IndexOf(edge.Coordinate1.X) * 2, xPos.IndexOf(edge.Coordinate2.X) * 2);
            var minX = Math.Min(xPos.IndexOf(edge.Coordinate1.X) * 2, xPos.IndexOf(edge.Coordinate2.X) * 2);

            var maxY = Math.Max(yPos.IndexOf(edge.Coordinate1.Y) * 2, yPos.IndexOf(edge.Coordinate2.Y) * 2);
            var minY = Math.Min(yPos.IndexOf(edge.Coordinate1.Y) * 2, yPos.IndexOf(edge.Coordinate2.Y) * 2);

            foreach (var x in Enumerable.Range(minX, maxX - minX + 1))
            {
                foreach (var y in Enumerable.Range(minY, maxY - minY + 1))
                {
                    grid.CellGrid[y][x].Value = 1;
                }
            }

        }


        Console.WriteLine("Matrix build");
        // add and edge of cells to the grid and start flood fill at -1,-1
        foreach (var cellY in grid.CellGrid)
        {

            var addBorder = new Cell<int>()
            {
                Coordinate = new Coordinate()
                {
                    X = cellY.Last().Coordinate.X,
                    Y = cellY.Last().Coordinate.Y + 1

                },
                Value = 0
            };


            var firstBorder = new Cell<int>()
            {
                Coordinate = new Coordinate()
                {
                    X = cellY.First().Coordinate.X,
                    Y = cellY.First().Coordinate.Y - 1

                },
                Value = 0
            };
            cellY.Insert(0, firstBorder);
            cellY.Add(addBorder);
        }

        var topCells = grid.CellGrid.First().Select(_ => new Cell<int>()
        {
            Coordinate = new Coordinate()
            {
                X = _.Coordinate.X,
                Y = -1
            },
            Value = 0
        });

        var bottemCells = grid.CellGrid.Last().Select(_ => new Cell<int>()
        {
            Coordinate = new Coordinate()
            {
                X = _.Coordinate.X,
                Y = -1
            },
            Value = 0
        });
        grid.CellGrid.Insert(0, topCells.ToList());
        grid.CellGrid.Add(bottemCells.ToList());

        // create offsetted grid by +1 +1
        var borderdGrid = new Grid<int>(grid.CellGrid.Select(_ => _.Select(_ => _.Value).ToList()).ToList());

        var queue = new Queue<Coordinate>();
        var visited = new HashSet<Coordinate>();

        queue.Enqueue(new Coordinate() { X = 0, Y = 0 });

        while (queue.Any())
        {
            // Console.WriteLine(queue.Count());
            var currentNode = queue.Dequeue();
            var currentCell = borderdGrid.GetCellByCoordinate(currentNode);
            var cellsInAllDirections = borderdGrid.GetCellsForEachDirection(currentCell);

            foreach (var (cell, _) in cellsInAllDirections)
            {
                if (cell is null || cell.Value == 1 || visited.Contains(cell.Coordinate)) continue;
                queue.Enqueue(cell.Coordinate);
                visited.Add(cell.Coordinate);

            }
        }

        foreach (var cell in borderdGrid.Cells)
        {
            if (visited.Contains(cell.Coordinate)) continue;
            cell.Value = 1;
        }

        borderdGrid.CellGrid.RemoveAt(0);
        borderdGrid.CellGrid.RemoveAt(borderdGrid.CellGrid.Count() - 1);
        foreach (var x in borderdGrid.CellGrid)
        {
            x.RemoveAt(x.Count() - 1);
            x.RemoveAt(0);

        }

        var reformatedGrid = new Grid<int>(borderdGrid.CellGrid.Select(_ => _.Select(_ => _.Value).ToList()).ToList());



        var emptyGrid = Enumerable.Range(0, borderdGrid.CellGrid.Count())
          .Select(_ => Enumerable.Range(0, borderdGrid.CellGrid.First().Count).Select(_ => 0).ToList()).ToList();

        var psaGrid = new Grid<int>(emptyGrid);

        // construct PSA
        for (int y = 0; y < psaGrid.CellGrid.Count(); y++)
        {
            for (int x = 0; x < psaGrid.CellGrid.First().Count; x++)
            {
                var cell = psaGrid.CellGrid[y][x];
                var left = psaGrid.GetCellBasedOnDirection(cell, DIRECTION.WEST)?.Value ?? 0;
                var top = psaGrid.GetCellBasedOnDirection(cell, DIRECTION.NORTH)?.Value ?? 0;
                var topLeft = psaGrid.GetCellBasedOnDirection(cell, DIRECTION.NORTHWEST)?.Value ?? 0;

                psaGrid.CellGrid[y][x].Value = left + top - topLeft + reformatedGrid.CellGrid[y][x].Value;
            }
        }

        var validArea = allPossibelEdgesCombinations.Where(
            _ => IsValid(new Edge(cords[_.i], cords[_.j]), xPos, yPos, psaGrid))
          .Select(_ => Area(cords[_.i], cords[_.j]))
          .OrderDescending()
          .FirstOrDefault();

        Console.WriteLine($"Day9 partTwo: {validArea}");

    }

    private bool IsValid(Edge edge, List<int> xPos, List<int> yPos, Grid<int> psaGrid)
    {

        var maxX = Math.Max(xPos.IndexOf(edge.Coordinate1.X) * 2, xPos.IndexOf(edge.Coordinate2.X) * 2);
        var minX = Math.Min(xPos.IndexOf(edge.Coordinate1.X) * 2, xPos.IndexOf(edge.Coordinate2.X) * 2);

        var maxY = Math.Max(yPos.IndexOf(edge.Coordinate1.Y) * 2, yPos.IndexOf(edge.Coordinate2.Y) * 2);
        var minY = Math.Min(yPos.IndexOf(edge.Coordinate1.Y) * 2, yPos.IndexOf(edge.Coordinate2.Y) * 2);

        int left = minX > 0 ? psaGrid.CellGrid[maxY][minX - 1].Value : 0;
        int top = minY > 0 ? psaGrid.CellGrid[minY - 1][maxX].Value : 0;
        int topleft = minX > 0 && 0 < minY ? psaGrid.CellGrid[minY - 1][minX - 1].Value : 0;

        var count = psaGrid.CellGrid[maxY][maxX].Value - left - top + topleft;

        return count == (maxX - minX + 1) * (maxY - minY + 1);
    }
}
