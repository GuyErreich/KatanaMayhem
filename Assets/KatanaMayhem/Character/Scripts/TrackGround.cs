using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KatanaMayhem.Character.Scripts
{
    public class TrackGround : MonoBehaviour
    {
        [SerializeField] private LayerMask ignoreLayers;
        [SerializeField, Range(0, 1)] private float upIndentation;
        [SerializeField, Range(0, 1)] private float distanceToGround;

        // Update is called once per frame
        void Update()
        {
            RaycastHit hit;
            Vector3 startPos = this.transform.position + (Vector3.up * this.upIndentation);
            Ray ray = new Ray(startPos, Vector3.down);

            if(Physics.Raycast(ray, out hit, ignoreLayers))
            {

                Vector3 footPosition = hit.point;
                footPosition.y += distanceToGround;
                this.transform.position = footPosition;

                var rotationX = Quaternion.FromToRotation(Vector3.up, hit.normal).eulerAngles.x;
                var rotationY = this.transform.eulerAngles.y;
                var rotationZ = Quaternion.FromToRotation(Vector3.up, hit.normal).eulerAngles.z;
                // var rotationZ = 0;
                
                
                if(this.transform.forward.z < 0)
                    rotationX = -rotationX;
                if(this.transform.right.x < 0)
                    rotationZ = -rotationZ;
                this.transform.eulerAngles = new Vector3(rotationX, rotationY, rotationZ);
            }
        }
    }
}
