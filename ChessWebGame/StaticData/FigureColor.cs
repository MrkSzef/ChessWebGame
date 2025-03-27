using System.ComponentModel.DataAnnotations;

namespace ChessWebGame;

public enum FigureColor
{
    [Display(ShortName = "w")]
    White = 0,
    [Display(ShortName = "b")]
    Black = 1,
    None = 2
}