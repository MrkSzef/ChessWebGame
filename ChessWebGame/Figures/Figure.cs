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
    
    public bool CheckCollision(int[] From, int[] To, List<List<Figure>> _GameState)
    {
        int[] lowerBand = new []{0,0}; // Identifier for first element
        
        int YDIFF = Math.Abs(From[0] - To[0]);
        int XDIFF = Math.Abs(From[1] - To[1]);
        List < List<int> > cords = [];
        
        Console.WriteLine(To[0] + To[1]);
        Console.WriteLine(From[0] + From[1]);
        /*
         * Block for checking ONLY on one dimension
         */
        if (To[0] == From[0])
        {
            lowerBand[0] = lowerBand[1] = To[1] < From[1] ? To[1] : From[1];
        }
        else if (To[1] == From[1])
        {
            lowerBand[0] = lowerBand[1] = To[0] < From[0] ? To[0] : From[0];
        }
        else if (To[0] - From[0] == To[1] - From[1])
        {
            lowerBand[0] = To[0] < From[0] ? To[0] : From[0];
            lowerBand[1] = To[1] < From[1] ? To[1] : From[1];
        }
        else if (To[0] + To[1] == From[0] + From[1])
        {
            lowerBand[0] = To[0] > From[0] ? To[0] : From[0];
            lowerBand[1] = To[1] < From[1] ? To[1] : From[1];
        }

        if (To[0] == From[0])
        {
            for (int i = 1; i < XDIFF + YDIFF; i++)
            {
                if (_GameState[From[0]][lowerBand[1] + i].GetType() != typeof(Empty))
                {
                    return false;
                }
                List<int> _temp = new List<int>([From[0], lowerBand[1] + i]);
                cords.Add(_temp);
            }
        }
        else if (To[1] == From[1])
        {
            for (int i = 1; i < XDIFF + YDIFF; i++)
            {
                if (_GameState[lowerBand[1] + i][From[1]].GetType() != typeof(Empty))
                {
                    return false;
                }
            }
        }
        else if (To[0] - From[0] == To[1] - From[1])
        {
            Console.WriteLine($"lowerBand {lowerBand[0]} {lowerBand[1]}");
            for (int i = 1; i < (XDIFF+YDIFF)/2; i++)
            {
                if (_GameState[lowerBand[0] + i][lowerBand[1] + i].GetType() != typeof(Empty))
                {
                    return false;
                }
                List<int> _temp = new List<int>([lowerBand[0] + i, lowerBand[1] + i]);
                cords.Add(_temp);
            }
        }
        else if (To[0] + To[1] == From[0] + From[1])
        {
            Console.WriteLine($"lowerBand {lowerBand[0]} {lowerBand[1]}");
            for (int i = 1; i < (XDIFF+YDIFF)/2; i++)
            {
                if (_GameState[lowerBand[0] - i][lowerBand[1] + i].GetType() != typeof(Empty))
                {
                    return false;
                }
                
                List<int> _temp = new List<int>([lowerBand[0] - i, lowerBand[1] + i]);
                cords.Add(_temp);
            }
        }

        foreach (List<int> row in cords)
        {
            Console.WriteLine(row);
            Console.WriteLine($"{row[0]} - {row[1]}");
        }
        return true; 
    }
}