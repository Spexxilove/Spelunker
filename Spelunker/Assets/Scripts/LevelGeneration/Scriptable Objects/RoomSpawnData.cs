using Assets.Scripts.Level_Generation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contains Data used to spawn Rooms with same openings
[CreateAssetMenu(fileName = "NewRoomSpawnData", menuName = "ScriptableObjects/LevelGeneration/RoomSpawnData")]
public class RoomSpawnData : ScriptableObject
{
    [Tooltip("Collection of room prefabs with the same opening settings")]
    public GameObject[] RoomPrefabs;

    [Tooltip("Has opening to the left (only use open or closed)")]
    public OpeningState openingLeft = OpeningState.CLOSED;
    [Tooltip("Has opening to the top (only use open or closed)")]
    public OpeningState openingTop = OpeningState.CLOSED;
    [Tooltip("Has opening to the right (only use open or closed)")]
    public OpeningState openingRight = OpeningState.CLOSED;
    [Tooltip("Has opening to the bottom (only use open or closed)")]
    public OpeningState openingBottom = OpeningState.CLOSED;

    internal bool IsCompatible(OpeningState leftCondition, OpeningState topCondition, OpeningState rightCondition, OpeningState bottomCondition)
    {
        // compatible if condition is ANY or equal to SpawnData
        bool compatibleLeft = leftCondition == OpeningState.ANY || leftCondition == this.openingLeft;
        bool compatibleRight = rightCondition == OpeningState.ANY || rightCondition == this.openingRight;
        bool compatibleBottom = bottomCondition == OpeningState.ANY || bottomCondition == this.openingBottom;
        bool compatibleTop = topCondition == OpeningState.ANY || topCondition == this.openingTop;

        return compatibleLeft && compatibleRight && compatibleBottom && compatibleTop;
    }
}

