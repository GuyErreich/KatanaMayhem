using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KatanaMayhem.Scripts
{    
    public class PhysicsSceneManager : MonoBehaviour {
        public static PhysicsSceneManager instance;

        [SerializeField] private Transform obstaclesParent;

        private static Scene simulationScene;
        private static PhysicsScene physicsScene;
        private static Dictionary<GameObject, GameObject> interdimensionalObjectConnector =  new Dictionary<GameObject, GameObject>();

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                CreatePhysicsScene();
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void CreatePhysicsScene() {
            simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
            physicsScene = simulationScene.GetPhysicsScene();

            foreach (Transform obj in this.obstaclesParent) {
                PhysicsSceneManager.AddObject(obj.gameObject, cache: true);
            }
        }

        public static GameObject AddObject(GameObject obj, bool cache = false) {
            var ghostObj = Instantiate(obj, obj.transform.position, obj.transform.rotation);
            ghostObj.GetComponent<Renderer>().enabled = false;
            ghostObj.tag = "Untagged";
            SceneManager.MoveGameObjectToScene(ghostObj, simulationScene);

            if (cache) 
                interdimensionalObjectConnector.Add(obj, ghostObj);

            return ghostObj;
        }

        public static void Remove(GameObject obj) {
            var ghostObj = interdimensionalObjectConnector[obj];

            Destroy(obj);
            Destroy(ghostObj);
        }

        public static void Simulate(float deltaTime) {
            physicsScene.Simulate(deltaTime);
        }
    }
}