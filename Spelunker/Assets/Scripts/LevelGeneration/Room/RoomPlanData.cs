using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Level_Generation
{
    public enum OpeningState
    {
        CLOSED,
        OPEN,
        ANY
    }

    // contains data for one room used when creating a plan for a level
    public class RoomPlanData
    {
        public int xCoordinate;
        public int yCoordinate;

        // special room conditions
        public bool isStart = false;
        public bool isGoal = false;

        // conditions for openings the placed room has to fulfill
        public OpeningState topState = OpeningState.ANY;
        public OpeningState bottomState = OpeningState.ANY;
        public OpeningState leftState = OpeningState.ANY;
        public OpeningState rightState = OpeningState.ANY;

        // room prefab to be placed in this location
        public GameObject roomPrefab;

        public RoomPlanData(int xCoordinate, int yCoordinate)
        {
            this.xCoordinate = xCoordinate;
            this.yCoordinate = yCoordinate;
        }
    }
}
