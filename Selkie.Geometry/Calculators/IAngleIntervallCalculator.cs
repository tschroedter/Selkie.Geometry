namespace Selkie.Geometry.Calculators
{
    public interface IAngleIntervallCalculator
        : ICalculator
    {
        double FromAngleInRadians { get; set; }
        double ToAngleInRadians { get; set; }
        int Steps { get; set; }
        Constants.TurnDirection TurnDirection { get; set; }
        double Intervall { get; }
        double MaxAngleInRadians { get; set; }
    }
}