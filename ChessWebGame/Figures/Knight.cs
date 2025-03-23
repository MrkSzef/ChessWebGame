﻿namespace ChessConsoleGame.Figures;

public class Knight : Figure
{
    public Knight() : base("N"){}

    public override bool ValidateMove(int[] From, int[] To, List<List<Figure>> _GameState)
    {
        int YDIFF = Math.Abs(From[0] - To[0]);
        int XDIFF = Math.Abs(From[1] - To[1]);
        
        return (YDIFF < 3 && XDIFF < 3 && XDIFF + YDIFF == 3);
    }
}