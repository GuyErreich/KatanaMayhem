using UnityEngine;
using KatanaMayhem.Scripts;
using KatanaMayhem.Scripts.DataObjects;

namespace KatanaMayhem.Character.Scripts
{
    public class JellyShoot : MonoBehaviour
    {
        [SerializeField] private Colors.Types slimeColor;
        [SerializeField] private Transform shootAnchor, aimCaster;
        [SerializeField] private float maxForce = 1000f;
        [SerializeField] private float forceStartPercentage = 0.1f;
        [SerializeField] private float forceGrowthPercentage = 0.1f;
        [SerializeField] private LayerMask mask;
        [SerializeField] private SlimeStorage slimesStack;


        private GameObject sphere;
        private bool isShooting;
        private float forcePercentage;

        public SlimeStorage SlimesStack { 
            get => this.slimesStack; 
            private set => this.slimesStack = value;
        }
        
        public Colors.Types SlimeColor { 
            get => this.slimeColor; 
            set => this.slimeColor = value; 
        }

        public Vector3 Speed { get; private set;}

        private void Awake() {
            this.forcePercentage = this.forceStartPercentage;
        }

        private void Update() {
            if (this.isShooting) {
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
                
                // var valueIndex = this.SlimesStack.Keys.IndexOf(this.SlimeColor);
                // GameObject refShoot = this.SlimesStack.Values[valueIndex];
                // this.projection.SimulateTrajectory(refShoot, this.Speed);
                // this.sphere.layer = LayerMask.NameToLayer("Player");
                // this.sphere.transform.position = endpoint;
            }
        }

        public void ShootPassive() {
            var valueIndex = this.SlimesStack.Keys.IndexOf(this.SlimeColor);
            GameObject refShoot = this.SlimesStack.Values[valueIndex];

            var rb = Instantiate(refShoot, this.shootAnchor.position, this.shootAnchor.rotation).GetComponent<Rigidbody>();
            rb.AddForce(this.Speed, ForceMode.VelocityChange);
        }

        public void ReceiveInput(bool isShooting) {
            this.isShooting = isShooting;
        }
    }
}