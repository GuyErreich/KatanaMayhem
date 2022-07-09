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
        private Trajectory trajectory;

        private Vector2 movement;
        private bool isJumping, isRunnig;

        private void Awake()
        {
            //to lock in the centre of window
            Cursor.lockState = CursorLockMode.Locked;
            //to hide the curser
            Cursor.visible = false;

            this.movementController = this.GetComponent<MovementController>();
            this.animationController = this.GetComponent<PlayerAnimationController>();
            this.jellyShoot = this.GetComponent<JellyShoot>();
            this.trajectory = this.GetComponent<Trajectory>();

            this.controls = new PlayerControls();
            characterInput = this.controls.Character;

            this.characterInput.Movement.performed += ctx => this.movement = ctx.ReadValue<Vector2>();
            this.characterInput.Run.performed += ctx => this.isRunnig = ctx.ReadValueAsButton();
            this.characterInput.Jump.started += ctx => this.isJumping = ctx.ReadValueAsButton();
            this.characterInput.Jump.canceled += ctx => this.isJumping = ctx.ReadValueAsButton();
            this.characterInput.Shoot.performed += ctx => this.jellyShoot.Shoot();
            this.characterInput.Shoot.performed += ctx => this.animationController.Point();
            this.characterInput.Aim.performed += ctx => this.trajectory.Draw();
        }

        private void Update() 
        {
            this.movementController.ReceiveInput(this.movement, this.isRunnig, this.isJumping);
            this.animationController.ReceiveInput(this.movement, this.isRunnig);
        }

        private void OnEnable() {
            controls.Enable();
        }

        private void OnDestroy() {
            controls.Disable();
        }
    }
}
