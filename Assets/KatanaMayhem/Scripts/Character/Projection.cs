using UnityEngine;
using KatanaMayhem.Scripts;

namespace KatanaMayhem.Scripts.Character {
    public class Projection : MonoBehaviour
    {
        [SerializeField] private Transform slime;
        [SerializeField] private Transform startingPoint;
        [SerializeField] private LineRenderer line;
        [SerializeField] private int maxPhysicsFrameIterations = 100;

        private bool isAming;

        private void Update() {
            if (this.isAming)
                this.SimulateTrajectory();
            else
                this.line.positionCount = 0;
        }

        public void ReceiveInput(bool isAming)
        {
            this.isAming = isAming;
        }

        private void SimulateTrajectory() {
            var ghostObj = PhysicsSceneManager.AddObject(slime.gameObject);

            var startingVelocity = this.GetComponent<JellyShoot>().Speed;
            ghostObj.transform.position = startingPoint.position;
            ghostObj.GetComponent<Rigidbody>().AddForce(startingVelocity, ForceMode.VelocityChange);

            this.line.positionCount = this.maxPhysicsFrameIterations;

            for (int i = 0; i < this.maxPhysicsFrameIterations; i++)
            {
                PhysicsSceneManager.Simulate(Time.fixedDeltaTime);   
                this.line.SetPosition(i, ghostObj.transform.position);
            }

            Destroy(ghostObj.gameObject);
        }
    }
}
