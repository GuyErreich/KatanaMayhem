using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KatanaMayhem.Character.Scripts
{
    public class IKFootPlacement : MonoBehaviour
    {    
        [SerializeField, Range(0, 1)]
        private float distanceToGround;

        [SerializeField]
        private Transform player;

        private Animator anim;

        // Start is called before the first frame update
        void Awake()
        {
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void OnAnimatorIK(int layerIndex) {
            anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);
            anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
            anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);

            RaycastHit hit;
            Ray ray = new Ray(anim.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);

            if(Physics.Raycast(ray, out hit, distanceToGround + 1, ~LayerMask.NameToLayer("Ignore Raycast")))
            {
                print(LayerMask.LayerToName(hit.transform.gameObject.layer));
                print(LayerMask.LayerToName(hit.transform.gameObject.layer));
                if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Terrain"))
                {
                    Vector3 footPosition = hit.point;
                    footPosition.y += distanceToGround;
                    
                    var rotationX = Quaternion.FromToRotation(transform.up, hit.normal);
                    // var rotationZ = Quaternion.FromToRotation(transform.forward, hit.normal);

                    Vector3 footForward = rotationX * transform.forward;

                    anim.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
                    anim.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(player.forward, hit.normal));
                    // anim.SetIKPosition()
                    Debug.DrawRay(anim.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down * (distanceToGround + 1f), Color.yellow);
                }
            }

            ray = new Ray(anim.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);

            if(Physics.Raycast(ray, out hit, distanceToGround + 1, ~LayerMask.NameToLayer("Ignore Raycast")))
            {
                print(LayerMask.LayerToName(hit.transform.gameObject.layer));
                print(LayerMask.LayerToName(hit.transform.gameObject.layer));
                if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Terrain"))
                {
                    Vector3 footPosition = hit.point;
                    footPosition.y += distanceToGround;
                    
                    var rotationX = Quaternion.FromToRotation(transform.up, hit.normal);
                    // var rotationZ = Quaternion.FromToRotation(transform.forward, hit.normal);

                    Vector3 footForward = rotationX * transform.forward;

                    anim.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);
                    anim.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(player.forward, hit.normal));
                    // anim.SetIKPosition()
                    Debug.DrawRay(anim.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down * (distanceToGround + 1f), Color.yellow);
                }
            }
        }
    }
}
