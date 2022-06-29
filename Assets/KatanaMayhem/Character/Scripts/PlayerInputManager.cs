using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace KatanaMayhem.Character.Scripts
{    
    [RequireComponent(typeof(MovementController))]
    [RequireComponent(typeof(PlayerAnimationController))]
    public class PlayerInputManager : MonoBehaviour
    {
        private PlayerControls controls;
        private PlayerControls.CharacterActions characterInput;

        private MovementController movementController;
        private PlayerAnimationController animationController;
        private JellyShoot jellyShoot;

        private Vector2 movement;
        private bool isJumping;

        private void Awake()
        {
            //to lock in the centre of window
            Cursor.lockState = CursorLockMode.Locked;
            //to hide the curser
            Cursor.visible = false;

            this.movementController = this.GetComponent<MovementController>();
            this.animationController = this.GetComponent<PlayerAnimationController>();
            this.jellyShoot = this.GetComponent<JellyShoot>();

            this.controls = new PlayerControls();
            characterInput = this.controls.Character;

            this.characterInput.Movement.performed += ctx => this.movement = ctx.ReadValue<Vector2>();
            this.characterInput.Shoot.performed += ctx => this.jellyShoot.Shoot();
            this.characterInput.Jump.performed += ctx => this.isJumping = ctx.ReadValueAsButton();
        }

        private void Update() 
        {
            this.movementController.ReceiveInput(this.movement, this.isJumping);
            this.animationController.ReceiveInput(this.movement);
        }

        private void OnEnable() {
            controls.Enable();
        }

        private void OnDestroy() {
            controls.Disable();
        }
    }
}
