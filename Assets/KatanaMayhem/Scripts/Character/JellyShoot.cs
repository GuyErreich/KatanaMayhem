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
        // [SerializeField] private float delay = 0.1f;
        [SerializeField] private LayerMask mask;
        [SerializeField] private SlimeStorage SlimesStack;


        private GameObject sphere;
        private SlimeData slimeData;
        private Colors.Types baseColor;
        private bool isShooting;

        public Vector3 Speed { get; private set;}

        private void Awake() {
            this.slimeData = this.GetComponent<SlimeData>();
            this.baseColor =  this.slimeData.color;
        }

        private void FixedUpdate() {
            Ray ray = new Ray(this.aimCaster.position, this.aimCaster.forward);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 500f, mask);
            Speed = (hit.point - shootAnchor.position).normalized * speed * Time.fixedDeltaTime;
        }

        public void ShootPassive() {
            var color = this.slimeData.color;
            var valueIndex = this.SlimesStack.Keys.IndexOf(color);
            GameObject refShoot = this.SlimesStack.Values[valueIndex];

            var rb = Instantiate(refShoot, shootAnchor.position, shootAnchor.rotation).GetComponent<Rigidbody>();
            rb.AddForce(Speed, ForceMode.VelocityChange);

            this.slimeData.color = baseColor;
        }

        public void ReceiveInput(bool isShooting) {
            this.isShooting = isShooting;
        }
    }
}