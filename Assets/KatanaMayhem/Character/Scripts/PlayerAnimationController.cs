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

        // Update is called once per frame
        void Update()
        {
            if(this.movement != Vector2.zero)
                anim.SetBool("isWalking", true);
            else
                anim.SetBool("isWalking", false);
            
            anim.SetFloat("movementX", this.movement.x);
            anim.SetFloat("movementY", this.movement.y);
        }

        public void ReceiveInput(Vector2 movement) {
            this.movement = movement;
        }
    }
}
