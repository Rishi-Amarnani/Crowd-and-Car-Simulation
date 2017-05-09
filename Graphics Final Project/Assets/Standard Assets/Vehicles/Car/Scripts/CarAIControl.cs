using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarAIControl : MonoBehaviour
    {
     
        //positive numbers indicate the number of ppl on that road
        //-1 indicates that the car is on that road
        private int PosXCount = 0;
        private int NegXCount = 0;
        private int PosZCount = 0;
        private int NegZCount = 0;

        private void Update()
        {
            
        }

    }
}
