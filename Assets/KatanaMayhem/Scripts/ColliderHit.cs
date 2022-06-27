using UnityEngine;

namespace KatanaMayhem.Scripts
{
    public class ColliderHit : MonoBehaviour {
        
        [SerializeField] private GameObject staticSlime;

        Rigidbody rb;

        private void Awake() {
            this.rb = this.GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision other) {
            var rotation = Quaternion.FromToRotation(Vector3.forward, other.GetContact(0).normal);
            Instantiate(staticSlime, other.GetContact(0).point, rotation);
            Destroy(this.gameObject);
        }
    }
}