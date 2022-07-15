using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;


namespace KatanaMayhem.Scripts {
    public class ControlWeightWithCurve : MonoBehaviour
    {
        [SerializeField] private Animator anim;
        [SerializeField] private float maxPositionWeight = 1f;
        [SerializeField] private float maxRotationWeight = 1f;
        [SerializeField] private float maxHintWeight = 1f;
        private TwoBoneIKConstraint rig;

        private void Awake() {
            this.rig = this.GetComponent<TwoBoneIKConstraint>();
        }

        // Update is called once per frame
        void Update()
        {
            var weight = this.anim.GetFloat("IKRightHand");
            this.rig.data.targetPositionWeight = Mathf.Clamp(weight, 0f, maxPositionWeight);
            this.rig.data.targetRotationWeight = Mathf.Clamp(weight, 0f, maxRotationWeight);
            this.rig.data.hintWeight = Mathf.Clamp(weight, 0f, maxHintWeight);
        }
    }
}
