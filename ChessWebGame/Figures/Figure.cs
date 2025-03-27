namespace ChessWebGame.Figures;

public class Figure
{
    #region Params

    private readonly FigureSymbol _symbol = FigureSymbol.Empty;
    private FigureColor _figureColor;
    
    public int[] position { set; get; }
    internal bool _hasMoved = false;
    
    #endregion
    
    #region ParamsConstructors
    
    public bool HasMoved
    {
        set => _hasMoved = value;
    }
    public FigureSymbol Symbol
    {
        get => _symbol;
    }
    public FigureColor FigureColor
    {
        get => _figureColor;
        init => _figureColor = value;
    }
    
    #endregion
    
    #region Constructor
    public Figure(FigureSymbol symbol)
    {
        _symbol = symbol;
    }

    #endregion



    /// <param name="From">[0]Ycords [1]Xcords</param>
    /// <param name="To">[0]Ycords [1]Xcords</param>
    public virtual bool ValidateMove(int[] From, int[] To, List<List<Figure>> _GameState)
    {
        if (From[0] < 8 & From[1] < 8)
        {
            return true;
        }

        return false;
    }
}