using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KatanaMayhem.Character.Scripts
{
    [RequireComponent(typeof(JellyShoot))]
    [RequireComponent(typeof(LineRenderer))]
    public class AimTrajectory : MonoBehaviour
    {
        [SerializeField] private Transform startingPoint;
        [SerializeField] private int numPoints = 50;
        [SerializeField] private float timeBetweenPoints = 0.1f;
        [SerializeField] private LayerMask CollidableLayers;
        [SerializeField] private LineRenderer lineRenderer;


        private Vector3 startingVelocity;
        private bool isAming;

        private void Update() {
            if (this.isAming)
                this.Draw();
            else
                this.lineRenderer.positionCount = 0;
        }

        private void Draw() {
            this.startingVelocity = this.GetComponent<JellyShoot>().Speed;
            this.lineRenderer.positionCount = this.numPoints;
            var points = new List<Vector3>();

            for (float t = 0; t < this.numPoints; t += this.timeBetweenPoints) {
                var newPoint = this.startingPoint.position + t * this.startingVelocity;
                newPoint.y = this.startingPoint.position.y + this.startingVelocity.y * t + (Physics.gravity.y * t * t) / 2;
                points.Add(newPoint);

                if (Physics.OverlapSphere(newPoint, 2, this.CollidableLayers).Length > 0) {
                    this.lineRenderer.positionCount = points.Count;
                    break;
                }
            }

            this.lineRenderer.SetPositions(points.ToArray());
        }

        public void ReceiveInput(bool isAming)
        {
            this.isAming = isAming;
        }
    }
}