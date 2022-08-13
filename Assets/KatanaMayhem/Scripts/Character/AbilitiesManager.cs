using UnityEngine;
using KatanaMayhem.Scripts.DataObjects;

namespace KatanaMayhem.Scripts.Character
{    
    public class AbilitiesManager : MonoBehaviour {
        public AbilityStorage abilities;

        public void EnableBounceSlimeAbillity() {
            var index = this.abilities.Keys.IndexOf(Colors.Types.Green);
            this.abilities.Values[index] = true;
        }

        private void OnDestroy() {
            if (this.abilities.Values != null) {
                for (int i = 1; i < this.abilities.Keys.Count; i++) {
                    this.abilities.Values[i] = false;
                }
            }
        }
    }
}