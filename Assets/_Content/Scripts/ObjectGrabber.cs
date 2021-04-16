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
        rb = GetComponent<Rigidbody>();
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

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Grabbable"))
        {
            grabbableObject = collision.gameObject.GetComponent<ConfigurableJoint>();
            var contact = collision.contacts[0];
            localGrabPosition = grabbableObject.transform.InverseTransformPoint(contact.point);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == grabbableObject)
            grabbableObject = null;
    }
}
