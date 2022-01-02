using Unity.Netcode;
public struct PlayerData : INetworkSerializable
{
    public string PlayerName { get; private set; }
    public ulong ClientId { get; private set; }
    public int ColorNum { get; set; }

    public PlayerData(string playerName, ulong clientId, int colorNum = 0)
    {
        PlayerName = playerName;
        ClientId = clientId;
        ColorNum = colorNum;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        string tempName = PlayerName;
        serializer.SerializeValue(ref tempName);
        PlayerName = tempName;

        ulong tempId = ClientId;
        serializer.SerializeValue(ref tempId);
        ClientId = tempId;

        int tempColorNum = ColorNum;
        serializer.SerializeValue(ref tempColorNum);
        ColorNum = tempColorNum;
    }
}