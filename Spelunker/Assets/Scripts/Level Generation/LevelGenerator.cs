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

    [Tooltip("Configuration scriptable object for level generation")]
    [SerializeField]
    private LevelGenerationConfig config;

    // using system random to have a seperate instance for safety
    private System.Random rng;

    // [0,0] is the bottom left room
    private RoomPlanData[,] levelPlan;

    private GameObject levelParentObject;

    void Start()
    {
        InitRandomNumberGenerator();
        SpawnNewLevel();
    }

    public void SpawnNewLevel()
    {
        EmptyLevel();
        InitLevelPlan();
        PlanLevelLayout();
        SpawnRooms();
        FillInFloorTiles();
        SpawnBounds();
    }

    private void EmptyLevel()
    {
        //Destroy current level
        if(this.levelParentObject != null)
        {
            GameObject.Destroy(this.levelParentObject);
        }

        this.levelParentObject = new GameObject("Level");
    }

    private void PlanLevelLayout()
    {

        // get choose starting point
        int startRoomX = rng.Next(0, this.config.xDimension);
        RoomPlanData startRoom = levelPlan[startRoomX, this.config.yDimension - 1];
        startRoom.roomPrefab = config.startRoom;

        // get choose end point:
        int goalRoomX = rng.Next(0, this.config.xDimension);
        RoomPlanData goalRoom = levelPlan[goalRoomX, 0];
        goalRoom.roomPrefab = config.goalRoom;

        PlanPath(startRoomX, goalRoomX);
    }

    private void PlanPath(int startRoomX, int goalRoomX)
    {
        int previousFloorDescentionX = startRoomX;

        // plan floors above bottom floor
        for (int y = this.config.yDimension - 1; y > 0; y--)
        {
            // current floor end generation
            int floorDescentionX = GenerateFloorDescention(previousFloorDescentionX);

            GenerateFloor(previousFloorDescentionX, y, floorDescentionX);

            previousFloorDescentionX = floorDescentionX;
        }

        //plan bottom floor
        GenerateFloor(previousFloorDescentionX, 0, goalRoomX);
    }

    //connects floor plan between previousFloorDescentionX and floorDescentionX on floor y
    private void GenerateFloor(int previousFloorDescentionX, int y, int floorDescentionX)
    {
        //floor starting point has to be open at top and closed at bottom
        levelPlan[previousFloorDescentionX, y].topState = OpeningState.OPEN;
        levelPlan[previousFloorDescentionX, y].bottomState = OpeningState.CLOSED;

        //floor end point has to be closed at top and open at bottom
        levelPlan[floorDescentionX, y].topState = OpeningState.CLOSED;
        levelPlan[floorDescentionX, y].bottomState = OpeningState.OPEN;


        int leftX = Math.Min(floorDescentionX, previousFloorDescentionX);
        int rightX = Math.Max(floorDescentionX, previousFloorDescentionX);

        // start and end horizontal openings
        levelPlan[leftX, y].rightState = OpeningState.OPEN;
        levelPlan[rightX, y].leftState = OpeningState.OPEN;

        // in between openings
        for (int x = leftX + 1; x < rightX; x++)
        {
            levelPlan[x, y].rightState = OpeningState.OPEN;
            levelPlan[x, y].leftState = OpeningState.OPEN;
        }

    }


    // Generate room x index on floor other than startRoomX
    private int GenerateFloorDescention(int startRoomX)
    {
        int roomXOffset = rng.Next(1, this.config.xDimension);
        int endRoomX = (startRoomX + roomXOffset) % this.config.xDimension;

        return endRoomX;
    }

    private void SpawnRooms()
    {
        //TODO: fix this mess
        for (int x = 0; x < this.config.xDimension; x++)
        {
            for (int y = 0; y < this.config.yDimension; y++)
            {
                if (this.levelPlan[x, y].roomPrefab == null)
                {
                    this.levelPlan[x, y].roomPrefab = this.config.GeneralRoomPool.GetRandomRoomWithOpenings(this.levelPlan[x, y], rng);
                }
            }
        }


        for (int x = 0; x < this.config.xDimension; x++)
        {
            for (int y = 0; y < this.config.yDimension; y++)
            {
                Vector3 position = GetRoomCoordinates(x, y);
                Instantiate(this.levelPlan[x, y].roomPrefab, position, Quaternion.identity, this.levelParentObject.transform);
            }
        }
    }

    private void InitLevelPlan()
    {
        this.levelPlan = new RoomPlanData[this.config.xDimension, this.config.yDimension];
        
        //fill initial level plan
        for(int x =0; x < this.config.xDimension; x++)
        {
            for (int y = 0; y < this.config.yDimension; y++)
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
        float xOffset = (this.config.roomWidth / 2f + 0.5f) * this.config.tileSideLength;
        float yOffset = (this.config.roomHeight / 2f + 0.5f) * this.config.tileSideLength;

        bottomLeftPosition.x -= xOffset;
        bottomLeftPosition.y -= yOffset;

        int levelWidthInTiles = this.config.xDimension * this.config.roomWidth;
        int levelHeightInTiles = this.config.yDimension * this.config.roomHeight;

        Vector3 tileOffsetRight = Vector3.right * this.config.tileSideLength;
        Vector3 tileOffsetUp = Vector3.up * this.config.tileSideLength;

        // parent object to group all the boundary tiles
        GameObject boundsObject = new GameObject("LevelBounds");
        boundsObject.transform.SetParent(this.levelParentObject.transform);

        //spawn bottom border including corners
        SpawnTileLine(bottomLeftPosition, tileOffsetRight, levelWidthInTiles + 2, this.config.boundryTile, boundsObject);

        //spawn left border without corners 
        SpawnTileLine(bottomLeftPosition+ tileOffsetUp, tileOffsetUp, levelHeightInTiles, this.config.boundryTile, boundsObject);

        //spawn top border with corners
        Vector3 topLeftPosition = bottomLeftPosition + tileOffsetUp * (levelHeightInTiles + 1);
        SpawnTileLine(topLeftPosition, tileOffsetRight, levelWidthInTiles + 2, this.config.boundryTile, boundsObject);

        //spawn right border without corners 
        Vector3 bottomRightPosition = bottomLeftPosition + tileOffsetRight * (levelWidthInTiles + 1);
        SpawnTileLine(bottomRightPosition + tileOffsetUp, tileOffsetUp, levelHeightInTiles, this.config.boundryTile, boundsObject);
    }

    private void SpawnTileLine(Vector3 startingPosition, Vector3 tileOffset, int amount, GameObject tile, GameObject parentObject)
    {

        for (int i = 0; i < amount; i++)
        {
            Vector3 position = startingPosition + tileOffset * i;
            Instantiate(tile, position, Quaternion.identity, parentObject.transform);
            
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
        float xCoord = x * this.config.roomWidth * this.config.tileSideLength;
        float yCoord = y * this.config.roomHeight * this.config.tileSideLength;

        return new Vector3(xCoord, yCoord, 0);
    }
}
