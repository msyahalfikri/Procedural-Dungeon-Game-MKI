using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DungeonLiberation
{
    public class PlayerManager : MonoBehaviour
    {
        InputHandler inputHandler;
        Animator anim;
        CameraHandler cameraHandler;
        PlayerLocomotion playerLocomotion;

        public bool isInteracting;

        [Header("Player Flags")]
        public bool isSprinting;
        public bool isAirborne;
        public bool isGrounded;
        public bool canDoCombo;
        void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            cameraHandler = CameraHandler.singleton;

            if (cameraHandler != null )
            {
                cameraHandler = FindAnyObjectByType<CameraHandler>();
            }
        }

        void Update()
        {
            float delta = Time.deltaTime;
            isInteracting = anim.GetBool("isInteracting");
            canDoCombo = anim.GetBool("canDoCombo");

            inputHandler.TickInput(delta);
            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleRollingAndSprinting(delta);
            playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);

            CheckForInteractables();
        }
        
        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;

            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }
        }

        private void LateUpdate()
        {
            inputHandler.rollFlag = false;
            inputHandler.sprintFlag = false;
            inputHandler.skill_input = false;
            inputHandler.ulti_input = false;
            inputHandler.slot1_input = false;
            inputHandler.slot2_input = false;
            inputHandler.slot3_input = false;
            inputHandler.slot4_input = false;
            inputHandler.interact_input = false;

            if (isAirborne)
            {
                playerLocomotion.inAirTimer += Time.deltaTime;
            }
        }

        public void CheckForInteractables()
        {
            RaycastHit hit;
            Vector3 rayOrigin = transform.position;
            rayOrigin.y = rayOrigin.y + 2f;

            if (Physics.SphereCast(transform.position, 0.3f, transform.forward, out hit, 1f, cameraHandler.ignoreLayers) || Physics.SphereCast(rayOrigin, 0.3f, Vector3.down, out hit, 2.5f, cameraHandler.ignoreLayers))
            {
                if (hit.collider.tag == "Interactable")
                {
                    Interactables interactableObject = hit.collider.GetComponent<Interactables>();

                    if (interactableObject != null)
                    {
                        string interactableText = interactableObject.interactableText;

                        //setup ui text

                        if (inputHandler.interact_input)
                        {
                            hit.collider.GetComponent<Interactables>().Interact(this);
                        }
                    }
                }
            }
        }
    }
}
