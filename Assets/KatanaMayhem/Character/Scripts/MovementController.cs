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
        [SerializeField]private float speed = 7.0f;
        [SerializeField]private float jumpForce = 7.0f;
        [SerializeField]private float gravity = 9.8f;
        // [SerializeField]private float max_slopes = 65f;
        

        // private Rigidbody rb;
        private CharacterController controller;
        private Vector3 velocity;
        private float ySpeed;
        private Vector2 movement;
        private bool isJumping;
        // private Quaternion slopAngle;
        // private bool isGrounded = true, isJumping = false;
        // private RaycastHit ground;

        // Start is called before the first frame update
        private void Awake()
        {
            this.controller = this.GetComponent<CharacterController>();
        }

        // Update is called once per frame
        private void Update()
        {
            this.velocity = (this.transform.right * this.movement.x) + (this.transform.forward * this.movement.y);
            this.velocity *= this.speed;

            // this.HandleSlop();
            this.HandleGravity();
            this.HandleRotation();
            this.HandleJump();

            this.velocity.y = this.ySpeed;

            this.controller.Move(this.velocity * Time.deltaTime);
        }

//         private void HandleSlop()
//         {
//             int layerMask = ~LayerMask.NameToLayer("Terrain");
//             RaycastHit hit;

//             Physics.Raycast(this.transform.position + (Vector3.up * 0.3f), Vector3.down, out hit, 0.5f, layerMask);

//             Quaternion slopAngle = Quaternion.FromToRotation(Vector3.up, hit.normal);
//             Vector3 velocityOnSurface = slopAngle * this.velocity;
            
//             this.velocity = velocityOnSurface;

// #if UNITY_EDITOR
//             Debug.DrawRay (this.transform.position, slopAngle * this.transform.forward, Color.red, 0.5f);
//             Debug.DrawRay (this.transform.position + (Vector3.up * 0.3f), Vector3.down * 0.4f, Color.red);
// #endif
//         }

        private void HandleGravity()
        {
            this.ySpeed += Physics.gravity.y * Time.deltaTime;
        }

        private void HandleRotation()
        {
            float rotationAngle = Mathf.LerpAngle(this.transform.eulerAngles.y, Camera.main.transform.eulerAngles.y, 2f * Time.deltaTime);
            this.transform.eulerAngles = new Vector3(0,rotationAngle,0);
        }

        private void HandleJump()
        {
            if (this.isJumping)
                this.ySpeed = jumpForce;
        }

        public void ReceiveInput(Vector2 movement, bool isJumping)
        {
            this.movement = movement;
            this.isJumping = isJumping;
        }
    }
}
