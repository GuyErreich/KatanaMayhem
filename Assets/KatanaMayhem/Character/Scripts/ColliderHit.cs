using UnityEngine;
using KatanaMayhem.Scripts;

namespace KatanaMayhem.Character.Scripts {
    [RequireComponent(typeof(SlimeData))]
    public class ColliderHit : MonoBehaviour {
        private SlimeData slimeData;

        private void Awake() {
            this.slimeData = this.GetComponent<SlimeData>();
        }


        private void OnTriggerEnter(Collider collider) {
            if (!collider.CompareTag("Tablet"))
                return;

            Colors.Types color = collider.gameObject.GetComponent<TabletData>().color;

            this.slimeData.color = color;
        }
    }
}