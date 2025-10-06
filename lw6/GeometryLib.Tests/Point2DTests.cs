namespace GeometryLib.Tests;

public class Point2DTests
{   
    [Fact]
    public void Init_Correctly()
    {
        Point2D p = new Point2D(1.5, -2.5);
        Assert.Equal(1.5, p.X);
        Assert.Equal(-2.5, p.Y);
    }
    
    [Theory]
    [MemberData(nameof(DistanceTestData))]
    public void Can_calculate_distance(Point2D a, Point2D b, double expected)
    {
        double result = a.DistanceTo(b);
        Assert.Equal(expected, result, Point2D.Tolerance);
    }

    public static TheoryData<Point2D, Point2D, double> DistanceTestData()
    {
        return new TheoryData<Point2D, Point2D, double>
        {
            // Основные сценарии
            { new Point2D(0, 0), new Point2D(0, 2), 2.0 },
            { new Point2D(0, 0), new Point2D(5, 5), 7.071 },
            { new Point2D(0, 0), new Point2D(2, 2.236), 3.0 },
            { new Point2D(0, 0), new Point2D(3, 4), 5.0 },
            { new Point2D(3, 4), new Point2D(0, 0), 5.0 },
            // Пограничные случаи
            { new Point2D(0, 0), new Point2D(0, 0), 0.0 },
            { new Point2D(0.0001, 0), new Point2D(0, 0.0001), 0.0001 },
            { new Point2D(0.0001, 0), new Point2D(0, 0.0001), 0.000141 },
            { new Point2D(0, 0), new Point2D(-3, -4), 5.0 },
            { new Point2D(0, 0), new Point2D(double.MaxValue, 0), double.PositiveInfinity },
            { new Point2D(0, 0), new Point2D(double.MinValue, 0), double.PositiveInfinity },
        };
    }
}