using UnityEngine;

namespace KatanaMayhem.Scripts
{    
    public class FollowTargetWithOffset : MonoBehaviour {
        [SerializeField] private Transform target;
        [SerializeField] Vector3 offset;

        private void Update() {
            this.transform.forward = this.target.forward;
            this.transform.position = this.target.position;
            this.transform.position += (this.target.right * this.offset.x) + (this.target.up * this.offset.y) + (this.target.forward * this.offset.z);
        }
    }
}