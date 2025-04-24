"use strict";
let board;
let PlayerColor;
let GameCode;


var connection = new signalR.HubConnectionBuilder()
                        .withUrl("/chessGame").build();

document.getElementById("createSessionButton").disabled = true;

connection.on("Transfer", function (key, boardlayout, playercolor) {
    document.getElementById("joinSessionInput").value = key;
    board = JSON.parse(boardlayout)
    GameCode = key;
    PlayerColor = playercolor;
    document.getElementById("createSessionButton").disabled = true;
    document.getElementById("joinSessionButton").disabled = true;
    document.getElementById("joinSessionInput").disabled = true;
    createBoard();
})

connection.on("NextMoveColor", function (color) {
    document.getElementById("next-move").textContent = `${color} Turn`;
})

connection.on("MoveTo", function (From, To) {
    movePiece(From, To);
    clearSelection();
})


document.getElementById("createSessionButton").addEventListener("click",()=>{
    connection.send("CreateSession");
})

document.getElementById("joinSessionButton").addEventListener("click",()=>{
    const inviteToken = document.getElementById("joinSessionInput").value
    connection.send("JoinSession",inviteToken)
})

connection.start().then(function () {
    document.getElementById("createSessionButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});



// Chess pieces Unicode
const pieces = {
    'wP': '♙', 'wR': '♖', 'wN': '♘', 'wB': '♗', 'wQ': '♕', 'wK': '♔',
    'bP': '♟', 'bR': '♜', 'bN': '♞', 'bB': '♝', 'bQ': '♛', 'bK': '♚'
};

// Initial board setup
const initialSetup = [
    ['bR', 'bN', 'bB', 'bQ', 'bK', 'bB', 'bN', 'bR'],
    ['bP', 'bP', 'bP', 'bP', 'bP', 'bP', 'bP', 'bP'],
    ['', '', '', '', '', '', '', ''],
    ['', '', '', '', '', '', '', ''],
    ['', '', '', '', '', '', '', ''],
    ['', '', '', '', '', '', '', ''],
    ['wP', 'wP', 'wP', 'wP', 'wP', 'wP', 'wP', 'wP'],
    ['wR', 'wN', 'wB', 'wQ', 'wK', 'wB', 'wN', 'wR']
];

// Create board representation

// File and rank labels
const files = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'];
const ranks = ['8', '7', '6', '5', '4', '3', '2', '1'];

// DOM elements
const chessboard = document.getElementById('chessboard');
const moveInfo = document.getElementById('moveInfo');

// Game state
let selectedPiece = null;
let selectedPosition = null;

// Create board
function createBoard() {
    chessboard.innerHTML = '';

    for (let rank = 0; rank < 8; rank++) {
        for (let file = 0; file < 8; file++) {
            const square = document.createElement('div');
            square.classList.add('square');
            square.classList.add((rank + file) % 2 === 0 ? 'white' : 'black');

            // Add piece if exists
            const piece = board[rank][file];
            if (piece) {
                square.textContent = pieces[piece];
            }

            // Add coordinates
            if (rank === 7) {
                const fileLabel = document.createElement('span');
                fileLabel.classList.add('coordinates', 'file');
                fileLabel.textContent = files[file];
                square.appendChild(fileLabel);
            }

            if (file === 0) {
                const rankLabel = document.createElement('span');
                rankLabel.classList.add('coordinates', 'rank');
                rankLabel.textContent = ranks[rank];
                square.appendChild(rankLabel);
            }

            // Store position as data attributes
            square.dataset.rank = rank;
            square.dataset.file = file;

            // Add event listener
            square.addEventListener('click', handleSquareClick);

            chessboard.appendChild(square);
        }
    }
}

// Handle square click
function handleSquareClick(event) {
    const square = event.currentTarget;
    const rank = parseInt(square.dataset.rank);
    const file = parseInt(square.dataset.file);
    
    
    if (board[rank][file][0] !== PlayerColor && !selectedPiece){
        console.log("Cannot Select Other Player Figures")
    }
    // If no piece is selected and the clicked square has a piece
    else if (!selectedPiece && board[rank][file]) {
        // Select piece
        selectedPiece = board[rank][file];
        selectedPosition = { rank, file };
        square.classList.add('selected');
    }
    // If a piece is already selected
    else if (selectedPiece) {
        // If clicking the same square, deselect
        if (selectedPosition.rank === rank && selectedPosition.file === file) {
            clearSelection();
        }
        // If clicking a different square, try to move
        else {
            console.log(GameCode,selectedPosition,{ rank, file })
            connection.send("MoveTo", GameCode, 
                Object.values(selectedPosition), 
                Object.values({ rank, file })
            )
            //movePiece(selectedPosition, { rank, file });
            //clearSelection();
        }
    }
}

// Move piece from one position to another
function movePiece(from, to) {
    // Check if the move is valid (simplified for this example)
    if (board[from[0]][from[1]]) {
        // Store the previous piece at destination (if any)
        const capturedPiece = board[to[0]][to[1]];

        // Move the piece
        board[to[0]][to[1]] = board[from[0]][from[1]];
        board[from[0]][from[1]] = '';

        // Update the board display
        createBoard();

        // Display the move information
        displayMoveInfo(from, to, capturedPiece);
    }
}

// Display move information in the required format
function displayMoveInfo(from, to, capturedPiece) {
    const moveText = `[{${from[1]},${from[0]}},{${to[1]},${to[0]}}]`;

    // Add additional information about the move
    const fromSquare = `${files[from[1]]}${ranks[from[0]]}`;
    const toSquare = `${files[to[1]]}${ranks[to[0]]}`;
    const piece = selectedPiece;

    let moveDescription = `${moveText}<br><br>`;
    moveDescription += `Moved ${getPieceName(piece)} from ${fromSquare} to ${toSquare}`;

    if (capturedPiece) {
        moveDescription += `<br>Captured ${getPieceName(capturedPiece)}`;
    }
    
    moveInfo.innerHTML = moveDescription;
}

// Get piece name from piece code
function getPieceName(pieceCode) {
    const color = pieceCode[0] === 'w' ? 'White' : 'Black';
    const pieceType = pieceCode[1];

    const pieceNames = {
        'P': 'Pawn',
        'R': 'Rook',
        'N': 'Knight',
        'B': 'Bishop',
        'Q': 'Queen',
        'K': 'King'
    };

    return `${color} ${pieceNames[pieceType]}`;
}

// Clear selection
function clearSelection() {
    selectedPiece = null;
    selectedPosition = null;

    // Remove selected class from all squares
    document.querySelectorAll('.square').forEach(square => {
        square.classList.remove('selected');
        square.classList.remove('valid-move');
    });
}

// Initialize the board
