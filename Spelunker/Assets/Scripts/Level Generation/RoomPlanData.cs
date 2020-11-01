using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Level_Generation
{
    class RoomPlanData
    {

        public int xCoordinate;
        public int yCoordinate;

        public GameObject roomPrefab;

        public RoomPlanData(int xCoordinate, int yCoordinate)
        {
            this.xCoordinate = xCoordinate;
            this.yCoordinate = yCoordinate;
        }
    }
}
