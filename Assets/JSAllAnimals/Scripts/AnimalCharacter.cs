using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JSukoAnimals
{
    public class AnimalCharacter : MonoBehaviour
    {
        public Animator animalAnimator;
        public float forwardSpeed;
        public float turnSpeed;

        public bool jumpStart = false;
        public float groundCheckDistance = 0.6f;
        public float groundCheckOffset = 0.01f;
        public bool isGrounded = true;
        public float jumpSpeed = 1f;
        public float jumpStartTime = 0f;

        Rigidbody animalRigid;
        public float walkMode = 1f;
        public float maxWalkSpeed = 1f;



        // Start is called before the first frame update
        void Start()
        {
            animalAnimator = GetComponent<Animator>();
            animalRigid = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {
            CheckGroundStatus();
            Move();
            maxWalkSpeed = Mathf.Lerp(maxWalkSpeed, walkMode, Time.deltaTime);
            jumpStartTime += Time.deltaTime;
        }
        public virtual void Attack()
        {
            animalAnimator.SetTrigger("Attack");
        }

        public virtual void Hit()
        {
            animalAnimator.SetTrigger("Hit");
        }


        public virtual void Death()
        {
            animalAnimator.SetBool("IsLived", false);
        }

        public virtual void Rebirth()
        {
            animalAnimator.SetBool("IsLived", true);
        }


        public virtual void SleepStart()
        {
            //animalAnimator.SetBool("IsHopping", true);
        }

        public virtual void SleepEnd()
        {
            //animalAnimator.SetBool("IsHopping", false);
        }

        public virtual void SitdownStart()
        {
            //animalAnimator.SetBool("IsHopping", true);
        }

        public virtual void SitdownEnd()
        {
            //animalAnimator.SetBool("IsHopping", false);
        }


        public virtual void Threat()
        {
            //animalAnimator.SetTrigger("Threat");
        }

        public virtual void HopStart()
        {
            //animalAnimator.SetBool("IsHopping", true);
        }

        public virtual void HopEnd()
        {
            //animalAnimator.SetBool("IsHopping", false);
        }

        public virtual void DiagonallyWalkStart()
        {
            //animalAnimator.SetBool("IsDiagonally", true);
        }

        public virtual void DiagonallyWalkEnd()
        {
            //animalAnimator.SetBool("IsDiagonally", false);
        }

        public virtual void Eat()
        {
            //animalAnimator.SetTrigger("Eat");
        }

        public virtual void EatStart()
        {
            //animalAnimator.SetBool("IsEating", true);
        }
        public virtual void EatEnd()
        {
            //animalAnimator.SetBool("IsEating", false);
        }

        public virtual void Cry()
        {
            //animalAnimator.SetTrigger("Roar");
        }

        public virtual void DrinkStart()
        {

        }

        public virtual void DrinkEnd()
        {

        }
        public virtual void HeadShake()
        {

        }



        public void Gallop()
        {
            walkMode = 4f;
        }

        public void Canter()
        {
            walkMode = 3f;
        }

        public void Trot()
        {
            walkMode = 2f;
        }

        public void Walk()
        {
            walkMode = 1f;
        }

        public void Jump()
        {
            if (isGrounded)
            {
                animalAnimator.SetTrigger("Jump");
                jumpStart = true;
                jumpStartTime = 0f;
                isGrounded = false;
                animalAnimator.SetBool("IsGrounded", false);
            }
        }

        void CheckGroundStatus()
        {
            RaycastHit hitInfo;
            isGrounded = Physics.Raycast(transform.position + (transform.up * groundCheckOffset), Vector3.down, out hitInfo, groundCheckDistance);

            if (jumpStart)
            {
                if (jumpStartTime > .25f)
                {
                    jumpStart = false;
                    animalRigid.AddForce((transform.up + transform.forward * forwardSpeed) * jumpSpeed, ForceMode.Impulse);
                    animalAnimator.applyRootMotion = false;
                    //    animalRigid.interpolation = RigidbodyInterpolation.None;
                    animalAnimator.SetBool("IsGrounded", false);
                }
            }

            if (isGrounded && !jumpStart && jumpStartTime > .5f)
            {
                animalAnimator.applyRootMotion = true;
                //  animalRigid.interpolation = RigidbodyInterpolation.Extrapolate;
                animalAnimator.SetBool("IsGrounded", true);
            }
            else
            {
                if (!jumpStart)
                {
                    animalAnimator.applyRootMotion = false;
                    //     animalRigid.interpolation = RigidbodyInterpolation.None;
                    animalAnimator.SetBool("IsGrounded", false);
                }
            }
        }




        public virtual void Move()
        {
            animalAnimator.SetFloat("Forward", forwardSpeed);
            animalAnimator.SetFloat("Turn", turnSpeed);
        }

    }
}
