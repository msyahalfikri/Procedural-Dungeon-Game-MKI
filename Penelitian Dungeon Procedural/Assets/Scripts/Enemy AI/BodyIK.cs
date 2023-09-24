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

    // Start is called before the first frame update
    void Start()
    {
        Animator animator = GetComponent<Animator>();
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
