
using Assets.Scripts.Level_Generation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRoomPool", menuName = "ScriptableObjects/LevelGeneration/RoomPool")]
public class RoomPool : ScriptableObject
{
    [Tooltip("Collection of room spawn data")]
    public RoomSpawnData[] roomSpawnData;

    public GameObject GetRandomRoomWithOpenings(OpeningState left, OpeningState top, OpeningState right, OpeningState bottom, System.Random rng)
    {
        List<GameObject> potentialRooms = new List<GameObject>();

        // get all compatible room variants
        foreach(RoomSpawnData roomdata in roomSpawnData)
        {
            if(roomdata.IsCompatible(left, top, right, bottom))
            {
                potentialRooms.AddRange(roomdata.RoomPrefabs);
            }
        }

        int randomIndex = rng.Next(0, potentialRooms.Count);
        return potentialRooms[randomIndex];
    }

    public GameObject GetRandomRoomWithOpenings(RoomPlanData roomPlanData, System.Random rng)
    {
        return GetRandomRoomWithOpenings(roomPlanData.leftState, roomPlanData.topState, roomPlanData.rightState, roomPlanData.bottomState, rng);
    }
}

