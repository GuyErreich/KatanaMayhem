using UnityEngine;
using KatanaMayhem.Scripts;
using KatanaMayhem.Scripts.DataObjects;

namespace KatanaMayhem.Character.Scripts {
    [RequireComponent(typeof(SlimeData))]
    public class SlimeCompanionColorChanger : MonoBehaviour {
        [SerializeField] private SlimeStorage SlimesStack;
        [SerializeField] private Transform Anchor;

        private Colors.Types currentColor;
        private GameObject previousSlime;
        private SlimeData slimeData;

        private void Awake() {
            this.slimeData = this.GetComponent<SlimeData>();
        }

        private void Start() {
            this.currentColor = this.slimeData.color;
            this.SpawnSlime();
        }

        private void Update() {
            this.HandleColorChange();
        }

        private void HandleColorChange() {
            if (this.currentColor != this.slimeData.color) {
                this.currentColor = this.slimeData.color;

                this.SpawnSlime();
            }
        }

        private void SpawnSlime() {
            print("shit");
            var valueIndex = this.SlimesStack.Keys.IndexOf(this.currentColor);
            var slime = this.SlimesStack.Values[valueIndex];

            if (this.previousSlime != null)
                Destroy(this.previousSlime);

            this.previousSlime = Instantiate(slime, Anchor);
            this.previousSlime.GetComponent<Rigidbody>().isKinematic = true;
            this.previousSlime.transform.localPosition = Vector3.zero;

        }

    }
}