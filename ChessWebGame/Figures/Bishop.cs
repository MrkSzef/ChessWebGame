﻿namespace ChessWebGame.Figures;

public class Bishop : Figure
{
    public Bishop() : base(FigureSymbol.Bishop){}

    public override bool ValidateMove(int[] From, int[] To, List<List<Figure>> _GameState)
    {
        int YDIFF = Math.Abs(From[0] - To[0]);
        int XDIFF = Math.Abs(From[1] - To[1]);
        
        return (YDIFF == XDIFF);
    }
}