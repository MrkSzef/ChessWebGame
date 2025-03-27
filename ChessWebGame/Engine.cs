using System.Runtime.Intrinsics.X86;
using ChessWebGame.Figures;
using System.Text.Json;
using ChessWebGame.ValidationLogic;

namespace ChessWebGame;

public partial class Engine
{
    public required string GameKey { init; get; }
    private int _NextMoveColor = 0;
    
    
    public string GetBoardLayout()
    {
        List<List<string>> TempMainList = new List<List<string>>();
        List<string> TempInnerList;

        foreach (var row in _GameField)
        {
            TempInnerList = new List<string>();
            foreach (Figure fig in row)
            {
                if (fig.Symbol == "E")
                {
                    TempInnerList.Add(string.Empty);
                }
                else
                {
                    TempInnerList.Add($"{(Figure.ColorShort)fig.FigureColor}{fig.Symbol}");
                }
            }
            TempMainList.Add(TempInnerList);
        }

        return JsonSerializer.Serialize(TempMainList);
    }
    
    public bool MoveTo(int[] From, int[] To)
    {
        Figure SelectedFigure = _GameField[From[0]][From[1]];
        
        
        SelectedFigure.CheckCollision(From,To,_GameField);
        
        
        // Next Move Checker

        if (_NextMoveColor != SelectedFigure.FigureColor)
        {
            Console.WriteLine($"Not Your Turn {SelectedFigure.FigureColor}");
            return false;
        }

        // Check if Move is Valid
        Validate.ValidateMove(From,To,_GameField);
        
        
        Console.WriteLine(SelectedFigure.Symbol);
        _GameField[To[0]][To[1]] = SelectedFigure;
        _GameField[To[0]][To[1]].position = To;
        _GameField[From[0]][From[1]] = new Empty();
        
        // Next Move Setting
        _NextMoveColor = _NextMoveColor == 0 ? 1 : 0;
        SelectedFigure.HasMoved = true;
        
        Console.WriteLine("moze sie udalo");
        return true;
    }
}