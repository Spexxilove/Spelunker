using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelGenerationConfig", menuName = "ScriptableObjects/LevvelGeneration/LevelGenerationConfig")]
public class LevelGenerationConfig : ScriptableObject
{
    public GameObject boundryTile;

    // size of level in x direction (horizontal)
    public int xDimension = 5;

    // size of level in y direction (vertical)
    public int yDimension = 5;

    // width and height of a tile in unity units
    public readonly float tileSideLength = 1;

    // width of a room in tiles
    public readonly int roomWidth = 10;

    // height of a room in tiles
    public readonly int roomHeight = 8;
}
