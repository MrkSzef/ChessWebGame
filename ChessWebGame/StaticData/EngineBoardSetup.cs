using ChessWebGame.Figures;

namespace ChessWebGame;

public partial class Engine
{
    private List<List<Figure>> _GameField = 
[
    // Row 1 (Black Major Pieces)
    [ new Rook(){position = [1,1], FigureColor = (int)Figure.Color.Black},
      new Knight(){position = [1,2], FigureColor = (int)Figure.Color.Black},
      new Bishop(){position = [1,3], FigureColor = (int)Figure.Color.Black},
      new Queen(){position = [1,4], FigureColor = (int)Figure.Color.Black},
      new King(){position = [1,5], FigureColor = (int)Figure.Color.Black},
      new Bishop(){position = [1,6], FigureColor = (int)Figure.Color.Black},
      new Knight(){position = [1,7], FigureColor = (int)Figure.Color.Black},
      new Rook(){position = [1,8], FigureColor = (int)Figure.Color.Black} 
    ],
    
    // Row 2 (Black Pawns)
    [ new Pawn(){position = [2,1], FigureColor = (int)Figure.Color.Black},
      new Pawn(){position = [2,2], FigureColor = (int)Figure.Color.Black},
      new Pawn(){position = [2,3], FigureColor = (int)Figure.Color.Black},
      new Pawn(){position = [2,4], FigureColor = (int)Figure.Color.Black},
      new Pawn(){position = [2,5], FigureColor = (int)Figure.Color.Black},
      new Pawn(){position = [2,6], FigureColor = (int)Figure.Color.Black},
      new Pawn(){position = [2,7], FigureColor = (int)Figure.Color.Black},
      new Pawn(){position = [2,8], FigureColor = (int)Figure.Color.Black}
    ],

    // Rows 3-6 (Empty Squares)
    [ new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty()],
    [ new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty()],
    [ new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty()],
    [ new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty(), new Empty()],
    
    // Row 7 (White Pawns)
    [ new Pawn(){position = [7,1], FigureColor = (int)Figure.Color.White},
      new Pawn(){position = [7,2], FigureColor = (int)Figure.Color.White},
      new Pawn(){position = [7,3], FigureColor = (int)Figure.Color.White},
      new Pawn(){position = [7,4], FigureColor = (int)Figure.Color.White},
      new Pawn(){position = [7,5], FigureColor = (int)Figure.Color.White},
      new Pawn(){position = [7,6], FigureColor = (int)Figure.Color.White},
      new Pawn(){position = [7,7], FigureColor = (int)Figure.Color.White},
      new Pawn(){position = [7,8], FigureColor = (int)Figure.Color.White}
    ],

    // Row 8 (White Major Pieces)
    [ new Rook(){position = [8,1], FigureColor = (int)Figure.Color.White},
      new Knight(){position = [8,2], FigureColor = (int)Figure.Color.White},
      new Bishop(){position = [8,3], FigureColor = (int)Figure.Color.White},
      new Queen(){position = [8,4], FigureColor = (int)Figure.Color.White},
      new King(){position = [8,5], FigureColor = (int)Figure.Color.White},
      new Bishop(){position = [8,6], FigureColor = (int)Figure.Color.White},
      new Knight(){position = [8,7], FigureColor = (int)Figure.Color.White},
      new Rook(){position = [8,8], FigureColor = (int)Figure.Color.White} 
    ]
];

}