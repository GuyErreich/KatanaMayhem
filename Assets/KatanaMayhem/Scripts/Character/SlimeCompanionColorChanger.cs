using UnityEngine;
using UnityEngine.VFX;
using KatanaMayhem.Scripts;
using KatanaMayhem.Scripts.DataObjects;

namespace KatanaMayhem.Character.Scripts {
    [RequireComponent(typeof(JellyShoot))]
    public class SlimeCompanionColorChanger : MonoBehaviour {
        [SerializeField] private Transform Anchor;

        private SlimeStorage slimesStack;
        private Colors.Types currentColor;
        private GameObject previousSlime;
        private JellyShoot jellyShoot;

        private void Awake() {
            this.jellyShoot = this.GetComponent<JellyShoot>();
            this.slimesStack = this.jellyShoot.SlimesStack;
            this.currentColor = this.jellyShoot.SlimeColor;
            this.SpawnSlime();
        }

        private void Update() {
            this.HandleColorChange();
        }

        private void HandleColorChange() {
            if (this.currentColor !=  this.jellyShoot.SlimeColor) {
                this.currentColor = this.jellyShoot.SlimeColor;
                this.SpawnSlime();
            }
        }

        private void SpawnSlime() {
            var valueIndex = this.slimesStack.Keys.IndexOf(this.currentColor);
            var slime = this.slimesStack.Values[valueIndex];

            if (this.previousSlime != null)
                Destroy(this.previousSlime);

            this.previousSlime = Instantiate(slime, Anchor);
            Destroy(this.previousSlime.GetComponent<Rigidbody>());
            Destroy(this.previousSlime.GetComponent<SphereCollider>());
            this.previousSlime.GetComponent<VisualEffect>().enabled = true;
            this.previousSlime.transform.localPosition = Vector3.zero;
        }

    }
}