using TMPro;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UNET;

public class MainMenuUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_InputField displayNameInputField;
    [SerializeField] private TMP_InputField ipAddressInputField;
    [SerializeField] private TMP_InputField portInputField;

    private UNetTransport gameUNet;

    private void Start()
    {
        PlayerPrefs.GetString("PlayerName");

        gameUNet = NetworkManager.Singleton.GetComponent<UNetTransport>();
    }

    public void OnHostClicked()
    {
        PlayerPrefs.SetString("PlayerName", displayNameInputField.text);

        if (!(portInputField.text.Length <= 0))
        {
            int.TryParse(portInputField.text, out int result); gameUNet.ServerListenPort = result;
        }

        GameNetPortal.Instance.StartHost();
    }

    public void OnClientClicked()
    {
        PlayerPrefs.SetString("PlayerName", displayNameInputField.text);

        if (ipAddressInputField.text.Length <= 0)
        {
            gameUNet.ConnectAddress = "127.0.0.1";
        }
        else
        {
            gameUNet.ConnectAddress = ipAddressInputField.text;         
        }

        if (portInputField.text.Length <= 0)
        {
            gameUNet.ConnectPort = 7777;
        }
        else 
        {
            int.TryParse(portInputField.text, out int result); gameUNet.ConnectPort = result;
        }

        ClientGameNetPortal.Instance.StartClient();

    }
}