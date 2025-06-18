using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
    public class PlayerMovement : MonoBehaviour
    {
        public float walkSpeed = 5f;
        public float runSpeed = 8f;
        public float airWalkSpeed = 3f;
        public float jumpImpulse = 10f;

        private Rigidbody2D rb;
        private Animator animator;
        private TouchingDirections touching;

        private bool isMoving;
        private bool isRunning;
        private bool facingRight = true;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            touching = GetComponent<TouchingDirections>();
        }

        public void Move(Vector2 input)
        {
            isMoving = input != Vector2.zero;
            UpdateDirection(input.x);

            float speed = GetCurrentSpeed();
            rb.linearVelocity = new Vector2(input.x * speed, rb.linearVelocity.y);

            animator.SetBool("isMoving", isMoving);
            animator.SetFloat("yVelocity", rb.linearVelocity.y);
        }

        public void SetRunning(bool running)
        {
            isRunning = running;
            animator.SetBool("isRunning", running);
        }

        public void Jump()
        {
            if (touching.IsGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpImpulse);
                animator.SetTrigger("jump");
            }
        }

        private void UpdateDirection(float x)
        {
            if (x > 0 && !facingRight || x < 0 && facingRight)
            {
                facingRight = !facingRight;
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }
        }

        private float GetCurrentSpeed()
        {
            if (isMoving && !touching.IsOnWall && touching.IsGrounded)
            {
                return isRunning ? runSpeed : walkSpeed;
            }
            return airWalkSpeed;
        }
    }
