using System.Runtime.Intrinsics.X86;
using ChessWebGame.Figures;
using System.Text.Json;
using ChessWebGame.HelperClasses;
using ChessWebGame.ValidationLogic;

namespace ChessWebGame;

public partial class Engine
{
    public required string GameKey { init; get; }
    private FigureColor _nextMoveColor = FigureColor.White;
    
    
    public List<List<string>> GetBoardLayout()
    {
        List<List<string>> tempMainList = new List<List<string>>();
        List<string> tempInnerList;

        foreach (var row in _GameField)
        {
            tempInnerList = new List<string>();
            foreach (Figure fig in row)
            {
                if (fig.Symbol == FigureSymbol.Empty)
                {
                    tempInnerList.Add(string.Empty);
                }
                else
                {
                    tempInnerList.Add($"{fig.FigureColor.ToShortName()}{fig.Symbol.ToShortName()}");
                }
            }
            tempMainList.Add(tempInnerList);
        }

        return tempMainList;
    }

    private static void ProgressTurn(ref FigureColor nextMoveColorVar)
    {
        nextMoveColorVar = nextMoveColorVar == FigureColor.White ? FigureColor.Black : FigureColor.White;
    }
    
    public bool MoveTo(int[] From, int[] To)
    {
        Figure selectedFigure = _GameField[From[0]][From[1]];
        
        
        selectedFigure.CheckCollision(From,To,_GameField);
        
        
        // Check If Correct Player Moves
        if (_nextMoveColor != selectedFigure.FigureColor)
        {
            
            return false;
        }

        // Check if Move is Valid
        Validate.ValidateMove(From,To,_GameField);

        // Move Figure To New Position
        _GameField[To[0]][To[1]] = selectedFigure;
        _GameField[To[0]][To[1]].position = To;
        _GameField[From[0]][From[1]] = new Empty();
        
        // Next Move Setting
        ProgressTurn(ref _nextMoveColor);
        selectedFigure.HasMoved = true;
        
        Console.WriteLine("moze sie udalo");
        return true;
    }
}