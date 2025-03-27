using System.Text.Json;
using ChessWebGame;
using ChessWebGame.HelperClasses;
using Microsoft.AspNetCore.SignalR;
namespace ChessWebGame.Hubs;


public class ChessGame : Hub
{
    static private Dictionary<string,Engine> _sessions = new Dictionary<string,Engine>();
    
    
    
    public async Task CreateSession()
    {
        string key = Guid.NewGuid().ToString()[..8];
        var engine = new Engine { GameKey = key };
        string boardLayout = JsonSerializer.Serialize(engine.GetBoardLayout());
        
        // Add Session To Pool
        _sessions.Add(key,engine);
        
        
        await Groups.AddToGroupAsync(Context.ConnectionId, key);
        await Clients.Client(Context.ConnectionId).SendAsync("Transfer", key,boardLayout,FigureColor.White.ToShortName());
    }

    public async Task JoinSession(string inviteKey)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, inviteKey);
        string boardLayout = JsonSerializer.Serialize(_sessions[inviteKey].GetBoardLayout());
        await Clients.Client(Context.ConnectionId)
            .SendAsync("Transfer", inviteKey, boardLayout,FigureColor.Black.ToShortName());
    }

    public async Task MoveTo(string inviteKey,int[] From,int[] To)
    {
        Engine CurrentSession = _sessions[inviteKey];
        if (CurrentSession.MoveTo(From, To))
        {
            await Clients.Group(inviteKey).SendAsync("MoveTo",From,To);
        }
    }
}