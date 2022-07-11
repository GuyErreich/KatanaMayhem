using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KatanaMayhem.Character.Scripts
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator anim;

        private Vector2 movement;
        private bool isRunning = false, isJumping = false, isPointing = false;

        // Update is called once per frame
        void Update() {
            this.Movement();
            this.Point();
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

        public void ReceiveInput(Vector2 movement, bool isRunning, bool isPointing) {
            this.movement = movement;
            this.isRunning = isRunning;
            this.isPointing = isPointing;
        }
    }
}
