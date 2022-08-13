using UnityEngine;

namespace KatanaMayhem.Scripts.Character
{
    public class IKFootPlacement : MonoBehaviour
    {
        [SerializeField, Range(0f, 1f)]
        private float distanceToGround;

        [SerializeField, Range(0f, 1f)]
        private float footWeightPos, footWeightRot, footHintWeight;

        [SerializeField, Range(0f, 5f)]
        private float hightShift = 1f;

        [SerializeField, Range(0f, 90f)]
        private float AngleShift = 25f;

        private Animator anim;
        private RaycastHit hit;

        // Start is called before the first frame update
        void Awake()
        {
            this.anim = GetComponent<Animator>();
        }

        private void OnAnimatorIK(int layerIndex)
        {
            Ray ray = new Ray(this.anim.GetIKPosition(AvatarIKGoal.LeftFoot) + (Vector3.up * hightShift), Vector3.down);

            if(Physics.Raycast(ray, out this.hit, this.distanceToGround + 1, ~LayerMask.NameToLayer("Terrain")))
            {
                this.FootPlacment(AvatarIKGoal.LeftFoot);
                this.FootRotation(AvatarIKGoal.LeftFoot);
                this.HintPosition(AvatarIKHint.LeftKnee);
            }

            ray = new Ray(this.anim.GetIKPosition(AvatarIKGoal.RightFoot) + (Vector3.up * hightShift), Vector3.down);

            if(Physics.Raycast(ray, out this.hit, this.distanceToGround + 1, ~LayerMask.NameToLayer("Terrain")))
            {
                this.FootPlacment(AvatarIKGoal.RightFoot);
                this.FootRotation(AvatarIKGoal.RightFoot);
                this.HintPosition(AvatarIKHint.RightKnee);
            }
        }

        private void FootPlacment(AvatarIKGoal side)
        {
            this.anim.SetIKPositionWeight(side, this.footWeightPos);

            Vector3 footPosition = this.hit.point;
            footPosition.y += this.distanceToGround;
      
            this.anim.SetIKPosition(side, footPosition);

            Debug.DrawRay(this.anim.GetIKPosition(side) + (Vector3.up * hightShift), Vector3.down * (this.distanceToGround + 1f), Color.yellow);
        }

        void FootRotation(AvatarIKGoal side)
        {
            this.anim.SetIKRotationWeight(side, this.footWeightRot);

            Transform foot = side == AvatarIKGoal.LeftFoot ? this.anim.GetBoneTransform(HumanBodyBones.LeftFoot) : this.anim.GetBoneTransform(HumanBodyBones.RightFoot);

            this.anim.SetIKRotation(side, Quaternion.FromToRotation(Vector3.up, this.hit.normal));

            Debug.DrawRay(foot.position, this.hit.normal);
            Debug.DrawRay(foot.position, foot.up);
        }

        void HintPosition(AvatarIKHint side)
        {
            this.anim.SetIKHintPositionWeight(side, this.footHintWeight);

            Transform foot = side == AvatarIKHint.LeftKnee ? transform.Find("LeftFoot") : transform.Find("RightFoot");
            Vector3 hintPos = foot.position + foot.forward;
        }
    }
}
