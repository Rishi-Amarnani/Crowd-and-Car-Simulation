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
        System.Random R;
        private Rigidbody rBody;
        private float increment;

        bool moving;

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

	        agent.updateRotation = false;
	        agent.updatePosition = true;
            moving = false;
            rBody = character.GetComponent<Rigidbody>();
            //increment = 1.0f;
            agent.SetDestination(new Vector3(-13.0f, 5.0f, 198.0f));
        }


        private void Update()
        {
            //R = new System.Random();
            //if (target != null)
            //    agent.SetDestination(target.position);
            //if you can still move forward, move forward
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                character.Move(agent.desiredVelocity/* * increment*/, false, false);
                //                character.runAnim(agent.desiredVelocity, false, false);
                rBody.transform.Translate(agent.desiredVelocity/* * increment*/ * 1.0f);
                //increment = Math.Min(2.0f, increment * 1.05f);
            }
            else
            {
                //increment = 1.0f;
                character.Move(Vector3.zero, false, false);
            }
            //double read = R.NextDouble();
            //Console.WriteLine(read);
            //if (read < .9)
            //{
            //    Console.WriteLine("read < .9");
            //    if (moving)
            //    {
            //        character.Move(agent.desiredVelocity, false, false);
            //    }
            //} else if (read < .95)
            //{
            //    Console.WriteLine("read < .95");
            //    if (moving)
            //    {
            //        character.Move(Vector3.zero, false, false);
            //        moving = false;
            //    } else
            //    {
            //        character.Move(new Vector3(1,100,1), false, false);
            //        moving = true;
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("BAD");
            //}
            //float f = R.Next();
            //float f2 = R.Next();
            //float f3 = R.Next();
            //character.Move(new Vector3(f, f2, f3), false, false);

        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
