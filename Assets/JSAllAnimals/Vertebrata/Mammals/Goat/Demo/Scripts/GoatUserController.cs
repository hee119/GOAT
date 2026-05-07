using UnityEngine;
using System.Collections;

namespace JSukoAnimals
{
    public class GoatUserController : MonoBehaviour
    {
        GoatCharacter goatCharacter;

        void Start()
        {
            goatCharacter = GetComponent<GoatCharacter>();
        }

        void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                goatCharacter.Attack();
            }
            if (Input.GetButtonDown("Jump"))
            {
                goatCharacter.Jump();
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                goatCharacter.Hit();
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                goatCharacter.Death();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                goatCharacter.Rebirth();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                goatCharacter.EatStart();
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                goatCharacter.EatEnd();
            }


            if (Input.GetKeyDown(KeyCode.G))
            {
                goatCharacter.Gallop();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                goatCharacter.Canter();
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                goatCharacter.Trot();
            }
            if (Input.GetKeyUp(KeyCode.X))
            {
                goatCharacter.Walk();
            }

            goatCharacter.forwardSpeed = goatCharacter.maxWalkSpeed * Input.GetAxis("Vertical");
            goatCharacter.turnSpeed = Input.GetAxis("Horizontal");
        }
    }
}