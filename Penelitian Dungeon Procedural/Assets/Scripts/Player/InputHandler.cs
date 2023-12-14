using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DungeonLiberation
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        public bool dash_input;
        public bool skill_input;
        public bool ulti_input;
        public bool jump_input;
        public bool interact_input;
        public bool inventory_input;

        public bool slot1_input;
        public bool slot2_input;
        public bool slot3_input;
        public bool slot4_input;

        public bool rollFlag;
        public bool sprintFlag;
        public bool comboFlag;
        public bool inventoryFlag;
        public float rollInputTimer;

        PlayerControls inputActions;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        PlayerManager playerManager;
        UIManager uiManager;

        Vector2 movementInput;
        Vector2 cameraInput;

        private void Awake()
        {
            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
            playerManager = GetComponent<PlayerManager>();
            uiManager = FindObjectOfType<UIManager>();
        }

        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            }

            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float delta)
        {
            MoveInput(delta);
            HandleRollInput(delta);
            HandleAttackInput(delta);
            HandleQuickSlotInput();
            HandleInteractInput();
            HandleJumpInput();
            HandleInventoryInput();
        }
        private void MoveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }
        private void HandleRollInput(float delta)
        {
            dash_input = inputActions.PlayerActions.Roll.IsPressed();

            if (dash_input)
            {
                rollInputTimer += delta;
                sprintFlag = true;
            }
            else
            {
                if (rollInputTimer > 0 && rollInputTimer < 0.5f)
                {
                    sprintFlag = false;
                    rollFlag = true;
                }

                rollInputTimer = 0;
            }
        }
        private void HandleAttackInput(float delta)
        {
            inputActions.PlayerActions.Skill.performed += i => skill_input = true;
            inputActions.PlayerActions.Ultimate.performed += i => ulti_input = true;

            if (skill_input)
            {
                if (playerManager.canDoCombo)
                {
                    comboFlag = true;
                    playerAttacker.HandleWeaponCombo(playerInventory.rightWeapon);
                    comboFlag = false;
                }
                else
                {
                    if (playerManager.isInteracting)
                    {
                        return;
                    }

                    if (playerManager.canDoCombo)
                    {
                        return;
                    }
                    playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
                }
            }

            if (ulti_input)
            {
                playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
            }
        }
        private void HandleQuickSlotInput()
        {
            inputActions.PlayerQuickSlots.Slot1.performed += i => slot1_input = true;
            inputActions.PlayerQuickSlots.Slot2.performed += i => slot2_input = true;

            if (slot1_input)
            {
                playerInventory.ChangeRightWeapon();
            }
            else if (slot2_input)
            {
                playerInventory.ChangeLeftWeapon();
            }
        }
        private void HandleInteractInput()
        {
            inputActions.PlayerActions.Interact.performed += i => interact_input = true;

        }
        private void HandleJumpInput()
        {
            inputActions.PlayerActions.Jump.performed += i => jump_input = true;
        }
        private void HandleInventoryInput()
        {
            inputActions.PlayerActions.Inventory.performed += i => inventory_input = true;

            if (inventory_input)
            {
                inventoryFlag = !inventoryFlag;

                if (inventoryFlag)
                {
                    uiManager.OpenSelectWindow();
                }

                if (!inventoryFlag)
                {
                    uiManager.CloseSelectWindow();
                }
            }
        }
    }
}
