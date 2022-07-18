using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KatanaMayhem.Character.Scripts
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator anim;

        private CharacterController charController;
        private MovementController movementController;

        private Vector2 movement;
        private bool isRunning = false, isJumping = false, isPointing = false;

        private void Awake() {
            this.charController = this.GetComponent<CharacterController>();
            this.movementController = this.GetComponent<MovementController>();
        }

        // Update is called once per frame
        void Update() {
            this.Movement();
            this.Point();
            this.Jump();
            this.isGrounded();
        }

        private void Movement() {
            var isMoving = this.movement != Vector2.zero ? true : false;

            this.anim.SetBool("isWalking", isMoving);
            this.anim.SetBool("isRunning", this.isRunning);
            
            this.anim.SetFloat("movementX", this.movement.x);
            this.anim.SetFloat("movementY", this.movement.y);
        }

        private  void Point() {
            this.anim.SetBool("Point", isPointing);
        }

        private void Jump() {
            if (this.movementController.IsJumping) {
                this.anim.SetTrigger("Jump");
                 this.anim.SetBool("isGrounded", false);
            }
        }

        private void isGrounded() {
            if(this.charController.isGrounded)
                this.anim.SetBool("isGrounded", true);
        }

        public void ReceiveInput(Vector2 movement, bool isRunning, bool isJumping, bool isPointing) {
            this.movement = movement;
            this.isRunning = isRunning;
            this.isJumping = isJumping;
            this.isPointing = isPointing;
        }
    }
}
