using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class CameraHolder : MonoBehaviour {
    [SerializeField] private GameObject playerCameraPos;

    void Update() {
        transform.position = playerCameraPos.transform.position;
    }
}
