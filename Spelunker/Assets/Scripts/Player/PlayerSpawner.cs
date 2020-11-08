using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private void Start()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        GameObject[] spawnedPlayers = GameObject.FindGameObjectsWithTag("Player");
        GameObject spawnedPlayer;

        //check if there is already a player otherwise spawn one
        if (spawnedPlayers.Length > 0)
        {
            spawnedPlayer = spawnedPlayers[0];
        }
        else
        {
            spawnedPlayer = Instantiate(player);
        }

        spawnedPlayer.transform.position = this.transform.position;
    }
}
