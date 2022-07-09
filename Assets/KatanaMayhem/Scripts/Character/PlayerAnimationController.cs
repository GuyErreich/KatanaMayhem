using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KatanaMayhem.Character.Scripts
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator anim;

        Vector2 direction;

        private Vector2 movement;
        private bool isRunning = false, isJumping = false;

        // Update is called once per frame
        void Update()
        {
            var isMoving = this.movement != Vector2.zero ? true : false;

            this.anim.SetBool("isWalking", isMoving);
            this.anim.SetBool("isRunning", this.isRunning);
            
            this.anim.SetFloat("movementX", this.movement.x);
            this.anim.SetFloat("movementY", this.movement.y);
        }

        public void Point()
        {
            this.anim.SetTrigger("Point");
        }

        public void ReceiveInput(Vector2 movement, bool isRunning) {
            this.movement = movement;
            this.isRunning = isRunning;
        }
    }
}
