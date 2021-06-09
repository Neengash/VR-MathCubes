using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementTouch : MonoBehaviour
{
    [SerializeField]
    Transform cameraBase;

    [SerializeField]
    Transform cameraTransform;

    [SerializeField, Range(0.5f, 10f)]
    float cameraSpeed;

    void Update() {
        if (Input.GetButton("Fire1")) {
            DoMovement();
        }
    }

    void DoMovement() {
        Vector3 direction = new Vector3(
            cameraTransform.forward.x,
            0,
            cameraTransform.forward.z
        );
        cameraBase.Translate(direction.normalized * cameraSpeed * Time.deltaTime);
    }
}
