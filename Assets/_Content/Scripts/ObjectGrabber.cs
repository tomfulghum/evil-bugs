using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabber : MonoBehaviour
{
    ConfigurableJoint grabbedObject;
    ConfigurableJoint grabbableObject;
    Vector3 localGrabPosition;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    public void OnGrab()
    {
        if (grabbedObject)
        {
            grabbedObject.connectedBody = null;
            grabbedObject.anchor = Vector3.zero;
            grabbedObject.xMotion = ConfigurableJointMotion.Free;
            grabbedObject.yMotion = ConfigurableJointMotion.Free;
            grabbedObject.zMotion = ConfigurableJointMotion.Free;
            grabbedObject = null;
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
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            grabbableObject = other.GetComponentInParent<ConfigurableJoint>();
            localGrabPosition = grabbableObject.transform.InverseTransformPoint(transform.position);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == grabbableObject)
            grabbableObject = null;
    }
}
