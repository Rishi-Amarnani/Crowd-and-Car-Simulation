using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; } // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for
        UnityEngine.Random R;
        private Vector3 movement;
        private Rigidbody rBody;
        private float increment;
        private int counter;
        private int state; //0 == park, 1 == sidewalk, 2 == street
        private int updateFrequency;
        private Vector3 position;
        bool moving;

        private void Start()
        {
            
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            updateFrequency = 50;
	        agent.updateRotation = false;
	        agent.updatePosition = true;
            moving = false;
            rBody = character.GetComponent<Rigidbody>();
            movement = new Vector3(1.0f, 0.0f, 0.0f);
            counter = (int) UnityEngine.Random.RandomRange(0.0f, updateFrequency);
            //increment = 1.0f;
            //agent.SetDestination(new Vector3(-13.0f, 5.0f, 198.0f));
        }

        private void parkUpdate()
        {
            float f = UnityEngine.Random.RandomRange(0.0f, 1.0f);
            if (counter % updateFrequency == 0)
            {
                if (f < .5)
                {
                    movement = Vector3.zero;
                }
                else
                {
                    float f1 = UnityEngine.Random.RandomRange(-1.0f, 1.0f);
                    float f2 = UnityEngine.Random.RandomRange(-1.0f, 1.0f);
                    float f3 = UnityEngine.Random.RandomRange(-1.0f, 1.0f);
                    movement = new Vector3(f1, f2, f3);
                }
            }
            character.Move2(movement, false, false, rBody);
        }

        private void testUpdate()
        {
            
            character.Move2(new Vector3(10.0f, 0.0f, 0.0f), false, false, rBody);
        }

        private void Update()
        {
            position = character.transform.position;
            if (position.x < 0)
            {
                parkUpdate();
            } else 
            {
                testUpdate();
            }

            
            counter++;
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
