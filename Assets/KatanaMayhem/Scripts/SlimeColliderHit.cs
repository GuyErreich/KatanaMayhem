using UnityEngine;

namespace KatanaMayhem.Scripts
{
    [RequireComponent(typeof(SlimeData))]
    public class SlimeColliderHit : MonoBehaviour {
        
        [SerializeField] private GameObject staticSlime;

        private Colors.Types color;

        private void Awake() {
            this.color = this.GetComponent<SlimeData>().color;
        }

        private void OnTriggerEnter(Collider collider) {
            if (!collider.gameObject.CompareTag("Panel"))
                return;

            Colors.Types targetColor = collider.gameObject.GetComponent<PanelData>().color;

            if (color == targetColor)
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