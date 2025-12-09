using Utils.Grids;

namespace Days;

public class Day7
{
    public void PartOne()
    {
        var inputs = File.ReadAllLines("./Inputs/Day7.txt");

        var gridList = inputs.Select(_ => _.Select(_ => _).ToList()).ToList();

        var grid = new Grid<char>(gridList);

        var start = grid.Cells.FirstOrDefault(_ => _.Value == 'S');

        grid.CurrentPossition = start;

        var amountOfSplits = AmountOfSplits(grid).Count();

        Console.WriteLine($"Day7 PartOne: {amountOfSplits}");
    }

    private HashSet<Coordinate> scanedCords = new HashSet<Coordinate>();

    private HashSet<Coordinate> AmountOfSplits(Grid<char> grid)
    {

        if (scanedCords.Contains(grid.CurrentPossition.Coordinate))
        {
            return [];
        }


        scanedCords.Add(new Coordinate(grid.CurrentPossition.Coordinate));

        while (grid.CurrentPossition.Value != '^')
        {
            grid.CurrentPossition.Value = '|';
            var currentCell = grid.GetCellBasedOnDirection(grid.CurrentPossition, DIRECTION.SOUTH);

            if (currentCell == null)
            {
                return [];
            }

            grid.CurrentPossition = currentCell;

        }

        var currentPos = new Coordinate(grid.CurrentPossition.Coordinate);

        var westGrid = new Grid<char>(grid);
        westGrid.CurrentPossition = westGrid.GetCellBasedOnDirection(grid.CurrentPossition, DIRECTION.WEST);
        var west = AmountOfSplits(westGrid);

        var eastGrid = new Grid<char>(grid);
        eastGrid.CurrentPossition = eastGrid.GetCellBasedOnDirection(grid.CurrentPossition, DIRECTION.EAST);
        var east = AmountOfSplits(eastGrid);

        east.UnionWith(west);
        east.Add(currentPos);

        return east;
    }



    public void PartTwo()
    {
        var inputs = File.ReadAllLines("./Inputs/Day7.txt");

        var gridList = inputs.Select(_ => _.Select(_ => _).ToList()).ToList();

        var grid = new Grid<char>(gridList);

        var start = grid.Cells.FirstOrDefault(_ => _.Value == 'S');

        grid.CurrentPossition = start;

        var amountOfSplits = AmountOfTimeLines(grid, 1);

        Console.WriteLine($"Day7 PartTwo: {amountOfSplits}");
    }

    private Dictionary<Coordinate, long> timeLineMap = new();

    private long AmountOfTimeLines(Grid<char> grid, long timeLinesBefore)
    {
        var originalPos = new Coordinate(grid.CurrentPossition.Coordinate);
        if (timeLineMap.TryGetValue(originalPos, out var val))
        {
            return val;
        }

        while (grid.CurrentPossition.Value != '^')
        {
            grid.CurrentPossition.Value = '|';
            var currentCell = grid.GetCellBasedOnDirection(grid.CurrentPossition, DIRECTION.SOUTH);

            if (currentCell == null)
            {
                return 0;
            }

            grid.CurrentPossition = currentCell;

        }

        timeLinesBefore++;

        var currentPos = new Coordinate(grid.CurrentPossition.Coordinate);

        var westGrid = new Grid<char>(grid);
        westGrid.CurrentPossition = westGrid.GetCellBasedOnDirection(grid.CurrentPossition, DIRECTION.WEST);
        var west = AmountOfTimeLines(westGrid, 0);

        var eastGrid = new Grid<char>(grid);
        eastGrid.CurrentPossition = eastGrid.GetCellBasedOnDirection(grid.CurrentPossition, DIRECTION.EAST);
        var east = AmountOfTimeLines(eastGrid, 0);
        var newTotal = timeLinesBefore + east + west;

        timeLineMap.TryAdd(originalPos, newTotal);

        return newTotal;
    }
}
