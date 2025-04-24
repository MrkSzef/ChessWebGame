namespace ChessWebGame.HelperClasses;

public class Player
{
    public string PlayerId { get; set; } = string.Empty;
    public FigureColor PlayerColor { get; init; }
    public bool IsConnected { get; set; }

    public void Connect(string playerId)
    {
        if (IsConnected) return;
        PlayerId = playerId;
        IsConnected = true;
    }
    
    public void Disconnect(string playerId)
    {
        if (!IsConnected || PlayerId != playerId) return;
        PlayerId = string.Empty;
        IsConnected = false;
    }
}