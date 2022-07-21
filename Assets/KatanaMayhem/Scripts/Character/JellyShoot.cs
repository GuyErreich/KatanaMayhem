using UnityEngine;
using KatanaMayhem.Scripts;
using KatanaMayhem.Scripts.DataObjects;

namespace KatanaMayhem.Character.Scripts
{
    [RequireComponent(typeof(SlimeData))]
    public class JellyShoot : MonoBehaviour
    {
        [SerializeField] private Transform shootAnchor, aimCaster;
        [SerializeField] private float maxForce = 1000f;
        [SerializeField] private float forceStartPercentage = 0.1f;
        [SerializeField] private float forceGrowthPercentage = 0.1f;
        [SerializeField] private LayerMask mask;
        [SerializeField] private SlimeStorage SlimesStack;


        private GameObject sphere;
        private SlimeData slimeData;
        private Colors.Types baseColor;
        private bool isShooting;
        private float forcePercentage;

        public Vector3 Speed { get; private set;}

        private void Awake() {
            this.slimeData = this.GetComponent<SlimeData>();
            this.baseColor =  this.slimeData.color;
            this.forcePercentage = this.forceStartPercentage;
            // this.sphere = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere));
        }

        private void Update() {
            if (this.isShooting) {
                print(this.forcePercentage);
                this.forcePercentage = Mathf.Lerp(this.forcePercentage, 1f, this.forceGrowthPercentage * Time.deltaTime);
            }
            else {
                this.forcePercentage = this.forceStartPercentage;
            }
        }

        private void FixedUpdate() {
            if (this.isShooting) {
                Ray ray = new Ray(this.aimCaster.position, this.aimCaster.forward);
                RaycastHit hit;
                Vector3 endpoint;
                float rayMaxDistance = 500f;

                if (Physics.Raycast(ray, out hit, rayMaxDistance, this.mask, QueryTriggerInteraction.Collide)) {
                    endpoint = hit.point;
                }
                else {
                    endpoint = ray.origin + (ray.direction * rayMaxDistance);
                }

                this.Speed = (endpoint - this.shootAnchor.position).normalized * this.maxForce * this.forcePercentage * Time.fixedDeltaTime;
                // this.sphere.layer = LayerMask.NameToLayer("Player");
                // this.sphere.transform.position = endpoint;
            }
        }

        public void ShootPassive() {
            var color = this.slimeData.color;
            var valueIndex = this.SlimesStack.Keys.IndexOf(color);
            GameObject refShoot = this.SlimesStack.Values[valueIndex];

            var rb = Instantiate(refShoot, this.shootAnchor.position, this.shootAnchor.rotation).GetComponent<Rigidbody>();
            rb.AddForce(this.Speed, ForceMode.VelocityChange);

            this.slimeData.color = this.baseColor;
        }

        public void ReceiveInput(bool isShooting) {
            this.isShooting = isShooting;
        }
    }
}