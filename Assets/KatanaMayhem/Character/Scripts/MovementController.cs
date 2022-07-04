using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KatanaMayhem.Character.Scripts
{
    // [RequireComponent(typeof(Rigidbody))]
    // [RequireComponent(typeof(CapsuleCollider))]
    public class MovementController : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] private float speed = 7.0f;
        [SerializeField, Range(1.1f, 5f)] private float sprintMultiplier = 1.5f;
        [SerializeField] private float jumpForce = 7.0f;
        
        private CharacterController charController;
        private Vector3 velocity;
        private float ySpeed;
        private Vector2 movement;
        private bool isRunning, isJumping;

        // Start is called before the first frame update
        private void Awake()
        {
            this.charController = this.GetComponent<CharacterController>();
        }

        // Update is called once per frame
        private void Update()
        {
            this.HandleMovement();
            this.HandleGravity();
            this.HandleRotation();
            this.HandleJump();
        }

        private void HandleMovement()
        {
            var speedMultiplier = this.isRunning ? this.sprintMultiplier : 1;
            
            var direction = (this.transform.right * this.movement.x) + (this.transform.forward * this.movement.y);
            this.velocity = direction * this.speed * speedMultiplier;
            this.velocity.y = this.ySpeed;

            this.charController.Move(this.velocity * Time.deltaTime);
        }

        private void HandleGravity()
        {
            if (!this.charController.isGrounded)
                this.ySpeed += Physics.gravity.y * Time.deltaTime;
            else if(ySpeed < 0)
                ySpeed = 0;
        }

        private void HandleRotation()
        {
            float rotationAngle = Mathf.LerpAngle(this.transform.eulerAngles.y, Camera.main.transform.eulerAngles.y, 2f * Time.deltaTime);
            this.transform.eulerAngles = new Vector3(0,rotationAngle,0);
        }

        private void HandleJump()
        {
            if (this.isJumping)
            {
                this.ySpeed = jumpForce;
                this.isJumping = false;
            }
        }

        public void ReceiveInput(Vector2 movement, bool isRunning, bool isJumping)
        {
            this.movement = movement;
            this.isRunning = isRunning;
            this.isJumping = isJumping;
        }
    }
}
