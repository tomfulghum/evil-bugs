using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraControl : MonoBehaviour
{
    [SerializeField] float horizontalSensitivity = 1f;
    [SerializeField] float verticalSensitivity = 1f;
    [SerializeField] bool invertHorizontal = false;
    [SerializeField] bool invertVertical = false;

    float mouseDeltaX;
    float mouseDeltaY;
    float rotX;
    float rotY;
    
    Transform trans;

    void Awake()
    {
        trans = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        rotX = 0;
        rotY = 0;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        rotX += mouseDeltaY * verticalSensitivity * (invertVertical ? -1 : 1);
        rotY += mouseDeltaX * horizontalSensitivity * (invertHorizontal ? -1 : 1);
        rotX = Mathf.Clamp(rotX, -89f, 89f);

        trans.rotation = Quaternion.Euler(rotX, rotY, 0);
    }

    public void OnLookX(InputValue value)
    {
        mouseDeltaX = (float)value.Get();
    }

    public void OnLookY(InputValue value)
    {
        mouseDeltaY = (float)value.Get();
    }
}
