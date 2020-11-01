using Assets.Scripts.Level_Generation;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private IntVariable seed;

    [SerializeField]
    private LevelGenerationConfig levelGenerationConfig;

    // just for testing
    public GameObject defaultRoomPrefab;

    // using system random to have a seperate instance for safety
    private System.Random rng;

    // [0,0] is the bottom left room
    private RoomPlanData[,] levelPlan;


    void Start()
    {
        InitRandomNumberGenerator();
        InitLevelPlan();
        PlanLevelLayout();
        SpawnRooms();
        FillInFloorTiles();
        SpawnBounds();
    }

    private void PlanLevelLayout()
    {
        for (int x = 0; x < this.levelGenerationConfig.xDimension; x++)
        {
            for (int y = 0; y < this.levelGenerationConfig.yDimension; y++)
            {
                // for testing
                this.levelPlan[x, y].roomPrefab = this.defaultRoomPrefab;
            }
        }
    }

    private void SpawnRooms()
    {
        for (int x = 0; x < this.levelGenerationConfig.xDimension; x++)
        {
            for (int y = 0; y < this.levelGenerationConfig.yDimension; y++)
            {
                Vector3 position = GetRoomCoordinates(x, y);
                Instantiate(this.levelPlan[x, y].roomPrefab, position, Quaternion.identity);
            }
        }
    }

    private void InitLevelPlan()
    {
        this.levelPlan = new RoomPlanData[this.levelGenerationConfig.xDimension, this.levelGenerationConfig.yDimension];
        
        //fill initial level plan
        for(int x =0; x < this.levelGenerationConfig.xDimension; x++)
        {
            for (int y = 0; y < this.levelGenerationConfig.yDimension; y++)
            {
                this.levelPlan[x, y] = new RoomPlanData(x, y);
            }
        }
    }

    //spawn level surrounding tiles
    private void SpawnBounds()
    {
        // bottom left room position
        Vector3 bottomLeftPosition = GetRoomCoordinates(0, 0);

        // get position bottom left outside of level bounds
        float xOffset = (this.levelGenerationConfig.roomWidth / 2f + 0.5f) * this.levelGenerationConfig.tileSideLength;
        float yOffset = (this.levelGenerationConfig.roomHeight / 2f + 0.5f) * this.levelGenerationConfig.tileSideLength;

        bottomLeftPosition.x -= xOffset;
        bottomLeftPosition.y -= yOffset;

        int levelWidthInTiles = this.levelGenerationConfig.xDimension * this.levelGenerationConfig.roomWidth;
        int levelHeightInTiles = this.levelGenerationConfig.yDimension * this.levelGenerationConfig.roomHeight;


        Vector3 tileOffsetRight = Vector3.right * this.levelGenerationConfig.tileSideLength;
        Vector3 tileOffsetUp = Vector3.up * this.levelGenerationConfig.tileSideLength;

        //spawn bottom border including corners
        SpawnTileLine(bottomLeftPosition, tileOffsetRight, levelWidthInTiles + 2, this.levelGenerationConfig.boundryTile);

        //spawn left border without corners 
        SpawnTileLine(bottomLeftPosition+ tileOffsetUp, tileOffsetUp, levelHeightInTiles, this.levelGenerationConfig.boundryTile);

        //spawn top border with corners
        Vector3 topLeftPosition = bottomLeftPosition + tileOffsetUp * (levelHeightInTiles + 1);
        SpawnTileLine(topLeftPosition, tileOffsetRight, levelWidthInTiles + 2, this.levelGenerationConfig.boundryTile);

        //spawn right border without corners 
        Vector3 bottomRightPosition = bottomLeftPosition + tileOffsetRight * (levelWidthInTiles + 1);
        SpawnTileLine(bottomRightPosition + tileOffsetUp, tileOffsetUp, levelHeightInTiles, this.levelGenerationConfig.boundryTile);
    }

    private void SpawnTileLine(Vector3 startingPosition, Vector3 tileOffset, int amount, GameObject tile)
    {

        for (int i = 0; i < amount; i++)
        {
            Vector3 position = startingPosition + tileOffset * i;
            Instantiate(tile, position, Quaternion.identity);
        }
    }

    private void InitRandomNumberGenerator()
    {
        this.rng = new System.Random(this.seed.Value);
    }

    private void FillInFloorTiles()
    {
        UnityEngine.Object[] spawnPoints = FindObjectsOfType(typeof(SpawnPoint));

        foreach(UnityEngine.Object spawnPoint in spawnPoints)
        {
            ((SpawnPoint)spawnPoint).SpawnRandomObject(this.rng.Next());
        }
    }

    //get position of room in scene
    private Vector3 GetRoomCoordinates(int x, int y)
    {
        // bottom left room spawns at 0, 0
        float xCoord = x * this.levelGenerationConfig.roomWidth * this.levelGenerationConfig.tileSideLength;
        float yCoord = y * this.levelGenerationConfig.roomHeight * this.levelGenerationConfig.tileSideLength;

        return new Vector3(xCoord, yCoord, 0);
    }
}
