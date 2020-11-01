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

    class RoomPlanData
    {

        public int xCoordinate;
        public int yCoordinate;

        public bool isStart = false;
        public bool isGoal = false;

        public OpeningState topState = OpeningState.ANY;
        public OpeningState bottomState = OpeningState.ANY;
        public OpeningState leftState = OpeningState.ANY;
        public OpeningState rightState = OpeningState.ANY;

        public GameObject roomPrefab;

        public RoomPlanData(int xCoordinate, int yCoordinate)
        {
            this.xCoordinate = xCoordinate;
            this.yCoordinate = yCoordinate;
        }
    }
}
