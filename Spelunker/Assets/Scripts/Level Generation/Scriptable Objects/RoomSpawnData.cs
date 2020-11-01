using Assets.Scripts.Level_Generation;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contains Data used to spawn Room
[CreateAssetMenu(fileName = "NewRoomSpawnData", menuName = "ScriptableObjects/LevelGeneration/RoomSpawnData")]
public class RoomSpawnData : ScriptableObject
{
    public GameObject RoomPrefab;

    //openings settings. should only use OPEN and CLOSED here
    public OpeningState openingLeft = OpeningState.CLOSED;
    public OpeningState openingTop = OpeningState.CLOSED;
    public OpeningState openingRight = OpeningState.CLOSED;
    public OpeningState openingBottom = OpeningState.CLOSED;


    internal bool IsCompatible(OpeningState left, OpeningState top, OpeningState right, OpeningState bottom)
    {
        // compatible if condition is ANY or equal to SpawnData
        bool compatibleLeft = left == OpeningState.ANY || left == this.openingLeft;
        bool compatibleRight = right == OpeningState.ANY || right == this.openingRight;
        bool compatibleBottom = bottom == OpeningState.ANY || bottom == this.openingBottom;
        bool compatibleTop = top == OpeningState.ANY || top == this.openingTop;

        return compatibleLeft && compatibleRight && compatibleBottom && compatibleTop;
    }
}

