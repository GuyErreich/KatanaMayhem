using UnityEngine;

namespace KatanaMayhem.Scripts
{
    public class ColliderHit : MonoBehaviour {
        
        [SerializeField] private GameObject staticSlime;
        [SerializeField] private Colors.Types slimeColor;

        Rigidbody rb;

        private void Awake() {
            this.rb = this.GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other) {
            if (!other.gameObject.CompareTag("Panel"))
                return;

            Colors.Types color = other.gameObject.GetComponent<PanelData>().color;

            if (color == slimeColor)
            {
                var rotation = Quaternion.FromToRotation(Vector3.forward, other.GetContact(0).normal);
                Instantiate(staticSlime, other., rotation);
                Destroy(this.gameObject);
            }
        }
    }
}