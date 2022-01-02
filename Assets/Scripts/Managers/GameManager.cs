using Unity.Netcode;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private NetworkSpawner networkSpawner;

    void Start()
    {
        ulong id = NetworkManager.Singleton.LocalClientId;
        networkSpawner.SpawnPlayerServerRpc(id);
    }

    private void Update()
    {
        //Debug.Log("Players in scene " + networkSpawner.playersInScene.Count);   
    }
}