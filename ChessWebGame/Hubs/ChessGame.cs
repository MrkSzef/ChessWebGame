using System.Text.Json;
using ChessWebGame;
using ChessWebGame.HelperClasses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.SignalR;
namespace ChessWebGame.Hubs;


public class ChessGame : Hub
{
    private static Dictionary<string,Engine> _sessions = new Dictionary<string,Engine>();
    private static Dictionary<string,string> _sessionKeysTies = new Dictionary<string,string>();
    
    
    public async Task CreateSession()
    {
        string key = Guid.NewGuid().ToString()[..8];
        var engine = new Engine { GameKey = key };
        string boardLayout = JsonSerializer.Serialize(engine.GetBoardLayout());
        _sessionKeysTies[Context.ConnectionId] = key; 
        
        // Add Player to Session 
        engine.WhitePlayer.Connect(Context.ConnectionId);
        
        
        // Add Session To Pool
        _sessions.Add(key,engine);
        
        
        await Groups.AddToGroupAsync(Context.ConnectionId, key);
        await Clients.Client(Context.ConnectionId).SendAsync("Transfer", key,boardLayout,FigureColor.White.ToShortName());
    }

    
    // Function Needs Refactoring
    public async Task JoinSession(string inviteKey)
    {
        // Tie Key To Session
        _sessionKeysTies[Context.ConnectionId] = inviteKey; 
        
        // Check if session exist
        if (_sessions.TryGetValue(inviteKey, out var currentSession))
        {
            // Add User To Session
            await Groups.AddToGroupAsync(Context.ConnectionId, inviteKey);
            string nextMoveColor = currentSession.NextMoveColor == 0 ? "White" : "Black";
            string playerStatus = "Spectator";
            
            // Set Player Status [Figure Color OR Spectator]
            Console.WriteLine(currentSession.IsGameStarted());
            if (!currentSession.IsGameStarted())
            {
                Console.WriteLine(currentSession.WhitePlayer.IsConnected);
                if (!currentSession.WhitePlayer.IsConnected)
                {
                    currentSession.WhitePlayer.Connect(Context.ConnectionId);
                    playerStatus = FigureColor.White.ToShortName();
                }
                else if(!currentSession.BlackPlayer.IsConnected)
                {
                    currentSession.BlackPlayer.Connect(Context.ConnectionId);
                    playerStatus = FigureColor.Black.ToShortName();
                }

            }
            Console.WriteLine(playerStatus);
            
            // Send Data To Clients
            string boardLayout = JsonSerializer.Serialize(currentSession.GetBoardLayout());
            await Clients.Client(Context.ConnectionId)
                .SendAsync("Transfer", inviteKey, boardLayout,playerStatus);
            
            await Clients.Group(inviteKey).SendAsync("NextMoveColor", nextMoveColor);
            
        }

    }

    public async Task MoveTo(string inviteKey,int[] From,int[] To)
    {
        Engine currentSession = _sessions[inviteKey];
        string nextMoveColor = currentSession.NextMoveColor == 0 ? "Black" : "White";
        if (currentSession.MoveTo(From, To))
        {
            await Clients.Group(inviteKey).SendAsync("MoveTo",From,To);
            await Clients.Group(inviteKey).SendAsync("NextMoveColor", nextMoveColor);
        }
    }

    
    
    // Test
    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var currentSession = _sessions[_sessionKeysTies[Context.ConnectionId]];
        currentSession.WhitePlayer.Disconnect(Context.ConnectionId);
        currentSession.BlackPlayer.Disconnect(Context.ConnectionId);
        
        Console.WriteLine($"{Context.ConnectionId} Disconnected");
        return base.OnDisconnectedAsync(exception);
    }

    public override Task OnConnectedAsync()
    {
        Console.WriteLine($"{Context.ConnectionId} Connected");
        return base.OnConnectedAsync();
    }
}