using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationField : MonoBehaviour
{
    [SerializeField] float gravitationForce = 1f;

    SphereCollider coll;
    Rigidbody player;

    void Awake()
    {
        coll = GetComponent<SphereCollider>();
    }

    void FixedUpdate()
    {
        if (player) {
            var gravitationVector = (transform.position - player.transform.position);
            var playerDistance = gravitationVector.magnitude;
            var gravitationStrength = ((Mathf.Sin(Mathf.InverseLerp(0f, coll.radius, playerDistance) * Mathf.PI + (Mathf.PI / 2)) + 1) / 2) * gravitationForce;
            Debug.Log(gravitationStrength);
            player.AddForce(gravitationVector * gravitationStrength, ForceMode.Force);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            player = other.GetComponentInParent<Rigidbody>();
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            player = null;
    }
}
