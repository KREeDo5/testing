namespace GeometryLib.Tests;

public class Circle2DTests
{
    /// Тест инициализации круга с некорректным радиусом
    [Theory]
    [MemberData(nameof(ExceptionInitCircleTestData))]
    public void Cannot_Init_Circle(Point2D center, double radius)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Circle2D(center, radius));
    }

    public static TheoryData<Point2D, double> ExceptionInitCircleTestData()
    {
        return new TheoryData<Point2D, double>
        {
            // Инициализация с отрицательным радиусом
            { new Point2D(0, 0), -2.0 },
            // Инициализация с нулевым радиусом
            { new Point2D(0, 0), 0.0 },
        };
    }

    [Fact]
    public void Can_Get_Diameter()
    {
        Point2D center = new Point2D(0, 0);
        const double circleRadius = 2.0;
        const double diameter = circleRadius * 2;
        Assert.Equal(diameter, new Circle2D(center, circleRadius).Diameter);
    }

    [Fact]
    public void Can_Get_Circumference()
    {
        Point2D center = new Point2D(0, 0);
        const double circleRadius = 2.0;
        const double circumference = 2 * Math.PI * circleRadius;
        Assert.Equal(circumference, new Circle2D(center, circleRadius).Circumference);
    }

    [Fact]
    public void Can_Get_Area()
    {
        Point2D center = new Point2D(0, 0);
        const double circleRadius = 2.0;
        double area = Math.PI * Math.Pow(circleRadius, 2);
        Assert.Equal(area, new Circle2D(center, circleRadius).Area);
    }

    [Theory]
    [MemberData(nameof(IntersectsCircleTestData))]
    public void Can_check_that_circles_intersect(Circle2D a, Circle2D b, bool expected)
    {
        bool result = a.IntersectsWith(b);
        Assert.Equal(expected, result);
    }

    public static TheoryData<Circle2D, Circle2D, bool> IntersectsCircleTestData()
    {
        return new TheoryData<Circle2D, Circle2D, bool>
        {
            // Одинаковые круги в одной позиции
            { new Circle2D(new Point2D(0, 0), 2), new Circle2D(new Point2D(0, 0), 2), true },
            // Круг в другом круге
            { new Circle2D(new Point2D(0, 0), 20), new Circle2D(new Point2D(0, 0), 1), false },
            // Круги касаются внешними точками
            { new Circle2D(new Point2D(0, 0), 2), new Circle2D(new Point2D(4, 0), 2), true },
            // Круги пересекаются в двух точках (Круги Эйлера)
            { new Circle2D(new Point2D(0, 0), 2), new Circle2D(new Point2D(2, 0), 2), true },
        };
    }

    [Theory]
    [MemberData(nameof(DistanceToPointTestData))]
    public void Can_calculate_distance_to_point(Circle2D circle, Point2D point, double expected)
    {
        double result = circle.DistanceTo(point);
        Assert.Equal(expected, result);
    }

    public static TheoryData<Circle2D, Point2D, double> DistanceToPointTestData()
    {
        return new TheoryData<Circle2D, Point2D, double>
        {
            // Точка внутри окружности
            { new Circle2D(new Point2D(0, 0), 2), new Point2D(0, 0), 2.0 },
            // Точка на окружности
            { new Circle2D(new Point2D(0, 0), 2), new Point2D(0, 2), 0.0 },
            // Точка вне окружности
            { new Circle2D(new Point2D(0, 0), 2), new Point2D(0, 3), 1.0 },
            // Тест с отрицательными координатами
            { new Circle2D(new Point2D(0, 0), 2), new Point2D(0, -3), 1.0 },
        };
    }


    [Theory]
    [MemberData(nameof(DistanceToCircleTestData))]
    public void Can_calculate_distance_to_another_circle(Circle2D a, Circle2D b, double expected)
    {
        double result = a.DistanceTo(b);
        Assert.Equal(expected, result);
    }

    public static TheoryData<Circle2D, Circle2D, double> DistanceToCircleTestData()
    {
        return new TheoryData<Circle2D, Circle2D, double>
        {
            // Равные окружности на одной позиции
            { new Circle2D(new Point2D(0, 0), 2), new Circle2D(new Point2D(0, 0), 2), 0.0 },

            // Окружность внутри окружности
            { new Circle2D(new Point2D(0, 0), 2), new Circle2D(new Point2D(0, 0), 1), 1.0 },

            // Окружности не пересекаются
            { new Circle2D(new Point2D(0, 0), 2), new Circle2D(new Point2D(5, 0), 1), 2.0 },

            // Окружности касаются внешними точками
            { new Circle2D(new Point2D(0, 0), 2), new Circle2D(new Point2D(4, 0), 2), 0.0 },

            // Окружности пересекаются в двух точках (Круги Эйлера)
            { new Circle2D(new Point2D(0, 0), 2), new Circle2D(new Point2D(3, 0), 2), 0.0 },
        };
    }


    [Theory]
    [MemberData(nameof(ContainPointTestData))]
    public void Can_check_that_circle_contain_point(Circle2D circle, Point2D point, bool expected)
    {
        bool result = circle.Contains(point);
        Assert.Equal(expected, result);
    }

    public static TheoryData<Circle2D, Point2D, bool> ContainPointTestData()
    {
        return new TheoryData<Circle2D, Point2D, bool>
        {
            // Точка в центре окружности
            { new Circle2D(new Point2D(0, 0), 2), new Point2D(0, 0), true },

            // Точка на окружности
            { new Circle2D(new Point2D(0, 0), 2), new Point2D(2, 0), true },
            { new Circle2D(new Point2D(0, 0), 2), new Point2D(0, -2), true },

            // Точка внутри окружности
            { new Circle2D(new Point2D(0, 0), 2), new Point2D(1, 1), true },

            // Точка вне окружности
            { new Circle2D(new Point2D(0, 0), 2), new Point2D(2, 2), false },
            {
                new Circle2D(new Point2D(0, 0), 2), new Point2D(double.PositiveInfinity, double.PositiveInfinity), false
            },
        };
    }

    [Theory]
    [MemberData(nameof(ContainCircleTestData))]
    public void Can_check_that_circle_contain_circle(Circle2D a, Circle2D b, bool expected)
    {
        bool result = a.Contains(b);
        Assert.Equal(expected, result);
    }

    public static TheoryData<Circle2D, Circle2D, bool> ContainCircleTestData()
    {
        return new TheoryData<Circle2D, Circle2D, bool>
        {   
            // Второй круг лежит на первом
            { new Circle2D(new Point2D(0, 0), 2), new Circle2D(new Point2D(0, 0), 2), true },
            
            // Второй круг лежит внутри первого
            { new Circle2D(new Point2D(0, 0), 2), new Circle2D(new Point2D(0, 0), 1), true },
            
            // Второй круг не пересекается с первым и не лежит в нём
            { new Circle2D(new Point2D(0, 0), 2), new Circle2D(new Point2D(10, 10), 1), false },
            
            // Второй круг пересекается с первым в одной точке
            { new Circle2D(new Point2D(0, 0), 2), new Circle2D(new Point2D(4, 0), 2), false },
            
            // Второй круг пересекается с первым в двух точках
            { new Circle2D(new Point2D(0, 0), 2), new Circle2D(new Point2D(3, 0), 2), false },
        };
    }
}