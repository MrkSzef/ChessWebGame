namespace ChessWebGame.Figures;

public class Pawn : Figure
{
    public Pawn() : base(FigureSymbol.Pawn){}

    
    
    public override bool ValidateMove(int[] From, int[] To, List<List<Figure>> _GameState)
    {
        int direction = FigureColor == (int)FigureColor.White ? -1 : 1;
        if (To[1] == From[1] && To[0] == From[0] + direction 
                             && _GameState[To[0]][To[1]].GetType() == typeof(Empty))
        {
            return true;
        }


        if (Math.Abs(To[1] - From[1]) == 1 && To[0] == From[0] + direction 
                                           && _GameState[To[0]][To[1]].GetType() != typeof(Empty)
                                           && _GameState[To[0]][To[1]].FigureColor != FigureColor)
        {
            return true;
        }

        if (!_hasMoved && To[1] == From[1] && To[0] == From[0] + (2 * direction)
            && _GameState[From[0] + direction][From[1]].GetType() == typeof(Empty)
            && _GameState[To[0]][To[1]].GetType() == typeof(Empty))
        {
            return true;
        }

        return false;
    }
}