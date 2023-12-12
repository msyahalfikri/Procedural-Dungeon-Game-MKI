using Cinemachine;
using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Windows;

public class CombatController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera blockVirtualCamera;
    [SerializeField] private LayerMask mouseColliderMask = new LayerMask();
    private StarterAssetsInputs input;
    [HideInInspector] public Animator animator;
    public GameObject sword;
    public GameObject blockSword;
    public ThirdPersonController thirdPersonController;
    private GameObject[] enemies;
    private Transform targetEnemy;
    // combat combo animation
    private float nextFireTime = 0f;
    private static int numberOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 1f;

    // combat system
    public float attackRange = 5f;
    public bool isBlocking = false;
    public bool isExhausted = false;
    public bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        input = GetComponent<StarterAssetsInputs>();
        sword = GameObject.FindGameObjectWithTag("Weapon");
        blockSword = GameObject.FindGameObjectWithTag("Shield");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        Block();
        CombatUpdate();
        FaceNearestEnemy();
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


        if (input.block && !input.attack && thirdPersonController.Grounded)
        {
            if (isExhausted)
            {
                isBlocking = false;
                animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0f, Time.deltaTime * 10f));
            }
            else
            {
                sword.SetActive(false);
                animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 1f, Time.deltaTime * 10f));

                Vector3 worldAimTarget = mouseWorldPosition;
                worldAimTarget.y = transform.position.y;
                Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

                transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
                isBlocking = true;
            }
        }
        else
        {
            isBlocking = false;
            animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0f, Time.deltaTime * 10f));
        }
        blockVirtualCamera.gameObject.SetActive(isBlocking);
        blockSword.SetActive(isBlocking);

    }

    private void CombatUpdate()
    {
        if (thirdPersonController.Grounded)
        {
            if (animator.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(1).IsName("Attack1"))
            {
                animator.SetBool("Attack1", false);
                isAttacking = false;
            }

            if (animator.GetCurrentAnimatorStateInfo(1).normalizedTime > 0.7f && animator.GetCurrentAnimatorStateInfo(1).IsName("Attack2"))
            {
                animator.SetBool("Attack2", false);
                isAttacking = false;
            }

            if (animator.GetCurrentAnimatorStateInfo(1).normalizedTime > 1.2f && animator.GetCurrentAnimatorStateInfo(1).IsName("Attack3"))
            {
                animator.SetBool("Attack3", false);
                numberOfClicks = 0;
                isAttacking = false;
            }

            if (Time.time - lastClickedTime > maxComboDelay)
            {
                numberOfClicks = 0;
                animator.SetBool("Attack1", false);
                animator.SetBool("Attack2", false);
                animator.SetBool("Attack3", false);
                animator.SetBool("IsAttacking", false);
                sword.SetActive(false);
            }

            if (Time.time > nextFireTime)
            {
                if (input.attack && !input.block)
                {
                    isAttacking = true;
                    animator.SetBool("IsAttacking", true);
                    OnClick();

                }
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

    private void FaceNearestEnemy()
    {
        // Find all game objects tagged as enemies
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Find the nearest enemy
        float closestDistance = Mathf.Infinity;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                targetEnemy = enemy.transform;
            }
        }

        // Face the nearest enemy
        if (targetEnemy != null && closestDistance <= attackRange)
        {
            Debug.Log("face the player");
            Vector3 direction = targetEnemy.position - transform.position;
            direction.y = 0f; // If you don't want the player to tilt up/down
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
    public void StartDealDamage()
    {
        sword.GetComponentInChildren<DamageDealer>().StartDealDamage();
    }

    public void EndDealDamage()
    {
        sword.GetComponentInChildren<DamageDealer>().EndDealDamage();
    }
}
