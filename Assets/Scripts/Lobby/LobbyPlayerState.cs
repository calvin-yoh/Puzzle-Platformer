
using Unity.Netcode;
using Unity.Collections;
using System;

public struct LobbyPlayerState : INetworkSerializable, IEquatable<LobbyPlayerState>
{
    public ulong ClientId;
    public FixedString32Bytes PlayerName;
    public bool IsReady;
    public int CharacterImageNum;

    public LobbyPlayerState(ulong clientId, FixedString32Bytes playerName, bool isReady, int characterImageNum = 0)
    {
        ClientId = clientId;
        PlayerName = playerName;
        IsReady = isReady;
        CharacterImageNum = characterImageNum;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref ClientId);
        serializer.SerializeValue(ref PlayerName);
        serializer.SerializeValue(ref IsReady);
        serializer.SerializeValue(ref CharacterImageNum);
    }

    public bool Equals(LobbyPlayerState other)
    {
        return ClientId == other.ClientId &&
               PlayerName.Equals(other.PlayerName) &&
               IsReady == other.IsReady &&
               CharacterImageNum == other.CharacterImageNum;
    }
}