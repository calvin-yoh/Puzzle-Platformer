using Unity.Netcode;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject _playerPrefab = null;
    [SerializeField] private Transform[] spawnLocations;
    [SerializeField] private MultipleTargetCamera mainCam;
    [SerializeField] private Sprite[] allSprites; 

    //Doesn't compile with new version : May not need for now
    //public NetworkDictionary<ulong, GameObject> playersInScene = new NetworkDictionary<ulong, GameObject>();

    #region Stuff that don't work and is commented out
    /*
    private void OnEnable()
    {
        playersInScene.OnDictionaryChanged += playersInSceneListChanged;
    }

    private void OnDisable()
    {
        playersInScene.OnDictionaryChanged -= playersInSceneListChanged;
    }

    private void playersInSceneListChanged(NetworkDictionaryEvent<ulong, GameObject> playerChange)
    {
        foreach (KeyValuePair<ulong, GameObject> kvp in playersInScene)
        {
            PlayerData? playerData = ServerGameNetPortal.Instance.GetPlayerData(kvp.Key);
            if (playerData != null)
            {
                kvp.Value.GetComponent<PlayerUI>().UpdateSprite(playerData.Value.ColorNum);
            }
        }
    }
    */
    #endregion

    private Transform GetSpawnPoint(ulong clientId)
    {
        // Stop if there are no spawn points in the seen
        if (spawnLocations.Length == 0) { return null; }

        // get number of players
        /*
        if (NetworkManager.Singleton.IsHost)
        {
            Debug.Log("Using host spawn point");
            return spawnLocations[0];
        }
        else
        {
            int count = (int)(clientId - 1);
            Debug.Log("Using client spawn point");
            return spawnLocations[count];
        }
        */

        //Get correct spawn locations
        if ((int)clientId == 0 || (int)clientId == 1)
        {
            return spawnLocations[0];
        }
        else
        {
            int count = (int)(clientId - 1);
            return spawnLocations[count];
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void SpawnPlayerServerRpc(ulong clientId)
    {
        // Get Spawn.  Stop if there are no spawn points in the seen
        Transform spawn = GetSpawnPoint(clientId);
        if (spawn == null) { Debug.Log("No Spawn Points in Scene!"); return; }

        //Instantiates the gameObject on the server
        GameObject go = Instantiate(_playerPrefab, spawn.position, spawn.rotation);
        //Change color before spawning on client
        //go.GetComponent<SpriteRenderer>().sprite = allSprites[playerData.Value.ColorNum];
        //Spawns on all clients
        go.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
        //playersInScene.Add(clientId, go);
        ulong objectId = go.GetComponent<NetworkObject>().NetworkObjectId;

        PlayerData? playerData = ServerGameNetPortal.Instance.GetPlayerData(clientId);
        if (IsServer)
        {
            Debug.Log("Setting Sprites");
            go.GetComponent<SpriteRenderer>().sprite = allSprites[playerData.Value.ColorNum];
        }
        
        mainCam.targets.Add(go.transform);

        SpawnClientRpc(objectId, playerData.Value.ColorNum);
    }

    // A ClientRpc can be invoked by the server to be executed on a client
    [ClientRpc]
    private void SpawnClientRpc(ulong objectId, int colorNum)
    {
        NetworkObject player = NetworkManager.Singleton.SpawnManager.SpawnedObjects[objectId];

        player.gameObject.GetComponent<SpriteRenderer>().sprite = allSprites[colorNum];

        /*
        foreach (KeyValuePair<ulong, NetworkObject> kvp in NetworkSpawnManager.SpawnedObjects)
        {
            var temp = kvp.Value;
            player.gameObject.GetComponent<SpriteRenderer>().sprite = allSprites[colorNum];
        }
        */
        
        /*
        if (IsClient)
        {
            foreach (KeyValuePair<ulong, NetworkClient> x in NetworkManager.Singleton.ConnectedClients)
            {
                var playerObject = x.Value.PlayerObject;

                if (playerObject != null)
                {
                    if (playerObject.gameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer spriteRenderer))
                    {
                        spriteRenderer.sprite = allSprites[colorNum];
                    }
                }
            }
        }
        */


        /*
        foreach (KeyValuePair<ulong, NetworkObject> x in NetworkSpawnManager.SpawnedObjects)
        {
            if (x.Value.tag == "Player")
            {
                if (x.Value.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidBody))
                {
                    if(!IsLocalPlayer)
                    {
                        rigidBody.isKinematic = true;
                    }
                }
            }
        }
        */
    }
}