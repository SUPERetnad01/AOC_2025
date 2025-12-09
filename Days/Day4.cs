using Utils.Grids;

namespace Days;


public class Day4
{

    public void PartOne()
    {
        var inputs = File.ReadAllLines("./Inputs/Day4.txt");

        var gridList = inputs.Select(_ => _.Select(_ => _).ToList()).ToList();

        var grid = new Grid<char>(gridList);

        var result = 0;
        foreach (var cell in grid.Cells)
        {

            if (cell.Value == '.')
            {
                continue;
            }

            var neighbours = grid.GetCellsForEachDirection(cell);
            var amountOfRollsOfPaper = neighbours?.Where(_ => _.cell.Value == '@').Count();

            if (amountOfRollsOfPaper < 4)
            {
                result++;
            }
        }

        Console.WriteLine($"Day4 PartOne: {result}");

    }

    public void PartTwo()
    {
        var inputs = File.ReadAllLines("./Inputs/Day4.txt");

        var gridList = inputs.Select(_ => _.Select(_ => _).ToList()).ToList();

        var grid = new Grid<char>(gridList);

        var (g, amountOfPaperRemoved) = RemoveRollOfPaper(grid);


        var total = amountOfPaperRemoved;

        while (amountOfPaperRemoved != 0)
        {
            grid = g;
            (g, amountOfPaperRemoved) = RemoveRollOfPaper(grid);
            total += amountOfPaperRemoved;



        }


        Console.WriteLine($"Day4 PartTwo: {total}");
    }

    private (Grid<char> newGrid, int rollsRemoved) RemoveRollOfPaper(Grid<char> grid)
    {

        var result = 0;
        var cellsToUpdate = new List<Cell<char>>();


        foreach (var cell in grid.Cells)
        {

            if (cell.Value == '.')
            {
                continue;
            }

            var neighbours = grid.GetCellsForEachDirection(cell);
            var amountOfRollsOfPaper = neighbours?.Where(_ => _.cell.Value == '@').Count();

            if (amountOfRollsOfPaper < 4)
            {
                cellsToUpdate.Add(cell);
                result++;
            }
        }

        foreach (var cell in cellsToUpdate)
        {

            var cellToUpdate = grid.GetCellByCoordinate(cell.Coordinate);
            cellToUpdate!.Value = '.';
        }

        return (grid, result);
    }
}
