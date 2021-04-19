using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabber : MonoBehaviour
{
    ConfigurableJoint grabbedObject;
    ConfigurableJoint grabbableObject;
    Vector3 localGrabPosition;

    [Header("Animation")]
    [SerializeField] Animator animator;

    GameManager manager;
    Rigidbody rb;
    CapsuleCollider coll;

    public bool grabbed { get; private set; }

    void Awake()
    {
        manager = FindObjectOfType<GameManager>();
        rb = GetComponentInParent<Rigidbody>();
        coll = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        var point1 = transform.TransformPoint(coll.center + new Vector3(0, 0, coll.height / 2));
        var point2 = transform.TransformPoint(coll.center - new Vector3(0, 0, coll.height / 2));
        var collisions = Physics.OverlapCapsule(point1, point2, coll.radius);
        foreach (var collider in collisions)
        {
            if (collider.CompareTag("Grabbable"))
            {
                grabbableObject = collider.GetComponentInParent<ConfigurableJoint>();
                localGrabPosition = grabbableObject.transform.InverseTransformPoint(collider.ClosestPoint(transform.position));
                break;
            }

            grabbableObject = null;
        }

        if (!grabbedObject)
            animator.SetBool("Grabbed", false);
    }

    public void OnGrab()
    {
        if (manager && manager.inputDisabled)
            return;

        if (grabbedObject)
        {
            grabbedObject.connectedBody = null;
            grabbedObject.anchor = Vector3.zero;
            grabbedObject.xMotion = ConfigurableJointMotion.Free;
            grabbedObject.yMotion = ConfigurableJointMotion.Free;
            grabbedObject.zMotion = ConfigurableJointMotion.Free;
            grabbedObject = null;
            animator.SetBool("Grabbed", false);
            grabbed = false;
            return;
        }
        else if (!grabbableObject)
        {
            return;
        }

        grabbedObject = grabbableObject;
        grabbedObject.connectedBody = rb;
        grabbedObject.anchor = localGrabPosition;
        grabbedObject.xMotion = ConfigurableJointMotion.Limited;
        grabbedObject.yMotion = ConfigurableJointMotion.Limited;
        grabbedObject.zMotion = ConfigurableJointMotion.Limited;
        animator.SetBool("Grabbed", true);
        grabbed = true;
    }
}
