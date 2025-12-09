namespace Utils;

public record CartisianCoordinate(int X, int Y, int Z)
{


    public double StraightLineDistance(CartisianCoordinate coordinate)
    {
        return Math.Sqrt(Math.Pow(X - coordinate.X, 2) + Math.Pow(Y - coordinate.Y, 2) + Math.Pow(Z - coordinate.Z, 2));
    }


};
