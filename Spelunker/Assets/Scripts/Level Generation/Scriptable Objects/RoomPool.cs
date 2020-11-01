using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRoomPool", menuName = "ScriptableObjects/LevelGeneration/RoomPool")]
public class RoomPool : ScriptableObject
{
    public GameObject[] RoomPrefabs;
}
