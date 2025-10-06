namespace GeometryLib;

/// <summary>
/// Класс Circle2D представляет круг в двухмерном Евклидовом пространстве.
/// </summary>
public class Circle2D
{
    // Максимальное отклонение, при котором значения всё ещё считаются равными.
    private const double Tolerance = 1e-10;
    
    private readonly Point2D _center;

    private readonly double _radius;
    
    public Circle2D(Point2D center, double radius)
    {
        if (radius <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(radius), "Радиус должен быть положительным.");
        }

        _center = center;
        _radius = radius;
    }
    
    /// <summary>
    /// Возвращает диаметр окружности
    /// </summary>
    public double Diameter => _radius * 2;

    /// <summary>
    /// Возвращает длину окружности
    /// </summary>
    public double Circumference => 2 * Math.PI * _radius;

    /// <summary>
    /// Возвращает площадь круга
    /// </summary>
    public double Area => Math.PI * Math.Pow(_radius, 2);

    /// <summary>
    /// Возвращает расстояние от точки `p` до ближайшей точки окружности
    /// </summary>
    public double DistanceTo(Point2D p)
    {
        double result = _radius - p.DistanceTo(_center);
        return Math.Abs(result);
    }

    /// <summary>
    /// Возвращает расстояние между ближайшими друг к другу точками окружностей
    /// </summary>
    public double DistanceTo(Circle2D p)
    {
        double distanceBetweenCenters = _center.DistanceTo(p._center);
        double sumOfTwoRadius = _radius + p._radius;
        // Если центры окружностей не совпадают
        if (distanceBetweenCenters > 0)
        {
            // Если окружности находятся не друг в друге и не пересекаются
            if (sumOfTwoRadius < distanceBetweenCenters)
            {
                return distanceBetweenCenters - sumOfTwoRadius;
            }

            // Если одна окружность полностью внутри другой
            double minR = Math.Min(p._radius, _radius);
            double maxR = Math.Max(p._radius, _radius);
            if (distanceBetweenCenters + minR < maxR)
            {
                return maxR - (distanceBetweenCenters + minR);
            }
        }
        // Если центры окружностей совпадают, но радиусы разные
        else if (Math.Abs(p._radius - _radius) > Tolerance)
        {
            return Math.Abs(p._radius - _radius);
        }

        return 0;
    }

    /// <summary>
    /// Пересекаются ли окружности
    /// </summary>
    public bool IntersectsWith(Circle2D other)
    {
        double result = DistanceTo(other);
        return result < Tolerance;
    }

    /// <summary>
    /// Лежит ли точка внутри круга
    /// </summary>
    public bool Contains(Point2D p)
    {
        return _center.DistanceTo(p) <= _radius;
    }

    /// <summary>
    /// Лежит ли другой круг полностью внутри этого круга
    /// </summary>
    public bool Contains(Circle2D other)
    {
        double distanceBetweenCenters = _center.DistanceTo(other._center);
        return (distanceBetweenCenters + other._radius) <= _radius;
    }
}