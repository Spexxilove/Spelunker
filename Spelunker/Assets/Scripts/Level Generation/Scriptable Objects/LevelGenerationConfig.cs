using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelGenerationConfig", menuName = "ScriptableObjects/LevelGeneration/LevelGenerationConfig")]
public class LevelGenerationConfig : ScriptableObject
{
    public GameObject boundryTile;

    // size of level in x direction (horizontal)
    public int xDimension = 5;

    // size of level in y direction (vertical)
    public int yDimension = 5;

    // room pools named by opening in direction
    public RoomPool leftRoomPool;
    public RoomPool leftBottomRoomPool;
    public RoomPool leftRightRoomPool;
    public RoomPool leftRightBottomRoomPool;
    public RoomPool leftTopRoomPool;
    public RoomPool leftTopRightRoomPool;
    public RoomPool rightRoomPool;
    public RoomPool rightBottomRoomPool;
    public RoomPool topRoomPool;
    public RoomPool topRightRoomPool;

    // start room prefab on top floor
    public GameObject startRoom;

    // goal room prefab on bottom floor
    public GameObject goalRoom;


    // width and height of a tile in unity units
    public readonly float tileSideLength = 1;

    // width of a room in tiles
    public readonly int roomWidth = 10;

    // height of a room in tiles
    public readonly int roomHeight = 8;

}
