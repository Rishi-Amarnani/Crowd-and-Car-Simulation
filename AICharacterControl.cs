using System;
using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.Vehicles.Car;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter ))]
    public class AICharacterControl : MonoBehaviour
    {
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        UnityEngine.Random R;
        private Vector3 movement;
        private Vector3 position;
        private Rigidbody rBody;

        private int counter;
        private int state; //0 == park, 1 == sidewalk, 2 == street
        
        private int parkUpdateFrequency;
        private int speedUpdateFrequency;
        
        private int doStart;
        private float makeNeg;
        private int type; 

        private Vector3 destination;

        private int waitTime;
        private string previousTerrain;
        private bool finishedStaring;
        private bool goingToPark;

        private float sidewalkSpeed;
        private bool increasingSpeed;
        private bool crossingRoad = false;
        private bool crossingBack = false;
        private bool alreadyDecided = false;

        private GameObject carParent;
        private CarController car;


        private void Start()
        {
            character = GetComponent<ThirdPersonCharacter>();

            carParent = GameObject.Find("Car");

            car = carParent.GetComponent<CarController>();


            speedUpdateFrequency = 200;
            parkUpdateFrequency = 50;

            rBody = character.GetComponent<Rigidbody>();
            counter = (int) UnityEngine.Random.Range(0.0f, parkUpdateFrequency);
            previousTerrain = "";
            movement = Vector3.zero;
            doStart = -1;
            makeNeg = 1.0f;
            finishedStaring = false;
            goingToPark = false;
            crossingRoad = false;

            sidewalkSpeed = UnityEngine.Random.RandomRange(0.1f, 0.8f);
            increasingSpeed = (UnityEngine.Random.RandomRange(0.0f, 1.0f) < 0.5f);
            
            float typeNum = UnityEngine.Random.Range(0.0f, 1.0f);
            if (typeNum < 0.5f) {
                type = 0;
            } else {
                type = 1;
                destination = randomParkDestination();
            }
        }

        private void parkUpdate()
        {
            if (type == 0) {
                if (counter % parkUpdateFrequency == 0)
                {
                    float f = UnityEngine.Random.Range(0.0f, 1.0f);
                    
                    if (f < .5)
                    {
                        movement = Vector3.zero;
                    }
                    else {
                        float f1 = UnityEngine.Random.Range(-0.5f, 0.5f);
                        float f2 = UnityEngine.Random.Range(-0.5f, 0.5f);
                        float f3 = UnityEngine.Random.Range(-0.5f, 0.5f);
                        movement = new Vector3(f1, f2, f3);
                    }
                }
            } 
            else {
                if ((character.transform.position - destination).magnitude < 5) {
                        destination = randomParkDestination();
                } else {
                        movement = destination - character.transform.position;
                        movement.Normalize();
                        movement = movement/3;
                }
            } 
        }

        private Vector3 randomParkDestination() {
            float xDest = UnityEngine.Random.Range(-330.0f, 0.0f);
            float zDest = UnityEngine.Random.Range(30.0f, 320.0f);
            return new Vector3(xDest, 0, zDest);
        }

        private void moveRandPosX()
        {
            movement = new Vector3(UnityEngine.Random.RandomRange(0.1f, 0.8f), movement.y, movement.z);
        }

        private void moveRandPosZ()
        {
            movement = new Vector3(movement.x, movement.y, UnityEngine.Random.RandomRange(0.1f, 0.8f));
        }

        private void moveRandNegX()
        {
            movement = new Vector3(-1.0f * UnityEngine.Random.RandomRange(0.1f, 0.8f), movement.y, movement.z);
        }

        private void moveRandNegZ()
        {
            movement = new Vector3(movement.x, movement.y, -1.0f * UnityEngine.Random.RandomRange(0.1f, 0.8f));
        }

        private void movePosZ()
        {
            movement = new Vector3(movement.x, movement.y, 0.5f);
        }

        private void moveNegZ()
        {
            movement = new Vector3(movement.x, movement.y, -0.5f);
        }

        private void movePosX()
        {
            movement = new Vector3(0.5f, movement.y, movement.z);
        }

        private void moveNegX()
        {
            movement = new Vector3(-0.5f, movement.y, movement.z);
        }

        private bool moveToPark(int SWCP)
        {
            //90% chance that they won't go back to park + their speed must be slow enough
            if(sidewalkSpeed > 0.6f || UnityEngine.Random.RandomRange(0.0f, 1.0f) < .9)
            {
                goingToPark = false;
                return false;
            }

            float speedValue = (float) Math.Sqrt(0.5f * Math.Pow(sidewalkSpeed, 2));

            switch(SWCP)
            {
                case 1: 
                {
                    movement = new Vector3(speedValue, 0.0f, speedValue);
                    break;
                }
                case 2: 
                {
                    movement = new Vector3(-1.0f * speedValue, 0.0f, speedValue);
                    break;
                }
                case 3: 
                {
                    movement = new Vector3(-1.0f * speedValue, 0.0f, -1.0f * speedValue);
                    break;
                }
                case 4: 
                {
                    movement = new Vector3(speedValue, 0.0f, -1.0f * speedValue);
                    break;
                }
                default:
                {
                    Debug.LogError("moveToPark() bad input: SWCP must be between 1 and 4 inclusive.");
                    break;
                }
            }

            goingToPark = true;
            return true;
        }

        //a function to gradually increase or decrease the speed
        private void changeSidewalkSpeed()
        {
            if(increasingSpeed)
            {
                if(sidewalkSpeed >= 0.8f)
                {                    
                    increasingSpeed = false;
                    changeSidewalkSpeed();
                    return;
                }
                sidewalkSpeed = sidewalkSpeed + 0.2f;
            }
            else
            {
                if(sidewalkSpeed <= 0.2f)
                {
                    increasingSpeed = true;
                    changeSidewalkSpeed();
                    return;
                }
                sidewalkSpeed = sidewalkSpeed - 0.2f;
            }
        }

        private void StopAndStare()
        {
            movement = Vector3.zero;
        }

        private void PosXSWUpdate()
        {
            if(goingToPark) return;
            
            if(previousTerrain == "PosXSW")
            {
                if(doStart > 1)
                {
                    --doStart;
                }
                else if(doStart == 1)
                {
                    movement = new Vector3(0.0f, 0.0f, sidewalkSpeed * makeNeg);      
                    --doStart;
                }
                return;                
            }

            doStart = 1;
            if(previousTerrain == "Park")
            {
                makeNeg = 1.0f;
                if(movement.z != 0)
                    makeNeg = movement.z/Math.Abs(movement.z);
                doStart = (int) UnityEngine.Random.RandomRange(3.0f, 10.0f);
                movement = new Vector3(sidewalkSpeed, 0.0f , 0.0f);
                --doStart;
                return;
            }
            
            if(previousTerrain == "SWCP3")
            {
                makeNeg = -1.0f;
            }
            else if(previousTerrain == "SWCP2")
            {
                makeNeg = 1.0f;
            }
            recordNewTerrain("PosXSW");
            PosXSWUpdate();
        }

        private void NegXSWUpdate()
        {            
            if(goingToPark) return;

            if(previousTerrain == "NegXSW")
            {
                if(doStart > 1)
                {
                    --doStart;
                }
                else if(doStart == 1)
                {
                    movement = new Vector3(0.0f, 0.0f, sidewalkSpeed * makeNeg);
                    --doStart;
                }
                return;                
            }

            doStart = 1;
            
            if(previousTerrain == "Park")
            {
                makeNeg = -1.0f;
                if(movement.z != 0)
                    makeNeg = movement.z/Math.Abs(movement.z) * -1.0f;
                doStart = (int) UnityEngine.Random.RandomRange(3.0f, 10.0f);
                movement = new Vector3(-1.0f * sidewalkSpeed, 0.0f , 0.0f);
                --doStart;
                return;
            }
            
            if(previousTerrain == "SWCP4")
            {
                makeNeg = -1.0f;
            }
            else if(previousTerrain == "SWCP1")
            {
                makeNeg = 1.0f;
            }
            recordNewTerrain("NegXSW");
            NegXSWUpdate();
        }


        private void PosZSWUpdate()
        {
            if(goingToPark) return;

            if(previousTerrain == "PosZSW")
            {
                if(doStart > 1)
                {
                    --doStart;
                }
                else if(doStart == 1)
                {
                    movement = new Vector3(sidewalkSpeed * makeNeg , 0.0f, 0.0f);
                    --doStart;
                }
                return;
            }

            doStart = 1;
            
            if(previousTerrain == "Park")
            {
                makeNeg = -1.0f;
                if(movement.x != 0)
                    makeNeg = movement.x/Math.Abs(movement.x) * -1.0f;
                doStart = (int) UnityEngine.Random.RandomRange(3.0f, 10.0f);
                movement = new Vector3(0.0f, 0.0f , sidewalkSpeed);
                --doStart;
                return;
            }
            
            if(previousTerrain == "SWCP3")
            {
                makeNeg = -1.0f;
            }
            else if(previousTerrain == "SWCP4")
            {
                makeNeg = 1.0f;
            }
            recordNewTerrain("PosZSW");
            PosZSWUpdate();
        }

        private void NegZSWUpdate()
        {
            if(crossingBack)
            {
                car.decrementCountNegZ();
                crossingRoad = false;
                crossingBack = false;
                alreadyDecided = false;   
            }
            else if(crossingRoad) return;


            if(goingToPark) return;

            if(UnityEngine.Random.RandomRange(0.0f, 1.0f) < 0.01f)
            {
                crossingRoad = true;
                crossingBack = false;
                alreadyDecided = false;

                movement = new Vector3(0.0f, 0.0f, -0.5f);
                return;
            }

            if(previousTerrain == "NegZSW")
            {
                if(doStart > 1)
                {
                    --doStart;
                }
                else if(doStart == 1)
                {
                    movement = new Vector3(sidewalkSpeed * makeNeg , 0.0f, 0.0f);
                    --doStart;
                }
                return;
            }

            doStart = 1;
            
            if(previousTerrain == "Park")
            {
                makeNeg = -1.0f;
                if(movement.x != 0)
                    makeNeg = movement.x/Math.Abs(movement.x);

                doStart = (int) UnityEngine.Random.RandomRange(3.0f, 10.0f);
                movement = new Vector3(0.0f, 0.0f , -1.0f * sidewalkSpeed);
                --doStart;
                return;
            }    
            
            if(previousTerrain == "SWCP2")
            {
                makeNeg = -1.0f;
            }
            else if(previousTerrain == "SWCP1")
            {
                makeNeg = 1.0f;
            }
            recordNewTerrain("NegZSW");
            NegZSWUpdate();
        }

        private void SWCP2Update()
        {
            if(goingToPark) return;

            if((previousTerrain == "PosXSW" || previousTerrain == "NegZSW") &&
                moveToPark(2))
                    return;

            //keep moving at same speed
            if(previousTerrain == "SWCP2")
            {
                if(doStart == -1)
                {
                    ++doStart;
                }
                else if(doStart > 1)
                {
                    --doStart;
                }
                else if(doStart == 1)
                {
                    float xOffset = UnityEngine.Random.RandomRange(0.1f, 0.5f) * makeNeg;
                    float zOffset = UnityEngine.Random.RandomRange(0.1f, 0.5f) * makeNeg;
                    movement = new Vector3(xOffset, 0.0f, zOffset);
                    --doStart;
                }
                return;
            }

            //case where you just turn around 180 degrees and go back the way you came. Happens atypically.
            if((previousTerrain == "PosXSW" || previousTerrain == "NegZSW") && (UnityEngine.Random.RandomRange(0.0f, 1.0f) < 0.2f))
            {
                doStart = -1;
                movement = -1.0f * movement;
                return;
            }
            
            makeNeg = 1.0f;

            movement = new Vector3(0.5f, 0.0f, 0.0f);

            if(previousTerrain == "PosXSW")
            {
                makeNeg = -1.0f;
                movement = new Vector3(0.0f, 0.0f, -0.5f);    
            }

            else if(previousTerrain == "Park")
            {
                int flipCoin = (int) UnityEngine.Random.RandomRange(0.0f, 1.0f);
                if(flipCoin == 0)
                    makeNeg = -1.0f;
            }
            
            doStart = (int) UnityEngine.Random.RandomRange(7.0f, 12.0f);
        }

        private void SWCP3Update()
        {
            if(goingToPark) return;

            if((previousTerrain == "PosXSW" || previousTerrain == "PosZSW") &&
                moveToPark(3))
                    return;

            if(previousTerrain == "SWCP3")
            {
                if(doStart == -1)
                {
                    ++doStart;
                }
                else if(doStart > 1)
                {
                    --doStart;
                }
                else if(doStart == 1)
                {
                    float xOffset = UnityEngine.Random.RandomRange(0.1f, 0.5f) * makeNeg;
                    float zOffset = UnityEngine.Random.RandomRange(0.1f, 0.5f) * makeNeg * -1.0f;
                    movement = new Vector3(xOffset, 0.0f, zOffset);
                    --doStart;
                }
                return;
            }

            //case where you just turn around 180 degrees and go back the way you came. Happens atypically.
            if((previousTerrain == "PosXSW" || previousTerrain == "PosZSW") && (UnityEngine.Random.RandomRange(0.0f, 1.0f) < 0.2f))
            {
                doStart = -1;
                movement = -1.0f * movement;
                return;
            }


            makeNeg = 1.0f;
            movement = new Vector3(0.5f, 0.0f, 0.0f);

            if(previousTerrain == "PosXSW")
            {
                makeNeg = -1.0f;
                movement = new Vector3(0.0f, 0.0f, 0.5f);
            }

            else if(previousTerrain == "Park")
            {
                int flipCoin = (int) UnityEngine.Random.RandomRange(0.0f, 1.0f);
                if(flipCoin == 0)
                    makeNeg = -1.0f;
            }

            doStart = (int) UnityEngine.Random.RandomRange(7.0f, 12.0f);
        }



        private void SWCP1Update()
        {
            if(goingToPark) return;
            
            if((previousTerrain == "NegXSW" || previousTerrain == "NegZSW") &&
                moveToPark(1))
                    return;

            if(previousTerrain == "SWCP1")
            {
                if(doStart == -1)
                {
                    ++doStart;
                }
                else if(doStart > 1)
                {
                    --doStart;
                }
                else if(doStart == 1)
                {
                    float xOffset = UnityEngine.Random.RandomRange(0.1f, 0.5f) * makeNeg;
                    float zOffset = UnityEngine.Random.RandomRange(0.1f, 0.5f) * makeNeg * -1.0f;
                    movement = new Vector3(xOffset, 0.0f, zOffset);
                    --doStart;
                }
                return;
            }

                
            if((previousTerrain == "NegXSW" || previousTerrain == "NegZSW") && (UnityEngine.Random.RandomRange(0.0f, 1.0f) < 0.2f))
            {
                doStart = -1;
                movement = -1.0f * movement;
                return;
            }

            makeNeg = 1.0f;
            movement = new Vector3(0.0f, 0.0f, -0.5f);
            
            if(previousTerrain == "NegZSW")
            {
                makeNeg = -1.0f;       
                movement = new Vector3(-0.5f, 0.0f, 0.0f);
            }
            if(previousTerrain == "Park")
            {
                int flipCoin = (int) UnityEngine.Random.RandomRange(0.0f, 1.0f);
                if(flipCoin == 0)
                    makeNeg = -1.0f;
            }

            doStart = (int) UnityEngine.Random.RandomRange(7.0f, 12.0f);
        }

        private void SWCP4Update()
        {
            if(goingToPark) return;
            
            if((previousTerrain == "PosZSW" || previousTerrain == "NegXSW") &&
                moveToPark(4))
                    return;

            if(previousTerrain == "SWCP4")
            {
                if(doStart == -1)
                {
                    ++doStart;
                }
                else if(doStart > 1)
                {
                    --doStart;
                }
                else if(doStart == 1)
                {
                    float xOffset = UnityEngine.Random.RandomRange(0.1f, 0.5f) * makeNeg;
                    float zOffset = UnityEngine.Random.RandomRange(0.1f, 0.5f) * makeNeg * -1.0f;
                    movement = new Vector3(xOffset, 0.0f, zOffset);
                    --doStart;
                }
                return;
            }
                
            if((previousTerrain == "NegXSW" || previousTerrain == "PosZSW") && (UnityEngine.Random.RandomRange(0.0f, 1.0f) < 0.2f))
            {
                doStart = -1;
                movement = -1.0f * movement;
                return;
            }

            makeNeg = 1.0f;
            movement = new Vector3(0.0f, 0.0f, 0.5f);
            
            if(previousTerrain == "PosZSW")
            {
                movement = new Vector3(-0.5f, 0.0f, 0.0f);
                makeNeg = -1.0f;
            }
            if(previousTerrain == "Park")
            {
                int flipCoin = (int) UnityEngine.Random.RandomRange(0.0f, 1.0f);
                if(flipCoin == 0)
                    makeNeg = -1.0f;
            }

            doStart = (int) UnityEngine.Random.RandomRange(7.0f, 12.0f);
        }

        private void recordNewTerrain(String newTag)
        {
                previousTerrain = newTag;
        }

        private bool moveRiver(float x, float z)
        {
            if(x >= -194 && x <= -163 && z >= 100 && z <= 132)
            {
                HandleRiver(true, false);
                return true;
            }
            else if(x >= -226 && x <= -194 && z >= 100 && z <= 132)
            {
                HandleRiver(false, false);
                return true;
            }
            else if(x >= -194 && x <= -163 && z >=132 && z <= 164)
            {
                HandleRiver(true, true);
                return true;
            }
            else if(x >= -226 && x <= -194 && z >=132 && z <= 164)
            {
                HandleRiver(true, true);
                return true;
            }
            else if(z >= 212 && z <= 281)
            {
                if(x <= -166 && x >= -183)
                {
                    HandleRiver(true, true);
                    return true;
                }
                else if(x >= -200 && x <= -183)
                {
                    HandleRiver(false, true);
                    return true;
                }
            }
            return false;
        }


        private void HandleRiver(bool makeXPos, bool makeZPos)
        {
            if(finishedStaring)
                return;

            if(movement.magnitude == 0)
            {
                if(doStart == 0)
                {
                    if(makeXPos)
                        moveRandPosX();
                    else
                        moveRandNegX();

                    if(makeZPos)
                        moveRandPosZ();
                    else
                        moveRandNegZ();

                    finishedStaring = true;
                }
                else
                {
                    --doStart;
                }
                return;
            }

            float rand = UnityEngine.Random.RandomRange(0.0f, 1.0f);
            if(rand < 0.5f)
            {
                StopAndStare();
                doStart = (int) UnityEngine.Random.RandomRange(30.0f, 100.0f);
            }
            else
            {
                if(makeXPos)
                    moveRandPosX();
                else
                    moveRandNegX();

                if(makeZPos)
                    moveRandPosZ();
                else
                    moveRandNegZ();
            }
            destination = randomParkDestination();
        }

        private void NegZOuterUpdate(float x, float z)
        {
            if(!crossingBack)
            {
                if(previousTerrain == "NegZSW")
                {
                    doStart = (int)UnityEngine.Random.RandomRange(100.0f, 200.0f);
                    StopAndStare();
                    car.decrementCountNegZ();
                }
                if(doStart > 1)
                    --doStart;
                if(doStart == 1)
                {
                    movement = new Vector3(0.0f, 0.0f, sidewalkSpeed);
                    --doStart;
                    crossingBack = true;
                }                
            }                    
            recordNewTerrain("NegZOuterSW");
        }

        private void Update()
        {
            if(counter % speedUpdateFrequency == 0)
                changeSidewalkSpeed();

            position = character.transform.position;
            float x = position.x;
            float z = position.z;
            
            //NegZRoad
            if(x >= -350 && x <= 27 && z >= -66 && z <= -15)
            {
                if(!alreadyDecided)
                {
                    if(car.location == 2) 
                    {
                        StopAndStare();
                    }
                    else
                    {
                        car.incrementCountNegZ();
                        
                        movement = new Vector3(0.0f, 0.0f, 0.5f);
                        if(previousTerrain == "NegZSW")
                            movement = new Vector3(0.0f, 0.0f, -0.5f);
                        
                        alreadyDecided = true;
                    }
                }
            }
            //NegZ outer SW
            else if(x >= -350 && x <= 27 && z < -66)
            {
                alreadyDecided = false;
                NegZOuterUpdate(x,z);
            }
            else if(x < -350 && !crossingRoad && !crossingBack){
                movePosX();
            }
            else if(x > 25 && !crossingRoad && !crossingBack){
                moveNegX();
            }
            else if(z < -15 && !crossingRoad && !crossingBack){
                movePosZ();
            }
            else if(z > 362 && !crossingRoad && !crossingBack){
                moveNegZ();
            }
            else if(z <= -1)
            {
                if(x <= -336){
                    SWCP1Update();
                    recordNewTerrain("SWCP1");
                }
                else if(x >= 15){
                    SWCP2Update();
                    recordNewTerrain("SWCP2");
                }
                else
                {
                    NegZSWUpdate();
                    recordNewTerrain("NegZSW");                    
                }

            }
            else if(z >= 349){
                if(x <= -336){
                    SWCP4Update();
                    recordNewTerrain("SWCP4");
                    
                }
                else if(x >= 15){
                    SWCP3Update();
                    recordNewTerrain("SWCP3");                    
                }
                else
                {
                    PosZSWUpdate();
                    recordNewTerrain("PosZSW");
                }
                
            }

            else if(x <= -336 && z < 349 && z > 1){
                //crossingRoad = false;
                NegXSWUpdate();
                recordNewTerrain("NegXSW");
                
            }
            else if(x >= 15 && z < 349 && z > 1){
                //crossingRoad = false;
                PosXSWUpdate();
                recordNewTerrain("PosXSW");
                
            }

            //PosZRoad
            
            else{
                crossingRoad = false;
                crossingBack = false;
                alreadyDecided = false;
                //Calls moveRiver(), and then runs parkUpdate() if moveRiver failed
                if(!moveRiver(x,z))
                {
                    goingToPark = false;
                    finishedStaring = false;
                    parkUpdate();
                    recordNewTerrain("Park");
                }
            }

            character.Move2(movement, false, false, rBody);
            counter++;
        }
    }
}
