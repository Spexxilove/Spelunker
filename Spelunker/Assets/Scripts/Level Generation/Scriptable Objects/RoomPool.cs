
using Assets.Scripts.Level_Generation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRoomPool", menuName = "ScriptableObjects/LevelGeneration/RoomPool")]
public class RoomPool : ScriptableObject
{
    public RoomSpawnData[] roomSpawnData;

    public GameObject GetRandomRoomWithOpenings(OpeningState left, OpeningState top, OpeningState right, OpeningState bottom, System.Random rng)
    {
        //TODO: Replace this list with different type
        ArrayList potentialRooms = new ArrayList();

        foreach(RoomSpawnData roomdata in roomSpawnData)
        {
            if(roomdata.IsCompatible(left, top, right, bottom))
            {
                potentialRooms.AddRange(roomdata.RoomPrefabs);
            }

        }

        int randomIndex = rng.Next(0, potentialRooms.Count);
        return ((GameObject) potentialRooms[randomIndex]);
    }

    public GameObject GetRandomRoomWithOpenings(RoomPlanData roomPlanData, System.Random rng)
    {
        return GetRandomRoomWithOpenings(roomPlanData.leftState, roomPlanData.topState, roomPlanData.rightState, roomPlanData.bottomState, rng);
    }
}

