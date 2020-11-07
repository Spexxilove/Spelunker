using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelGenerationConfig", menuName = "ScriptableObjects/LevelGeneration/LevelGenerationConfig")]
public class LevelGenerationConfig : ScriptableObject
{
    public GameObject boundryTile;

    [Tooltip("size of level in x direction in rooms (horizontal)")] 
    public int xDimension = 5;

    [Tooltip("size of level in y direction in rooms (vertical)")]
    public int yDimension = 5;

    [Tooltip("Scripable object containing all room data for level generation")]
    public RoomPool GeneralRoomPool;

    [Tooltip("start room prefab on top floor")]
    public GameObject startRoom;

    [Tooltip("goal room prefab on bottom floor")]
    public GameObject goalRoom;

    // not exposed values --------------------------------

    // width and height of a tile in unity units
    public readonly float tileSideLength = 1;

    // width of a room in tiles
    public readonly int roomWidth = 10;

    // height of a room in tiles
    public readonly int roomHeight = 8;

}
