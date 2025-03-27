using ChessWebGame.Figures;

namespace ChessWebGame.ValidationLogic;

public partial class Validate
{
    static readonly int MaxBoardDimension = 7;

    
    public static bool ValidateMove(int[] From, int[] To, List<List<Figure>> _GameState)
    {
        Figure selectedFigure = _GameState[From[0]][From[1]];
        
        // Check if the data is valid
        if (From.Length != 2 & To.Length != 2) return false;
        
        // Check if the move is on the board
        if (From[0] > MaxBoardDimension & From[1] > MaxBoardDimension & To[0] > MaxBoardDimension & To[1] > MaxBoardDimension) return false;
        
        // Check if the selected figure is empty
        if (selectedFigure is Empty) return false;
        
        // Check if the move is the same
        if (From[0] == To[0] & From[1] == To[1]) return false;
        
        // Check if there's a figure with the same color in the way
        if (selectedFigure.FigureColor == _GameState[To[0]][To[1]].FigureColor) return false;
        
        // Check if the move is valid for the selected figure
        if (!selectedFigure.ValidateMove(From,To,_GameState)) return false;
        
        // Check if there's a figure in the way
        if (!ValidateCollision(From, To, _GameState) && selectedFigure is not Knight) return false;
        
        return true;
    }
}