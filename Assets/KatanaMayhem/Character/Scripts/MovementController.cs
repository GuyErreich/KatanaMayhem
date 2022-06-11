using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KatanaMayhem.Character.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class MovementController : MonoBehaviour
    {

        [Header("Reference")]
        [SerializeField] private Transform lookAt;

        [Header("Stats")]
        [SerializeField]private float speed = 7.0f;
        [SerializeField]private float gravity = 9.8f;
        // [SerializeField]private float max_slopes = 65f;

        private Rigidbody rb;
        private Vector3 direction;
        private Vector2 movement;
        // private Quaternion slopAngle;
        private bool isGrounded = true;
        private RaycastHit ground;

        // Start is called before the first frame update
        private void Awake()
        {
            this.rb = this.GetComponent<Rigidbody>();
        }

        private void Update()
        {

            this.direction = (this.transform.right * this.movement.x) + (this.transform.forward * this.movement.y);

            this.CheckForGround();
            this.HandleSlop();
            this.HandleFalling();
            this.HandleRotation();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            this.rb.velocity = this.direction * this.speed * Time.fixedDeltaTime;
        }

        private void CheckForGround()
        {
            int layerMask = ~LayerMask.NameToLayer("Terrain");
            isGrounded = Physics.Raycast(this.transform.position, Vector3.down, out this.ground, 0.5f, layerMask);
            
                
#if UNITY_EDITOR
            Debug.DrawRay (this.transform.position, Vector3.down * 0.5f, Color.red);
#endif
        }

        private void HandleSlop()
        {
            Quaternion slopAngle = Quaternion.FromToRotation(Vector3.up, this.ground.normal);
            Vector3 directionOnSurface = slopAngle * this.direction;

            
#if UNITY_EDITOR
            Debug.DrawRay (this.transform.position, slopAngle * this.transform.forward, Color.red, 0.5f);
            Debug.Log(this.ground.normal.x);
#endif

            // if(this.ground.normal.x >= max_slopes)
            // {
            this.direction = directionOnSurface;
            // }
        }

        private void HandleFalling()
        {
            if(!isGrounded)
                this.direction = this.direction + (Vector3.down * gravity);
        }

        private void HandleRotation()
        {
            float rotationAngle = Mathf.LerpAngle(this.transform.eulerAngles.y, Camera.main.transform.eulerAngles.y, 2f * Time.deltaTime);
            this.transform.eulerAngles = new Vector3(0,rotationAngle,0);
        }

        public void ReceiveInput(Vector2 movement) {
            this.movement = movement;
        }
    }
}
