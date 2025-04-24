using System.Runtime.Intrinsics.X86;
using ChessWebGame.Figures;
using System.Text.Json;
using ChessWebGame.HelperClasses;
using ChessWebGame.ValidationLogic;

namespace ChessWebGame;

public class Engine
{
    public required string GameKey { init; get; }
    private FigureColor _nextMoveColor = FigureColor.White;

    public FigureColor NextMoveColor
    {
        get => _nextMoveColor;
    }

    private readonly List<List<Figure>> _gameField = BoardInitializer.InitializeGameBoard();
    
    
    
    public Player WhitePlayer = new Player()
    {
        PlayerColor = FigureColor.White,
        IsConnected = false
    };

    public Player BlackPlayer = new Player()
    {
        PlayerColor = FigureColor.Black,
        IsConnected = false
    };
    
    public List<List<string>> GetBoardLayout()
    {
        List<List<string>> tempMainList = new List<List<string>>();
        List<string> tempInnerList;

        foreach (var row in _gameField)
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
        Figure selectedFigure = _gameField[From[0]][From[1]];
        
        // Check If Correct Player Moves
        if (_nextMoveColor != selectedFigure.FigureColor)
        {
            return false;
        }

        if (!IsGameStarted())
        {
            return false;
        }
        
        // Validate For Basic Rules
        if (!Validate.ValidateMove(From, To, _gameField)) return false;
        
        // Validate Selected Figure
        if (!selectedFigure.ValidateMove(From,To,_gameField)) return false;
        
        // Move Figure To New Position
        _gameField[To[0]][To[1]] = selectedFigure;
        _gameField[To[0]][To[1]].position = To;
        _gameField[From[0]][From[1]] = new Empty();
        
        // Next Move Setting
        ProgressTurn(ref _nextMoveColor);
        selectedFigure.HasMoved = true;
        
        return true;
    }

    public bool IsGameStarted()
    {
        return this.WhitePlayer.IsConnected && this.BlackPlayer.IsConnected;
    }
    
}