using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KatanaMayhem.Character.Scripts
{
    [RequireComponent(typeof(CharacterController))]
    public class MovementController : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] private float speed = 7f;
        [SerializeField] private float rotationSpeed = 2f;
        [SerializeField, Range(1.1f, 5f)] private float sprintMultiplier = 1.5f;
        [SerializeField] private float jumpForce = 7f, jumpGracePeriod = 0.2f;
        
        private CharacterController charController;
        private Vector3 velocity;
        private Vector2 movement;
        private float ySpeed;
        private float? lastGroundedTime, jumpButtonPressedTime;
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
            this.HandleJump();
        }

        private void FixedUpdate() {
            this.HandleRotation();
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
            float rotationAngle = Mathf.LerpAngle(this.transform.eulerAngles.y, Camera.main.transform.eulerAngles.y, rotationSpeed * Time.fixedDeltaTime);
            this.transform.eulerAngles = new Vector3(0,rotationAngle,0);
        }

        private void HandleJump() {
            if(this.charController.isGrounded)
                this.lastGroundedTime =  Time.time;

            if (this.isJumping)
                this.jumpButtonPressedTime = Time.time;

            // The same as checking ground but gives a little preiod where it still considers you grounded.
            // this gives the char a better jump interaction because most of the times people dont press the jump
            // button in the perfect right time to make the char jump again. 
            if (Time.time - this.lastGroundedTime <= this.jumpGracePeriod) {
                //This does the same but gives a little period while you are in the air 
                if (Time.time - this.jumpButtonPressedTime <= this.jumpGracePeriod) {
                    this.ySpeed = jumpForce;
                    this.isJumping = false;

                    this.lastGroundedTime = null;
                    this.jumpButtonPressedTime = null;
                }
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
