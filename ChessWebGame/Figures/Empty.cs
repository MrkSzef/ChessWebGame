namespace ChessConsoleGame.Figures;

public class Empty : Figure
{
    public Empty() : base("E")
    {
        position = new[] { 100, 100 };
    }
}