using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        public CarController m_Car; 

        private float steering = 0.0f;
        private float accel = 0.0f;
        private float footbreak = 0.0f;
        
        private Vector3 movement = Vector3.zero;
        private string previous = "";
        private float expectedAngle = 0.0f;

        private void Awake()
        {
            m_Car = GetComponent<CarController>();
        }


        private void Update()
        {
            float x = m_Car.transform.position[0];
            float z = m_Car.transform.position[2];
            m_Car.location = -1;

            //entire positive X side
            if(x <= 78 && x > 30)
            {
                if(z <= -20 && z >= -62)
                {
                    movement = new Vector3(0.0f, 0.0f, 1.0f);
                    expectedAngle = 45.0f;
                }
                else if(z < 365 && z > -20)
                {
                    movement = new Vector3(0.0f, 0.0f, 1.0f);
                    expectedAngle = 0.0f;
                }
                else if(z <= 410 && z >= 365)
                {
                    movement = new Vector3(0.0f, 0.0f, 1.0f);
                    expectedAngle = 315.0f;
                }
            }

            //positive z SW
            else if(z <= 410 && z >= 365 && x < 30 && x > -355)
            {
                movement = new Vector3(0.0f, 0.0f, 1.0f);
                expectedAngle = 270.0f;
            }

            //entire negative x side
            else if(x <= -355 && x >= -397)
            {
                if(z <= 410 && z >= 365)
                {            
                    movement = new Vector3(0.0f, 0.0f, 1.0f);
                    expectedAngle = 225.0f;
                }
                else if(z < 365 && z > -20)
                {
                    movement = new Vector3(0.0f, 0.0f, 1.0f);
                    expectedAngle = 180.0f;
                }
                else if(z <= -20 && z >= -62)
                {
                    movement = new Vector3(0.0f, 0.0f, 1.0f);
                    expectedAngle = 135.0f;
                }
                
            }            

            //negative z SW
            else if(z <= -20 && z >= -62 && x < 30 && x > -355)
            {
                if(m_Car.NegZCount > 0)
                {
                    m_Car.location = 2;
                    movement = Vector3.zero;
                }
                else
                {
                    m_Car.location = 2;
                    movement = new Vector3(0.0f, 0.0f, 1.0f);
                    expectedAngle = 90.0f;
                }
            }

            m_Car.Move2(movement, expectedAngle);
        }

        }
    }
