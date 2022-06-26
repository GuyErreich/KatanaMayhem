using UnityEngine;

namespace KatanaMayhem.Scripts
{
    public class ColliderHit : MonoBehaviour {

        Rigidbody rb;

        private void Awake() {
            this.rb = this.GetComponent<Rigidbody>();
        }
        
        private void OnCollisionEnter(Collision other) {
            this.rb.isKinematic = true;
        }
    }
}