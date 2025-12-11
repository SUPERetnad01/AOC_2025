namespace Utils.Grids;

public class Cell<T>
{
    public Coordinate Coordinate { get; set; }

    public T Value { get; set; }

    public int Distance(Coordinate otherCoordinate)
    {
        var comparedX = Math.Abs(otherCoordinate.X - Coordinate.X);
        var comparedY = Math.Abs(otherCoordinate.Y - Coordinate.Y);

        return comparedX + comparedY;
    }



}

public class Coordinate
{
    public int Y { get; set; }
    public int X { get; set; }


    public override string ToString()
    {
        return $"x:{X}, y:{Y}";
    }
    public Coordinate()
    {
    }

    public Coordinate(Coordinate coordinate)
    {
        X = coordinate.X;
        Y = coordinate.Y;
    }



    public int ManhatanDistance(Coordinate otherCoordinate)
    {
        var xDiff = Math.Abs(X - otherCoordinate.X);
        var yDiff = Math.Abs(Y - otherCoordinate.Y);

        return xDiff + yDiff;
    }


    public Coordinate GetCellBasedOnDirection(DIRECTION direction)
    {
        var cord = direction switch
        {
            DIRECTION.NORTH => new Coordinate() { Y = Y - 1, X = X },
            DIRECTION.SOUTH => new Coordinate() { Y = Y + 1, X = X },
            DIRECTION.EAST => new Coordinate() { Y = Y, X = X + 1 },
            DIRECTION.WEST => new Coordinate() { Y = Y, X = X - 1 },
            DIRECTION.NORTHEAST => new Coordinate() { Y = Y - 1, X = X + 1 },
            DIRECTION.NORTHWEST => new Coordinate() { Y = Y - 1, X = X - 1 },
            DIRECTION.SOUTHWEST => new Coordinate() { Y = Y + 1, X = X - 1 },
            DIRECTION.SOUTHEAST => new Coordinate() { Y = Y + 1, X = X + 1 },
            _ => throw new NotImplementedException(),
        };

        return cord;
    }

    public static Coordinate operator +(Coordinate p1, Coordinate p2)
    {
        return new Coordinate()
        {

            X = p1.X + p2.X,
            Y = p1.Y + p2.Y
        };
    }

    public static Coordinate operator -(Coordinate p1, Coordinate p2)
    {
        return new Coordinate()
        {

            X = p1.X - p2.X,
            Y = p1.Y - p2.Y
        };
    }

    public static bool operator ==(Coordinate p1, Coordinate p2)
    {
        var x = p1.X == p2.X;
        var y = p1.Y == p2.Y;

        return x && y;
    }

    public static bool operator !=(Coordinate? p1, Coordinate? p2)
    {
        var x = p1?.X != p2?.X;
        var y = p1?.Y != p2?.Y;

        return x || y;
    }
    public override bool Equals(object obj)
    {
        if (obj is Coordinate point)
        {
            return this == point;
        }
        return false;
    }

    public int CrossProduct(Coordinate coordinate)
    {
        return coordinate.X * Y - coordinate.Y * X;

    }

    // Override GetHashCode
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}
