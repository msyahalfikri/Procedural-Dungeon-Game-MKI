using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class HumanoidBone
{
    public HumanBodyBones bone;
    public float weight = 1.0f;
}
public class BodyIK : MonoBehaviour
{
    public Transform targetTransform;
    public Transform aimTransform;

    public int iterations = 10;
    [Range(0, 1)]
    public float weight = 1.0f;

    public float angleLimit = 90.0f;
    public float distanceLimit = 1.5f;

    public HumanoidBone[] humanoidBones;
    Transform[] boneTransforms;

    public float rotationSpeed = 2.0f;
    bool turnLeft, turnRight, hasTurned;
    float crossProduct;
    private AIAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        agent = GetComponent<AIAgent>();
        boneTransforms = new Transform[humanoidBones.Length];
        for (int i = 0; i < boneTransforms.Length; i++)
        {
            boneTransforms[i] = animator.GetBoneTransform(humanoidBones[i].bone);
        }
    }

    Vector3 GetTargetPosition()
    {
        Vector3 targetDirection = targetTransform.position - aimTransform.position;
        Vector3 aimDirection = aimTransform.forward;
        float blendOut = 0.0f;

        float targetAngle = Vector3.Angle(targetDirection, aimDirection);
        if (targetAngle > angleLimit)
        {
            blendOut += (targetAngle - angleLimit) / 50.0f;
        }

        float targetDistance = targetDirection.magnitude;
        if (targetDistance < distanceLimit)
        {
            blendOut += distanceLimit - targetDistance;
        }


        Vector3 direction = Vector3.Slerp(targetDirection, aimDirection, blendOut);
        return aimTransform.position + direction;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //========Upper Body Rotation===========
        if (aimTransform == null)
        {
            return;
        }

        if (targetTransform == null)
        {
            return;
        }

        Vector3 targetPosition = GetTargetPosition();
        for (int b = 0; b < boneTransforms.Length; b++)
        {
            Transform bone = boneTransforms[b];
            float boneWeight = humanoidBones[b].weight * weight;
            AimAtTarget(bone, targetPosition, boneWeight);
        }

        //========Lower Body Rotation========
        Vector3 directionToPlayer = targetTransform.transform.position - transform.position;

        // Calculate the angle between the enemy's forward direction and the direction to the player
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
        float dotProduct = Vector3.Dot(transform.forward, directionToPlayer.normalized);

        // Check if the angle is greater than X degrees
        if (angleToPlayer > 50.0f && dotProduct < 0.5f && hasTurned == false)
        {
            crossProduct = Vector3.Cross(transform.forward, directionToPlayer).y;
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

            // Smoothly interpolate the enemy's rotation towards the target rotation
            if (crossProduct > 0)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * 1 * Time.deltaTime);
                turnLeft = false;
                turnRight = true;
            }
            else if (crossProduct < 0)
            {
                agent.transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * -1 * Time.deltaTime);
                turnLeft = true;
                turnRight = false;
            }
            hasTurned = true;
        }
        else
        {
            crossProduct = 0f;
            if (crossProduct == 0)
            {
                turnLeft = false;
                turnRight = false;
                hasTurned = false;
            }
        }
        agent.turnedLeft = turnLeft;
        agent.turnedRight = turnRight;
        agent.hasTurned = hasTurned;
        // Debug.Log("Left: " + agent.turnedLeft + " || Right: " + agent.turnedRight + " || CrossProduct: " + agent.hasTurned);


    }

    private void AimAtTarget(Transform bone, Vector3 targetPosition, float weight)
    {
        Vector3 aimDirection = aimTransform.forward;
        Vector3 targetDirection = targetPosition - aimTransform.position;
        Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirection);
        Quaternion blendedRotation = Quaternion.Slerp(Quaternion.identity, aimTowards, weight);
        bone.rotation = blendedRotation * bone.rotation;
    }

    public void SetTargetTransform(Transform target)
    {
        targetTransform = target;
    }

    public void SetAimTranform(Transform aim)
    {
        aimTransform = aim;
    }

}
