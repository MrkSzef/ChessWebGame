using System.ComponentModel.DataAnnotations;

namespace ChessWebGame;

public enum FigureSymbol
{
    [Display(ShortName = "E")]
    Empty = 0,
    [Display(ShortName = "P")]
    Pawn = 1,
    [Display(ShortName = "R")]
    Rook = 2,
    [Display(ShortName = "N")]
    Knight = 3,
    [Display(ShortName = "B")]
    Bishop = 4,
    [Display(ShortName = "Q")]
    Queen = 5,
    [Display(ShortName = "K")]
    King = 6
}