﻿using System.Runtime.Intrinsics.X86;
using ChessWebGame.Figures;
using System.Text.Json;

namespace ChessWebGame;

public partial class Engine
{
    public string _GameKey { init; get; }
    public string _WhiteUser { init; get; }
    public string _BlackUser { set; get; }
    private int _NextMoveColor = 0;

    public void GetAllPositionsAndNames()
    {
        foreach (var row in _GameField)
        {
            foreach (Figure fig in row)
            {
                Console.Write(string.Join(" ",fig.Symbol));
                Console.Write(" ");
            }
            Console.Write("\n");
        }
    }

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

        if (!(_NextMoveColor == SelectedFigure.FigureColor))
        {
            Console.WriteLine($"Not Your Turn {SelectedFigure.FigureColor}");
            return false;
        }


        // Basic Logic Checking
        if (From[0].Equals(To[0]) & From[1].Equals(To[1]))
        {
            Console.WriteLine("Move aint moving");
            return false;
        }
        if (From.Length != 2 & To.Length != 2)
        {
            Console.WriteLine("Invalid Move 1");
            return false;
        }
        if (From[0] > 7 & From[1] > 7 & To[0] > 7 & To[1] > 7)
        {
            Console.WriteLine("Invalid Move 2");
            return false;
        }
        if (SelectedFigure.GetType() == typeof(Empty))
        {
            Console.WriteLine("Invalid Move 3");
            return false;
        }
        if (!SelectedFigure.ValidateMove(From,To,_GameField))
        {
            Console.WriteLine("Invalid Move 4");
            return false;
        }
        if (!SelectedFigure.CheckCollision(From, To, _GameField) && SelectedFigure.GetType() != typeof(Knight))
        {
            Console.WriteLine("There is other figure in path");
            return false;
        }
        if (SelectedFigure.FigureColor == _GameField[To[0]][To[1]].FigureColor)
        {
            Console.WriteLine("Cannot Capture Figure with the same color ERR5");
            return false;
        }
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