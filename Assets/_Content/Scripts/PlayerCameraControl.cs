using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Metamothosis.GameEvents;

public class PlayerCameraControl : MonoBehaviour
{
    [SerializeField] float horizontalSensitivity = 1f;
    [SerializeField] float verticalSensitivity = 1f;
    [SerializeField] bool invertHorizontal = false;
    [SerializeField] bool invertVertical = false;

    float sensitivity;
    float mouseDeltaX;
    float mouseDeltaY;
    float rotX;
    float rotY;

    GameManager manager;
    Transform trans;

    void OnEnable()
    {
        GameEvent<MouseSensitivityChangedEvent>.Register(OnMouseSensitivityChanged);
    }

    void OnDisable()
    {
        GameEvent<MouseSensitivityChangedEvent>.Deregister(OnMouseSensitivityChanged);
    }

    void Awake()
    {
        manager = FindObjectOfType<GameManager>();
        trans = transform;
        sensitivity = 0.5f;
    }

    // Start is called before the first frame update
    void Start()
    {
        rotX = 42;
        rotY = 40;
    }

    // Update is called once per frame
    void Update()
    {
        rotX += mouseDeltaY * verticalSensitivity * sensitivity * (invertVertical ? -1 : 1);
        rotY += mouseDeltaX * horizontalSensitivity * sensitivity * (invertHorizontal ? -1 : 1);
        rotX = Mathf.Clamp(rotX, -89f, 89f);

        trans.rotation = Quaternion.Euler(rotX, rotY, 0);

        mouseDeltaX = 0;
        mouseDeltaY = 0;
    }

    public void OnLookX(InputValue value)
    {
        if (manager && manager.cameraDisabled)
            return;

        mouseDeltaX += (float)value.Get();
    }

    public void OnLookY(InputValue value)
    {
        if (manager && manager.cameraDisabled)
            return;

        mouseDeltaY += (float)value.Get();
    }

    void OnMouseSensitivityChanged(float value)
    {
        sensitivity = value;
    }
}
