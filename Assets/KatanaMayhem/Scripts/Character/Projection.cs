using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KatanaMayhem.Character.Scripts {
    public class Projection : MonoBehaviour
    {
        [SerializeField] private Transform obstaclesParent;
        [SerializeField] private Transform slime;
        [SerializeField] private Transform startingPoint;
        [SerializeField] private LineRenderer line;
        [SerializeField] private int maxPhysicsFrameIterations = 100;

        private Scene simulationScene;
        private PhysicsScene physicsScene;
        private bool isAming;

        private void Awake() {
            this.CreatePhysicsScene();
        }

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

        private void CreatePhysicsScene() {
            this.simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
            this.physicsScene = this.simulationScene.GetPhysicsScene();

            foreach (Transform obj in this.obstaclesParent) {
                var ghostObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
                ghostObj.GetComponent<Renderer>().enabled = false;
                SceneManager.MoveGameObjectToScene(ghostObj, this.simulationScene);
            }
        }

        private void SimulateTrajectory() {
            var ghostObj = Instantiate(slime.gameObject, startingPoint.position, slime.rotation);
            ghostObj.GetComponent<Renderer>().enabled = false;
            SceneManager.MoveGameObjectToScene(ghostObj.gameObject, this.simulationScene);

            var startingVelocity = this.GetComponent<JellyShoot>().Speed;
            ghostObj.GetComponent<Rigidbody>().AddForce(startingVelocity, ForceMode.VelocityChange);

            this.line.positionCount = this.maxPhysicsFrameIterations;

            for (int i = 0; i < this.maxPhysicsFrameIterations; i++)
            {
                this.physicsScene.Simulate(Time.deltaTime);   
                this.line.SetPosition(i, ghostObj.transform.position);
            }

            Destroy(ghostObj.gameObject);
        }

        // private void OnTriggerEnter(Collider collider) {
        //     var ghostObj = Instantiate(collider.gameObject, collider.gameObject.transform.position, collider.gameObject.transform.rotation);
        //     ghostObj.GetComponent<Renderer>().enabled = false;
        //     SceneManager.MoveGameObjectToScene(ghostObj, simulationScene);
        // }
    }
}
