using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace KatanaMayhem.Character.Scripts
{    
    [RequireComponent(typeof(MovementController))]
    [RequireComponent(typeof(PlayerAnimationController))]
    [RequireComponent(typeof(JellyShoot))]
    public class PlayerInputManager : MonoBehaviour
    {
        private PlayerControls controls;
        private PlayerControls.CharacterActions characterInput;

        private MovementController movementController;
        private PlayerAnimationController animationController;
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

            this.movementController = this.GetComponent<MovementController>();
            this.animationController = this.GetComponent<PlayerAnimationController>();
            this.jellyShoot = this.GetComponent<JellyShoot>();
            this.aimTrajectory = this.GetComponent<AimTrajectory>();

            this.controls = new PlayerControls();
            characterInput = this.controls.Character;

            this.characterInput.Movement.performed += ctx => this.movement = ctx.ReadValue<Vector2>();
            this.characterInput.Run.performed += ctx => this.isRunnig = ctx.ReadValueAsButton();
            this.characterInput.Jump.started += ctx => this.isJumping = ctx.ReadValueAsButton();
            this.characterInput.Jump.canceled += ctx => this.isJumping = ctx.ReadValueAsButton();
            this.characterInput.Shoot.started += ctx => this.isShooting = ctx.ReadValueAsButton();
            this.characterInput.Shoot.canceled += ctx => this.isShooting = ctx.ReadValueAsButton();
            this.characterInput.Aim.performed += ctx => this.isAming = ctx.ReadValueAsButton();
        }

        private void Update() 
        {
            this.movementController.ReceiveInput(this.movement, this.isRunnig, this.isJumping);
            this.jellyShoot.ReceiveInput(this.isShooting);
            this.animationController.ReceiveInput(this.movement, this.isRunnig, this.isShooting);
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
