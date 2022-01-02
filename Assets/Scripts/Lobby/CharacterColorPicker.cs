using Unity.Netcode;
using UnityEngine;

public class CharacterColorPicker : MonoBehaviour
{
    public void SelectColor(int teamIndex)
    {
        // Get the local client's id
        ulong localClientId = NetworkManager.Singleton.LocalClientId;

        // Try to get the local client object
        // Return if unsuccessful
        if (!NetworkManager.Singleton.ConnectedClients.TryGetValue(localClientId, out NetworkClient networkClient))
        {
            return;
        }

        // Try to get the TeamPlayer component from the player object
        // Return if unsuccessful
        if (!networkClient.PlayerObject.TryGetComponent<PlayerColor>(out var player))
        {
            return;
        }

        // Send a message to the server to set the local client's team
        player.SetTeamServerRpc((byte)teamIndex);
    }
}