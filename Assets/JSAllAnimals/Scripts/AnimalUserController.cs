using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JSukoAnimals
{
    public class AnimalUserController : MonoBehaviour
    {
        public AnimalCharacter animalCharacter;


        // Start is called before the first frame update
        void Start()
        {
            animalCharacter = GetComponent<AnimalCharacter>();
        }

        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                animalCharacter.Attack();
            }

            if (Input.GetButtonDown("Jump"))
            {
                animalCharacter.Jump();
            }


            if (Input.GetKeyDown(KeyCode.H))
            {
                animalCharacter.Hit();
            }


            if (Input.GetKeyDown(KeyCode.K))
            {
                animalCharacter.Death();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                animalCharacter.Rebirth();
            }



            if (Input.GetKeyDown(KeyCode.Y))
            {
                animalCharacter.Threat();
            }


            if (Input.GetKeyDown(KeyCode.E))
            {
                animalCharacter.Eat();
            }




            if (Input.GetKeyDown(KeyCode.P))
            {
                animalCharacter.HopStart();
            }


            if (Input.GetKeyUp(KeyCode.P))
            {
                animalCharacter.HopEnd();
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                animalCharacter.DiagonallyWalkStart();
            }


            if (Input.GetKeyUp(KeyCode.O))
            {
                animalCharacter.DiagonallyWalkEnd();
            }


            if (Input.GetKeyDown(KeyCode.Q))
            {
                animalCharacter.DrinkStart();
            }

            if (Input.GetKeyUp(KeyCode.Q))
            {
                animalCharacter.DrinkEnd();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                animalCharacter.Cry();
            }


            if (Input.GetKeyDown(KeyCode.N))
            {
                animalCharacter.SitdownStart();
            }

            if (Input.GetKeyUp(KeyCode.N))
            {
                animalCharacter.SitdownEnd();
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                animalCharacter.SleepStart();
            }

            if (Input.GetKeyUp(KeyCode.M))
            {
                animalCharacter.SleepEnd();
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                animalCharacter.HeadShake();
            }


        }

        private void FixedUpdate()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            if (Input.GetKey(KeyCode.LeftShift)) v *= 2f;

            animalCharacter.turnSpeed = h;
            animalCharacter.forwardSpeed = v;
        }
    }
}
