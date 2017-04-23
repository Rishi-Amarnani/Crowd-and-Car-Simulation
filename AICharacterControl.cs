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
            movement = new Vector3(1.0f, 0.0f, 0.0f);
            //increment = 1.0f;
            //agent.SetDestination(new Vector3(-13.0f, 5.0f, 198.0f));
        }


        private void Update()
        {
            //R = new System.Random();
            //if (target != null)
            //    agent.SetDestination(target.position);
            //if you can still move forward, move forward


            //character.Move2(new Vector3(1.0f, 0.0f, 0.0f), false, false, rBody);
            //rBody.transform.Translate(new Vector3(1.0f, 0.0f, 0.0f));


            //if (agent.remainingDistance > agent.stoppingDistance)
            //{
            //    character.Move(agent.desiredVelocity * 0.1f/* * increment*/, false, false);
            //    //                character.runAnim(agent.desiredVelocity, false, false);
            //    rBody.transform.Translate(agent.desiredVelocity * 0.1f/* * increment*/ * 1.0f);
            //    //increment = Math.Min(2.0f, increment * 1.05f);
            //}
            //else
            //{
            //    //increment = 1.0f;
            //    character.Move2(Vector3.zero, false, false, rBody);
            //}
            //if (counter % 10 == 0)
            //{

            float f = UnityEngine.Random.RandomRange(0.0f, 1.0f);
            if (counter % 10 == 0)
            {
                if (f < .5)
                {
                    movement = Vector3.zero;
                }
                else
                {
                    movement = new Vector3(1.0f, 0.0f, 0.0f);
                }
            }
            character.Move2(movement, false, false, rBody);

            //    if (read < .9)
            //    {
            //        Console.WriteLine("read < .9");
            //        if (moving)
            //        {
            //            character.Move2(movement, false, false, rBody);
            //        }
            //    }
            //    else if (read < .95)
            //    {
            //        Console.WriteLine("read < .95");
            //        if (moving)
            //        {
            //            character.Move2(Vector3.zero, false, false, rBody);
            //            moving = false;
            //        }
            //        else
            //        {
            //            character.Move2(movement, false, false, rBody);
            //            moving = true;
            //        }
            //    }
            //    else
            //    {
            //        Console.WriteLine("BAD");
            //    }
            //    //float f = R.Next();
            //    //float f2 = R.Next();
            //    //float f3 = R.Next();
            //    //character.Move2(new Vector3(f, f2, f3), false, false, rBody);
            ////}
            counter++;
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
