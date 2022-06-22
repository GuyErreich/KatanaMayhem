using UnityEngine;
using System.Collections;

public class LocomotionUpdate : StateMachineBehaviour {
    [SerializeField] private float maxDistance = 0.7f;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) 
	{
		animator.GetComponent<MecFootPlacer>().EnablePlant(AvatarIKGoal.LeftFoot);
		animator.GetComponent<MecFootPlacer>().EnablePlant(AvatarIKGoal.RightFoot);		
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) 
	{

		float IKLeftFoot = animator.GetFloat("IKLeftFoot");
		float IKRightFoot = animator.GetFloat("IKRightFoot");

		FootPlacementData[] lFeet = animator.GetComponents<FootPlacementData>();
		FootPlacementData lFoot = null;

        //First foot setup start
        for (byte i = 0; i < lFeet.Length; i++)
       {
            switch (lFeet[i].mFootID)
            {
                case FootPlacementData.LimbID.LEFT_FOOT:
                    lFoot = lFeet[i];
                    lFoot.mExtraRayDistanceCheck = this.maxDistance * IKLeftFoot;         
                    break;

                case FootPlacementData.LimbID.RIGHT_FOOT:
                    lFoot = lFeet[i];
                    lFoot.mExtraRayDistanceCheck = this.maxDistance * IKRightFoot;         
                    break;
            }        
        }	
	}
}
