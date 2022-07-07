using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KatanaMayhem.Scripts;
using KatanaMayhem.Scripts.DataObjects;

namespace KatanaMayhem.Character.Scripts
{
    [RequireComponent(typeof(SlimeData))]
    public class JellyShoot : MonoBehaviour
    {
        // [SerializeField] private GameObject refShoot;
        [SerializeField] private Transform shootAnchor, aimCaster;
        [SerializeField] private float speed;
        [SerializeField] private LayerMask mask;
        [SerializeField] private SlimeStorage SlimesStack;


        private GameObject sphere;
        private SlimeData slimeData;

        private void Awake() {
            // this.sphere = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere));
            this.slimeData = this.GetComponent<SlimeData>();
        }

        // private void Update() {
        //     Ray ray = new Ray(this.aimCaster.position, this.aimCaster.forward);
        //     RaycastHit hit;
        //     if (Physics.Raycast(ray, out hit, 500f, mask)) 
        //     {
        //         this.sphere.transform.position = hit.point;
        //         this.sphere.transform.up = hit.normal;
        //     }
        // }


        public void Shoot() 
        {
            var color = this.slimeData.color;
            var valueIndex = this.SlimesStack.Keys.IndexOf(color);
            GameObject refShoot = this.SlimesStack.Values[valueIndex];
            var rb = Instantiate(refShoot, shootAnchor.position, shootAnchor.rotation).GetComponent<Rigidbody>();
            rb.AddForce(Camera.main.transform.forward * this.speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}