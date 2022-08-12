using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.Events; 
using KatanaMayhem.Scripts;
using KatanaMayhem.Scripts.DataObjects;

namespace KatanaMayhem.Character.Scripts
{    
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(JellyShoot))]
    public class PlayerInputManager : MonoBehaviour
    {
        [SerializeField] AbilityStorage abilityStorage;
        public UnityEvent<Colors.Types> SlimeChangeEvent;

        private PlayerControls controls;
        private PlayerControls.CharacterActions characterInput;
        private PlayerControls.SlimeRepoActions slimeRepoInput;
        private PlayerController playerController;
        private JellyShoot jellyShoot;
        private AimTrajectory aimTrajectory;
        private Projection projection;

        private Vector2 movement;
        private bool isJumping, isRunnig, isAming, isShooting;

        private void Awake()
        {
            //to lock in the centre of window
            Cursor.lockState = CursorLockMode.Locked;
            //to hide the curser
            Cursor.visible = false;

            this.controls = new PlayerControls();
            this.playerController = this.GetComponent<PlayerController>();
            this.jellyShoot = this.GetComponent<JellyShoot>();
            this.aimTrajectory = this.GetComponent<AimTrajectory>();
            this.projection = this.GetComponent<Projection>();

            this.CharacterInput();
            this.SlimeRepoInput();
        }

        private void Update() 
        {
            this.playerController.ReceiveInput(this.movement, this.isRunnig, this.isJumping, this.isShooting);
            this.jellyShoot.ReceiveInput(this.isShooting);
            // this.aimTrajectory.ReceiveInput(this.isAming);
            this.projection.ReceiveInput(this.isAming);

        }

        private void CharacterInput() {
            this.characterInput = this.controls.Character;

            this.characterInput.Movement.performed += ctx => this.movement = ctx.ReadValue<Vector2>();
            this.characterInput.Run.performed += ctx => this.isRunnig = ctx.ReadValueAsButton();
            this.characterInput.Jump.started += ctx => this.isJumping = ctx.ReadValueAsButton();
            this.characterInput.Jump.canceled += ctx => this.isJumping = ctx.ReadValueAsButton();
            this.characterInput.Shoot.performed += ctx => this.isShooting = ctx.ReadValueAsButton();
            this.characterInput.Shoot.performed += ctx => this.isAming = ctx.ReadValueAsButton();
        }

        private void SlimeRepoInput() {
            this.slimeRepoInput = this.controls.SlimeRepo;

            this.slimeRepoInput.Purple.started += _ => {
                this.jellyShoot.SlimeColor = Colors.Types.Purple;
                
                SlimeChangeEvent?.Invoke(Colors.Types.Purple);
            };
            this.slimeRepoInput.Green.started += _ => {
                var index = this.abilityStorage.Keys.IndexOf(Colors.Types.Green);
                if (this.abilityStorage.Values[index])
                    this.jellyShoot.SlimeColor = Colors.Types.Green;
                
                SlimeChangeEvent?.Invoke(Colors.Types.Green);
            };
        }

        private void OnEnable() {
            controls.Enable();
        }

        private void OnDestroy() {
            controls.Disable();
        }
    }
}
