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

        private void OnTriggerEnter(Collider collider) {
            if (!collider.gameObject.CompareTag("Panel"))
                return;

            print("hit");

            Colors.Types color = collider.gameObject.GetComponent<PanelData>().color;

            if (color == slimeColor)
            {
                var collisionPoint = collider.ClosestPoint(transform.position);
                var collisionNormal = transform.position - collisionPoint;

                var rotation = Quaternion.FromToRotation(Vector3.forward, collisionNormal);
                Instantiate(staticSlime, collisionPoint, rotation);
                Destroy(this.gameObject);
            }
        }
    }
}