using UnityEngine;
using System.Collections;

namespace JSukoAnimals
{
    public class GoatCharacter : MonoBehaviour
    {
        Animator goatAnimator;
        public bool jumpStart = false;
        public float groundCheckDistance = 0.6f;
        public float groundCheckOffset = 0.01f;
        public bool isGrounded = true;
        public float jumpSpeed = 1f;
        Rigidbody goatRigid;
        public float forwardSpeed;
        public float turnSpeed;
        public float walkMode = 1f;
        public float jumpStartTime = 0f;
        public float maxWalkSpeed = 1f;

        public bool canJump = true;
        public bool isLived = true;

        void Start()
        {
            goatAnimator = GetComponent<Animator>();
            goatRigid = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            CheckGroundStatus();
            Move();
            jumpStartTime += Time.deltaTime;
            maxWalkSpeed = Mathf.Lerp(maxWalkSpeed, walkMode, Time.deltaTime);
        }

        public void Attack()
        {
            goatAnimator.SetTrigger("Attack");
        }

        public void Hit()
        {
            goatAnimator.SetTrigger("Hit");
        }

        public void Death()
        {
            goatAnimator.SetBool("IsLived", false);
            isLived = false;
        }

        public void Rebirth()
        {
            goatAnimator.SetBool("IsLived", true);
            isLived = true;
        }

        public void EatStart()
        {
            goatAnimator.SetBool("IsEating", true);
            canJump = false;
        }
        public void EatEnd()
        {
            goatAnimator.SetBool("IsEating", false);
            canJump = true;
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
            if (isGrounded && canJump && isLived)
            {
                goatAnimator.SetTrigger("Jump");
                jumpStart = true;
                jumpStartTime = 0f;
                isGrounded = false;
            }
        }

        void CheckGroundStatus()
        {
            RaycastHit hitInfo;
            isGrounded = Physics.Raycast(transform.position + (transform.up * groundCheckOffset), Vector3.down, out hitInfo, groundCheckDistance);

            if (jumpStart)
            {
                if (jumpStartTime > .1f)
                {
                    jumpStart = false;
                    goatRigid.AddForce((transform.up + transform.forward * forwardSpeed * .3f) * jumpSpeed, ForceMode.Impulse);
                    goatAnimator.applyRootMotion = false;
                    //goatRigid.interpolation = RigidbodyInterpolation.None;
                    goatAnimator.SetBool("IsGrounded", false);
                }
            }

            if (isGrounded && !jumpStart && jumpStartTime > .5f)
            {
                goatAnimator.applyRootMotion = true;
                //goatRigid.interpolation = RigidbodyInterpolation.Extrapolate;
                goatAnimator.SetBool("IsGrounded", true);
            }
            else
            {
                if (!jumpStart)
                {
                    goatAnimator.applyRootMotion = false;
                    //goatRigid.interpolation = RigidbodyInterpolation.None;
                    goatAnimator.SetBool("IsGrounded", false);
                }
            }
        }

        public void Move()
        {
            goatAnimator.SetFloat("Forward", forwardSpeed);
            goatAnimator.SetFloat("Turn", turnSpeed);
        }
    }
}