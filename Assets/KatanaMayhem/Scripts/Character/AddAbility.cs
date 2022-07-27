using UnityEngine;
using UnityEngine.Events;
using KatanaMayhem.Scripts;

namespace KatanaMayhem.Character.Scripts {
    public class AddAbility : MonoBehaviour {
        public UnityEvent addGreenSlime;

        private void OnTriggerEnter(Collider collider) {
            if (!collider.CompareTag("Tablet"))
                return;

            Colors.Types color = collider.gameObject.GetComponent<TabletData>().color;

            if (color == Colors.Types.Green) {
                addGreenSlime?.Invoke();
                Destroy(collider.gameObject);
            }
        }
    }
}