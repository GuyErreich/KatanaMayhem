using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KatanaMayhem.Character.Scripts
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator anim;

        private Vector2 movement;

        // Update is called once per frame
        void Update()
        {
            if(this.movement != Vector2.zero)
            {
                print("walking");
                anim.SetBool("isWalking", true);
            }
            else
                anim.SetBool("isWalking", false);
        }

        public void ReceiveInput(Vector2 movement) {
            this.movement = movement;
        }
    }
}
