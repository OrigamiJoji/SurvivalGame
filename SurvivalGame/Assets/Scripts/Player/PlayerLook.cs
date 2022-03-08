using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerLook : MonoBehaviour {

    [Header("~ Sensitivity <3")]
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;

    [Header("~ FOV <3")]
    [SerializeField] private float defaultFOV = 60f;

    [Header("~ References <3")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform orientation;

    private readonly float multiplier = 0.01f;
    private float xRotation;
    private float yRotation;

    private float mouseX;
    private float mouseY;
    private float lockValue;


    private void Start() {
        // Cursor.lockState = CursorLockMode.Locked;
        playerCamera.fieldOfView = defaultFOV;
        LockMouseInput(false);
    }

    private void Update() {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * sensX * multiplier * lockValue;
        xRotation -= mouseY * sensY * multiplier * lockValue;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void LockMouseInput(bool locked) {
        if (locked) {
            lockValue = 0;
        }
        else {
            lockValue = 1;
        }
    }

}

