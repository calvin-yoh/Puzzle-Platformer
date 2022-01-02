using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class PlayerColor : NetworkBehaviour
{
    [SerializeField] private Sprite[] characterSprites;

    [SerializeField] private Image playerImage;

    private NetworkVariable<byte> colorIndex = new NetworkVariable<byte>();

    [ServerRpc]
    public void SetTeamServerRpc(byte newColorIndex)
    {
        // Make sure the newTeamIndex being received is valid
        if (newColorIndex > 7) { return; }

        // Update the teamIndex NetworkVariable
        colorIndex.Value = newColorIndex;
    }

    private void OnEnable()
    {
        // Start listening for the team index being updated
        colorIndex.OnValueChanged += OnTeamChanged;
    }

    private void OnDisable()
    {
        // Stop listening for the team index being updated
        colorIndex.OnValueChanged -= OnTeamChanged;
    }

    private void OnTeamChanged(byte oldTeamIndex, byte newTeamIndex)
    {
        // Only clients need to update the renderer
        if (!IsClient) { return; }

        // Update the colour of the player's mesh renderer
        playerImage.sprite = characterSprites[0];
    }
}
