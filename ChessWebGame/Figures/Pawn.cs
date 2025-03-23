namespace ChessConsoleGame.Figures;

public class Pawn : Figure
{
    public Pawn() : base("P"){}

    
    
    public override bool ValidateMove(int[] From, int[] To, List<List<Figure>> _GameState)
    {
        if (_GameState[To[0]+1][To[1]].GetType() != typeof(Empty))
        {
            return false;
        }
        if (From[1] != To[1])
        {
            return false;
        }
        else if (_NumberOfFigureMoves == 0 && To[0]-From[0] <= 2)
        {
            return true;
        }

        return false;
    }
}