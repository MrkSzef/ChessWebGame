using ChessConsoleGame;
using ChessConsoleGame.Figures;
using Microsoft.AspNetCore.SignalR;
namespace ChessWebGame.Hubs;
using Microsoft.AspNetCore.Authorization;


public class ChessGame : Hub
{
    static private Dictionary<string,Engine> _sessions = new Dictionary<string,Engine>();
    
    
    
    public async Task CreateSession()
    {
        Console.WriteLine(Context.User.Identity.Name);
        string key = Guid.NewGuid().ToString();
        var engine = new Engine { _GameKey = key };
        _sessions.Add(key,engine);
        
        foreach (KeyValuePair<string, Engine> entry in _sessions)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }
        await Groups.AddToGroupAsync(Context.ConnectionId, key);
        await Clients.Client(Context.ConnectionId).SendAsync("Transfer", key,engine.GetBoardLayout(),"w");
    }

    public async Task JoinSession(string inviteKey)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, inviteKey);
        foreach (KeyValuePair<string, Engine> entry in _sessions)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }
        await Clients.Client(Context.ConnectionId)
            .SendAsync("Transfer", inviteKey, _sessions[inviteKey].GetBoardLayout(),"b");
    }

    public async Task MoveTo(string inviteKey,int[] From,int[] To)
    {
        Engine CurrentSession = _sessions[inviteKey];
        
        Console.WriteLine($"{CurrentSession}\n{From}\n{To}\n{inviteKey}");
        if (CurrentSession.MoveTo(From, To))
        {
            await Clients.Group(inviteKey).SendAsync("MoveTo",From,To);
        }
    }
    /*
    public async Task MoveTo(int sessionId,int[] From,int[] To)
    {
        Engine CurrentGame = _sessions[sessionId];
        CurrentGame.MoveTo(From,To);
    }
    */
}