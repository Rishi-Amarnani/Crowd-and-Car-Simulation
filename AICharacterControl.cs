using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    //[RequireComponent(typeof (UnityEngine.AI.NavMeshAgent:]
    [RequireComponent(typeof (ThirdPersonCharacter ))]
    public class AICharacterControl : MonoBehaviour
    {
        //public UnityEngine.AI.NavMeshAgent agent { get; private set; } // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for
        UnityEngine.Random R;
        private Vector3 movement;
        private Rigidbody rBody;
        private float increment;
        private int counter;
        private int programCounter;
        private int oldProgramCounter;
        private int state; //0 == park, 1 == sidewalk, 2 == street
        private int updateFrequency;
        private int checkFrequency;
        private Vector3 position;
        bool moving;

        string previousTerrain;

        private void Start()
        {
            
            // get the components on the object we need ( should not be null due to require component so no need to check )
            //agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            updateFrequency = 50;
            checkFrequency = (int) UnityEngine.Random.RandomRange(20.0f, 30.0f);
            checkFrequency = 3;
            //agent.updateRotation = false;
            //agent.updatePosition = true;
            moving = false;
            rBody = character.GetComponent<Rigidbody>();
            counter = (int) UnityEngine.Random.RandomRange(0.0f, updateFrequency);
            //increment = 1.0f;
            //agent.SetDestination(new Vector3(-13.0f, 5.0f, 198.0f:;
            previousTerrain = "";
            //movement = new Vector3(0.0f, 0.0f, 0.0f);
            oldProgramCounter = -2;
            movement = Vector3.zero;
            UnityEngine.Random.InitState(42);
        }

        private void parkUpdate()
        {
            if (counter % updateFrequency == 0)
            {
                float f = UnityEngine.Random.RandomRange(0.0f, 1.0f);
                if (f < .5)
                {
                    movement = Vector3.zero;
                }
                else
                {
                    float f1 = UnityEngine.Random.RandomRange(-0.5f, 0.5f);
                    float f2 = UnityEngine.Random.RandomRange(-0.5f, 0.5f);
                    float f3 = UnityEngine.Random.RandomRange(-0.5f, 0.5f);
                    movement = new Vector3(f1, f2, f3);
                }
            }
        }

        private void doNothing()
        {
            movement = Vector3.zero;
        }

        private void PosXSWUpdate()
        {
            if(previousTerrain == "Park")
            {
                int doStart = (int) UnityEngine.Random.RandomRange(5.0f, 10.0f);
                for(int x = 0; x<doStart; ++x)
                {
                    float xStart = UnityEngine.Random.RandomRange(0.3f, 0.5f);
                    character.Move2(new Vector3(xStart, 0.0f, movement.z), false, false, rBody);
                }

                recordNewTerrain("PosXSW");
                float xOffset = UnityEngine.Random.RandomRange(0.0f, 0.3f);
                float zOffset = UnityEngine.Random.RandomRange(0.2f, 0.8f);
                movement = new Vector3(xOffset, 0.0f, zOffset);
                oldProgramCounter = programCounter;
            }
            else if(previousTerrain == "PosXSW" && programCounter == oldProgramCounter + 1)
            {
                float xOffset = UnityEngine.Random.RandomRange(-0.05f, 0.0f);
                float zOffset = UnityEngine.Random.RandomRange(0.2f, 0.8f);
                movement = new Vector3(xOffset, 0.0f, zOffset);
            }
            else
            {
                float xOffset = UnityEngine.Random.RandomRange(0.0f, 0.05f);
                float zOffset = UnityEngine.Random.RandomRange(0.2f, 0.8f);
                movement = new Vector3(xOffset, 0.0f, zOffset);
                oldProgramCounter = programCounter;                
            }
        }
        private void PosZSWUpdate()
        {
            if(previousTerrain == "Park")
            {
                character.Move2(new Vector3(0.0f, 0.0f, 0.1f), false, false, rBody);
                recordNewTerrain("PosZSW");
            }
            movement = new Vector3(-0.5f, 0.0f, 0.0f);
        }
        private void NegZSWUpdate()
        {
            if(previousTerrain == "Park")
            {
                character.Move2(new Vector3(0.0f, 0.0f, -0.1f), false, false, rBody);
                recordNewTerrain("NegZSW");
            }
            movement = new Vector3(0.5f, 0.0f, 0.0f);
        }
        private void SWCP2Update()
        {
            //keep moving at same speed
            if(previousTerrain == "SWCP2")
                return;
            float makeNegative = 1.0f;

            if(previousTerrain == "PosXSW"|| previousTerrain == "Park")
                makeNegative = -1.0f;

            float zOffset = UnityEngine.Random.RandomRange(0.1f, 0.5f) * makeNegative;
            float xOffset = UnityEngine.Random.RandomRange(0.1f, 0.5f) * makeNegative;
            movement = new Vector3(xOffset, 0.0f, zOffset);


        }
        private void SWCP3Update()
        {
            if(previousTerrain == "SWCP3")
                return;

            float makeNegative = 1.0f;

            if(previousTerrain == "PosXSW")
                makeNegative = -1.0f;

            float zOffset = UnityEngine.Random.RandomRange(0.1f, 0.5f) * makeNegative;
            float xOffset = UnityEngine.Random.RandomRange(0.1f, 0.5f) * makeNegative * -1.0f;
            movement = new Vector3(xOffset, 0.0f, zOffset);

            movement = new Vector3(-1.0f, 0.0f, 0.0f);
        }


/*
        private void SWCP1Update()
        {
            movement = new Vector3(0.3f, 0.0f, 0.0f);
        }
        private void SWCP4Update()
        {
            doNothing();
        }

        private void PosXSWGuardUpdate()
        {
            float positiveX = UnityEngine.Random.RandomRange(0.1f, 0.3f);
            character.Move2(new Vector3(1.0f, 0.0f, movement.z), false, false, rBody);
        }
*/
        private void recordNewTerrain(String newTag)
        {
                previousTerrain = newTag;
        }


        private void Update()
        {
            if(character==null)
            {
                Debug.LogError("It's the character!!");
            }


            if (counter % checkFrequency != 0)
            {
                character.Move2(movement, false, false, rBody);
                ++counter;
                return;
            }

            position = character.transform.position;

            RaycastHit hitinfo;
            float dist = 1;
            Vector3 dir = new Vector3(0, -1, 0);

            //To actually draw the raw for debugging purposes
            //Debug.DrawRay(position, dir * dist, Color.green);

            //the ray collided with some ground terrain, info in hitinfo
            if (Physics.Raycast(position, dir, out hitinfo, dist))
            {
                switch(hitinfo.collider.gameObject.tag)
                {
                    case "Park":
                    {
                        parkUpdate();
                        recordNewTerrain("Park");
                        break;
                    }

                    case "PosXSW":
                    {
                        PosXSWUpdate();
                        recordNewTerrain("PosXSW");
                        break;
                    }
                    case "PosZSW":
                    {
                        PosZSWUpdate();
                        recordNewTerrain("PosZSW");
                        break;
                    }
                    case "NegZSW":
                    {
                        NegZSWUpdate();
                        recordNewTerrain("NegZSW");
                        break;
                    }
                    case "SWCP2":
                    {
                        SWCP2Update();
                        recordNewTerrain("SWCP2");
                        break;
                    }
                    case "SWCP3":
                    {
                        SWCP3Update();
                        recordNewTerrain("SWCP3");
                        break;
                    }
/*                   
                    case "SWCP1":
                    {
                        SWCP1Update();
                        recordNewTerrain("SWCP1");
                        break;
                    }
                    case "SWCP4":
                    {
                        SWCP4Update();
                        recordNewTerrain("SWCP4");
                        break;
                    }
                    case "PosXSWGuard":
                    {
                        PosXSWGuardUpdate();
                        recordNewTerrain("PosXSWGuard");
                        break;
                    }
*/                    
                    default:
                    {
                        Debug.LogError("Physics Raycast succeeded on intersection, but did not intersect any known terrains");
                        break;
                    }
                }

            }
            else
            {
                //not intersection within 10 meters
                doNothing();

            }
            character.Move2(movement, false, false, rBody);
            counter++;
            programCounter++;
        }

    }
}
