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

    GameManager manager;
    Transform trans;

    void Awake()
    {
        manager = FindObjectOfType<GameManager>();
        trans = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        rotX = 0;
        rotY = 0;
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
        if (manager && manager.cameraDisabled)
            return;

        mouseDeltaX = (float)value.Get();
    }

    public void OnLookY(InputValue value)
    {
        if (manager && manager.cameraDisabled)
            return;

        mouseDeltaY = (float)value.Get();
    }
}
