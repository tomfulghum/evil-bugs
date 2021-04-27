using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] Transform lookBone;
    [SerializeField] Transform lookAtTarget;
    [SerializeField] float angleLimit = 75f;
    [SerializeField] float maxDegreesDelta = 180f;

    Vector3 initialForward;
    Quaternion initialLookRotation;

    // Start is called before the first frame update
    void Start()
    {
        initialForward = lookBone.forward;
        initialLookRotation = lookBone.rotation;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var dir = lookAtTarget.position - lookBone.position;
        var lookRotation = Vector3.Angle(initialForward, dir) < angleLimit ? Quaternion.LookRotation(dir, Vector3.up) : initialLookRotation;
        lookBone.rotation = Quaternion.RotateTowards(lookBone.rotation, lookRotation, Time.deltaTime * maxDegreesDelta);
    }
}
