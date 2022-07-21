using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace KatanaMayhem.Character.Scripts
{    
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(JellyShoot))]
    public class PlayerInputManager : MonoBehaviour
    {
        private PlayerControls controls;
        private PlayerControls.CharacterActions characterInput;
        private PlayerController playerController;
        private JellyShoot jellyShoot;
        private AimTrajectory aimTrajectory;

        private Vector2 movement;
        private bool isJumping, isRunnig, isAming, isShooting;

        private void Awake()
        {
            //to lock in the centre of window
            Cursor.lockState = CursorLockMode.Locked;
            //to hide the curser
            Cursor.visible = false;

            this.playerController = this.GetComponent<PlayerController>();
            this.jellyShoot = this.GetComponent<JellyShoot>();
            this.aimTrajectory = this.GetComponent<AimTrajectory>();

            this.controls = new PlayerControls();
            characterInput = this.controls.Character;

            this.characterInput.Movement.performed += ctx => this.movement = ctx.ReadValue<Vector2>();
            this.characterInput.Run.performed += ctx => this.isRunnig = ctx.ReadValueAsButton();
            this.characterInput.Jump.started += ctx => this.isJumping = ctx.ReadValueAsButton();
            this.characterInput.Jump.canceled += ctx => this.isJumping = ctx.ReadValueAsButton();
            this.characterInput.Shoot.performed += ctx => this.isShooting = ctx.ReadValueAsButton();
            this.characterInput.Shoot.performed += ctx => this.isAming = ctx.ReadValueAsButton();
        }

        private void Update() 
        {
            this.playerController.ReceiveInput(this.movement, this.isRunnig, this.isJumping, this.isShooting);
            this.jellyShoot.ReceiveInput(this.isShooting);
            this.aimTrajectory.ReceiveInput(this.isAming);
        }

        private void OnEnable() {
            controls.Enable();
        }

        private void OnDestroy() {
            controls.Disable();
        }
    }
}
