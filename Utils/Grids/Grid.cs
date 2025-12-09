namespace Utils.Grids;

public class Grid<T>
{
    public HashSet<Cell<T>> Cells { get; set; } = [];
    public List<List<Cell<T>>> CellGrid { get; set; } = [];
    public Cell<T> CurrentPossition { get; set; }


    public Grid(List<List<T>> grid)
    {
        for (int y = 0; y < grid.Count; y++)
        {
            CellGrid.Add([]);
            for (var x = 0; x < grid.First().Count; x++)
            {
                var cell = new Cell<T>()
                {
                    Coordinate = new Coordinate
                    {
                        Y = y,
                        X = x
                    },
                    Value = grid[y][x]
                };
                CellGrid[y].Add(cell);
                Cells.Add(cell);
            }
        }
    }

    public Grid(List<Cell<T>> cells)
    {
        Cells = new HashSet<Cell<T>>(cells);
    }

    public Grid(List<List<Cell<T>>> cells)
    {
        CellGrid = cells;
        Cells = cells.SelectMany(_ => _).ToHashSet();
    }

    public Grid(Grid<T> grid)
    {
        Cells = grid.Cells;
        CellGrid = grid.CellGrid;
        CurrentPossition = grid.CurrentPossition;
    }

    public Cell<T>? GetCellByCoordinate(Coordinate cords)
    {
        if (!IsInGrid(cords))
        {
            return null;
        }
        var cell = CellGrid[cords.Y][cords.X];

        //var cell = Cells.FirstOrDefault(_ => _.Coordinate.X == cords.X && _.Coordinate.Y == cords.Y);
        return cell;
    }

    public List<(Cell<T> cell, DIRECTION direction)>? GetCellsForEachDirection(Cell<T> cell)
    {
        var x = (DIRECTION[])Enum.GetValues(typeof(DIRECTION));
        var cells = x.Select(_ => (GetCellBasedOnDirection(cell, _), _))
            .Where(_ => _.Item1 != null)
            .ToList();
        return cells;
    }

    public Cell<T>? GetCellBasedOnDirection(Cell<T> cell, DIRECTION direction)
    {
        var coordinate = cell.Coordinate.GetCellBasedOnDirection(direction);

        if (!IsInGrid(coordinate))
        {
            return null;
        }

        var cellBasedOnDirection = CellGrid[coordinate.Y][coordinate.X];
        return cellBasedOnDirection;
    }

    public bool IsInGrid(Cell<T> cell)
    {
        var isNotInGrid = cell.Coordinate.Y < 0 || cell.Coordinate.X < 0 || cell.Coordinate.Y >= CellGrid.Count || cell.Coordinate.X >= CellGrid.First().Count;
        return !isNotInGrid;
    }

    public bool IsInGrid(Coordinate coordinate)
    {
        var isNotInGrid = coordinate.X < 0 || coordinate.Y < 0 || coordinate.X >= CellGrid.First().Count || coordinate.Y >= CellGrid.Count;

        return !isNotInGrid;
    }

    public void PrintGrid()
    {
        int maxX = Cells.Max(c => c.Coordinate.X);
        int maxY = Cells.Max(c => c.Coordinate.Y);

        T[,] grid = new T[maxY + 1, maxX + 1];

        foreach (var cell in Cells)
        {
            grid[cell.Coordinate.Y, cell.Coordinate.X] = cell.Value;
        }

        for (int y = 0; y <= maxY; y++)
        {
            for (int x = 0; x <= maxX; x++)
            {
                Console.Write(grid[y, x]?.ToString() ?? " " + "\t");
            }
            Console.WriteLine();
        }
    }

    public Cell<T> GetCellBasedOnDirection(Coordinate current, DIRECTION direction)
    {
        var coordinate = current.GetCellBasedOnDirection(direction);
        if (!IsInGrid(coordinate))
        {
            return null;
        }
        var cellBasedOnDirection = CellGrid[coordinate.Y][coordinate.X];
        return cellBasedOnDirection;
    }
}

public enum DIRECTION
{
    NORTH,
    SOUTH,
    EAST,
    WEST,
    NORTHEAST,
    NORTHWEST,
    SOUTHWEST,
    SOUTHEAST
}
public static class DirectionHelper
{

    public static bool IsOppisite(this DIRECTION direction, DIRECTION possibleNeighbour)
    {
        var isOtherCorner = direction switch
        {
            DIRECTION.NORTHWEST => possibleNeighbour == DIRECTION.SOUTHEAST,
            DIRECTION.NORTHEAST => possibleNeighbour == DIRECTION.SOUTHWEST,
            DIRECTION.SOUTHEAST => possibleNeighbour == DIRECTION.NORTHWEST,
            DIRECTION.SOUTHWEST => possibleNeighbour == DIRECTION.NORTHEAST,
            DIRECTION.NORTH => possibleNeighbour == DIRECTION.SOUTH,
            DIRECTION.SOUTH => possibleNeighbour == DIRECTION.NORTH,
            DIRECTION.WEST => possibleNeighbour == DIRECTION.EAST,
            DIRECTION.EAST => possibleNeighbour == DIRECTION.WEST,
            _ => throw new NotImplementedException(),
        }; ;
        return isOtherCorner;
    }

    public static List<DIRECTION> Corners
    {
        get
        {
            return [DIRECTION.NORTHEAST, DIRECTION.NORTHWEST, DIRECTION.SOUTHEAST, DIRECTION.SOUTHWEST];

        }
    }

    public static List<DIRECTION> UpDownLeftRight
    {
        get
        {
            return [DIRECTION.NORTH, DIRECTION.EAST, DIRECTION.SOUTH, DIRECTION.WEST];

        }
    }


}
