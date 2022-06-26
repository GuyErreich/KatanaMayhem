using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KatanaMayhem.Character.Scripts
{
    public class JellyShoot : MonoBehaviour
    {
        [SerializeField] private GameObject refShoot;
        [SerializeField] private Transform shootAnchor, aimCaster;
        [SerializeField] private float speed;
        [SerializeField] private LayerMask mask;


        private GameObject sphere;

        private void Awake() {
            this.sphere = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere));
        }

        private void Update() {
            Ray ray = new Ray(this.aimCaster.position, this.aimCaster.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 500f, mask)) 
            {
                this.sphere.transform.position = hit.point;
                this.sphere.transform.up = hit.normal;
            }
        }


        public void Shoot() 
        {
            var rb = Instantiate(this.refShoot, shootAnchor.position, shootAnchor.rotation).GetComponent<Rigidbody>();
            rb.AddForce(Camera.main.transform.forward * this.speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}