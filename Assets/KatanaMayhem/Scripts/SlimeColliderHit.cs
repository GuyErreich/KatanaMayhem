using UnityEngine;

namespace KatanaMayhem.Scripts
{
    public class SlimeColliderHit : MonoBehaviour {
        
        [SerializeField] private GameObject staticSlime;
        [SerializeField] private float offset;

        private void OnTriggerEnter(Collider collider) {
            if (!collider.gameObject.CompareTag("Panel"))
                return;

            var collisionPoint = collider.ClosestPoint(transform.position);
            var collisionNormal = transform.position - collisionPoint;

            var rotation = Quaternion.FromToRotation(Vector3.forward, collisionNormal);
            var hardendSlime = Instantiate(staticSlime, collisionPoint, rotation);
            hardendSlime.transform.position += hardendSlime.transform.forward * this.offset;
            Destroy(this.gameObject);
        }
    }
}