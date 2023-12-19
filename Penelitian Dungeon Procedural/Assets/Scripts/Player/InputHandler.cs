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
        public bool lockOn_input;

        public bool lockLeftTarget_input;
        public bool lockRightTarget_input;

        public bool slot1_input;
        public bool slot2_input;
        public bool slot3_input;
        public bool slot4_input;

        public bool rollFlag;
        public bool sprintFlag;
        public bool comboFlag;
        public bool lockOnFlag;
        public bool inventoryFlag;
        public float rollInputTimer;

        PlayerControls inputActions;
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        PlayerManager playerManager;
        CameraHandler cameraHandler;
        UIManager uiManager;

        Vector2 movementInput;
        Vector2 cameraInput;

        private void Awake()
        {
            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
            playerManager = GetComponent<PlayerManager>();
            uiManager = FindObjectOfType<UIManager>();
            cameraHandler = FindObjectOfType<CameraHandler>();
        }

        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

                inputActions.PlayerActions.Skill.performed += i => skill_input = true;
                inputActions.PlayerActions.Ultimate.performed += i => ulti_input = true;
                inputActions.PlayerQuickSlots.Slot1.performed += i => slot1_input = true;
                inputActions.PlayerQuickSlots.Slot2.performed += i => slot2_input = true;
                inputActions.PlayerActions.Interact.performed += i => interact_input = true;
                inputActions.PlayerActions.Jump.performed += i => jump_input = true;
                inputActions.PlayerActions.Inventory.performed += i => inventory_input = true;
                inputActions.PlayerActions.LockOn.performed += i => lockOn_input = true;
                inputActions.PlayerMovement.LockOnTargetLeft.performed += i => lockLeftTarget_input = true;
                inputActions.PlayerMovement.LockOnTargetRight.performed += i => lockRightTarget_input = true;
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
            HandleInventoryInput();
            HandleLockOnInput();
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
            sprintFlag = dash_input;

            if (dash_input)
            {
                rollInputTimer += delta;
            }
            else
            {
                if (rollInputTimer > 0 && rollInputTimer < 0.3f)
                {
                    sprintFlag = false;
                    rollFlag = true;
                }

                rollInputTimer = 0;
            }
        }

        private void HandleAttackInput(float delta)
        {
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
            if (slot1_input)
            {
                playerInventory.ChangeRightWeapon();
            }
            else if (slot2_input)
            {
                playerInventory.ChangeLeftWeapon();
            }
        }

        private void HandleInventoryInput()
        {
            if (inventory_input)
            {
                inventoryFlag = !inventoryFlag;

                if (inventoryFlag)
                {
                    uiManager.OpenSelectWindow();
                    Cursor.visible = true;
                    uiManager.UpdateUI();
                    uiManager.hudWindow.SetActive(false);
                }

                if (!inventoryFlag)
                {
                    uiManager.CloseSelectWindow();
                    uiManager.CloseAllInventoryWindows();
                    uiManager.hudWindow.SetActive(true);
                }
            }
        }

        private void HandleLockOnInput()
        {
            if (lockOn_input && lockOnFlag == false)
            {
                cameraHandler.ClearLockOnTargets();
                lockOn_input = false;
                cameraHandler.HandleLockOn();

                if (cameraHandler.nearestLockOnTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.nearestLockOnTarget;
                    lockOnFlag = true;
                }
            }
            else if (lockOn_input && lockOnFlag)
            {
                lockOn_input = false;
                lockOnFlag = false;
                cameraHandler.ClearLockOnTargets();
            }

            if (lockOnFlag && lockLeftTarget_input)
            {
                lockLeftTarget_input = false;
                cameraHandler.HandleLockOn();

                if (cameraHandler.leftLockOnTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.leftLockOnTarget;
                }
            }

            if (lockOnFlag && lockRightTarget_input)
            {
                lockRightTarget_input = false;
                cameraHandler.HandleLockOn();

                if (cameraHandler.rightLockOnTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.rightLockOnTarget;
                }
            }
        }
    }
}
