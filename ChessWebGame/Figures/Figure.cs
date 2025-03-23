namespace ChessWebGame.Figures;

public class Figure
{
    #region Params

    protected string _symbol = "E";
    private int _figureColor;
    public int[] position { set; get; }
    internal bool hasMoved = false;
    
    #endregion

    #region ParamsConstructors
    
    public string Symbol
    {
        get => _symbol;
    }
    public int FigureColor
    {
        get => _figureColor;
        init => _figureColor = (int)value;
    }
    
    #endregion
    
    #region Constructor
    public Figure(string symbol)
    {
        _symbol = symbol;
    }

    #endregion


    
    /// <param name="From">[0]Ycords [1]Xcords</param>
    /// <param name="To">[0]Ycords [1]Xcords</param>
    public virtual bool ValidateMove(int[] From,int[] To, List<List<Figure>> _GameState)
    {
        if (From[0] < 8 & From[1] < 8)
        {
            hasMoved = true;
            return true;
        }

        hasMoved = false;
        return false;
    }

    public enum Color
    {
        White,
        Black
    }
    
    public enum ColorShort
    {
        w,
        b
    }
}