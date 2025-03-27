namespace ChessWebGame.Figures;

public class Empty : Figure
{
    public Empty() : base(FigureSymbol.Empty)
    {
        position = new[] { 100, 100 };
        FigureColor = FigureColor.None;
    }
}