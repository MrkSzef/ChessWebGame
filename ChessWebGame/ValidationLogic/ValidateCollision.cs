using ChessWebGame.Figures;

namespace ChessWebGame.ValidationLogic;

public partial class Validate
{
    public static bool ValidateCollision(int[] From, int[] To, List<List<Figure>> _GameState)
    {
        int[] lowerBand = new []{0,0}; // Identifier for first element
        
        int yDiff = Math.Abs(From[0] - To[0]);
        int xDiff = Math.Abs(From[1] - To[1]);
        List < List<int> > cords = [];
        
        
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
            for (int i = 1; i < xDiff + yDiff; i++)
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
            for (int i = 1; i < xDiff + yDiff; i++)
            {
                if (_GameState[lowerBand[1] + i][From[1]].GetType() != typeof(Empty))
                {
                    return false;
                }
            }
        }
        else if (To[0] - From[0] == To[1] - From[1])
        {
            for (int i = 1; i < (xDiff+yDiff)/2; i++)
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
            for (int i = 1; i < (xDiff+yDiff)/2; i++)
            {
                if (_GameState[lowerBand[0] - i][lowerBand[1] + i].GetType() != typeof(Empty))
                {
                    return false;
                }
                
                List<int> _temp = new List<int>([lowerBand[0] - i, lowerBand[1] + i]);
                cords.Add(_temp);
            }
        }
        
        return true; 
    }
}