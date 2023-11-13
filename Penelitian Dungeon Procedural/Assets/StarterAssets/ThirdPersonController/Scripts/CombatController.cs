using Cinemachine;
using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class CombatController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera blockVirtualCamera;
    [SerializeField] private LayerMask mouseColliderMask = new LayerMask();

    private StarterAssetsInputs input;
    private Animator animator;
    public GameObject sword;
    public GameObject blocksword;
    bool inAttackAnimation;
    private CharacterController characterController;
    public float attackForce = 0.005f;

    // combat combo animation
    public float cooldownTime = 2f;
    private float nextFireTime = 0f;
    private static int numberOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 1f;

    // combat system
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
        sword = GameObject.FindGameObjectWithTag("Weapon");
        blocksword = GameObject.FindGameObjectWithTag("Shield");
        animator = GetComponent<Animator>();
        inAttackAnimation = false;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Block();
        CombatUpdate();

        if (inAttackAnimation == true)
        {
            sword.SetActive(true);
        }
        else
        {
            sword.SetActive(false);
        }
    }

    private void Block()
    {
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);

        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, mouseColliderMask))
        {
            mouseWorldPosition = raycastHit.point;
        }

        if (input.block)
        {
            blockVirtualCamera.gameObject.SetActive(true);
            blocksword.SetActive(true);

            animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 1f, Time.deltaTime * 10f));

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }

        else
        {
            blockVirtualCamera.gameObject.SetActive(false);
            blocksword.SetActive(false);

            animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0f, Time.deltaTime * 10f));
        }
    }

    private void CombatUpdate()
    {
        if (animator.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(1).IsName("Attack1"))
        {
            animator.SetBool("Attack1", false);
        }

        if (animator.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(1).IsName("Attack2"))
        {
            animator.SetBool("Attack2", false);
        }

        if (animator.GetCurrentAnimatorStateInfo(1).normalizedTime > 1.2f && animator.GetCurrentAnimatorStateInfo(1).IsName("Attack3"))
        {
            animator.SetBool("Attack3", false);
            numberOfClicks = 0;
        }

        if (Time.time - lastClickedTime > maxComboDelay)
        {
            numberOfClicks = 0;
            animator.SetBool("Attack1", false);
            animator.SetBool("Attack2", false);
            animator.SetBool("Attack3", false);
            animator.SetBool("IsAttacking", false);
            inAttackAnimation = false;
        }

        if (Time.time > nextFireTime)
        {
            if (input.attack)
            {
                animator.SetBool("IsAttacking", true);
                OnClick();
            }
        }
    }

    private void OnClick()
    {
        lastClickedTime = Time.time;
        numberOfClicks++;

        if (numberOfClicks == 1)
        {
            sword.SetActive(true);
            animator.SetBool("Attack1", true);
            Attack();
        }

        numberOfClicks = Mathf.Clamp(numberOfClicks, 0, 3);

        if (numberOfClicks >= 2 && animator.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(1).IsName("Attack1"))
        {
            animator.SetBool("Attack2", true);
            animator.SetBool("Attack1", false);
        }

        if (numberOfClicks >= 3 && animator.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(1).IsName("Attack2"))
        {
            animator.SetBool("Attack3", true);
            animator.SetBool("Attack2", false);
        }
    }

    private void Attack()
    {
        Vector3 forwardMovement = transform.forward * attackForce;
        characterController.Move(forwardMovement);
    }
}
