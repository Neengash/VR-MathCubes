using Google.XR.Cardboard;
using UnityEngine.XR.Management;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SpatialTracking;

public class VRSimulator : MonoBehaviour
{
    [SerializeField, Range(.05f, 2)]
    float dragRate = .2f;

    Quaternion initialRotation;
    Quaternion attitude;
    Vector2 dragDegrees;
    Vector3 lastMousePos;

    void Awake() {
        initialRotation = transform.rotation;
    }

    void Update() {
        SimulateVR();

        attitude = initialRotation * Quaternion.Euler(dragDegrees.x, 0, 0);
        transform.rotation = Quaternion.Euler(0, -dragDegrees.y, 0) * attitude;
    }

    void SimulateVR() {
        var mousePos = Input.mousePosition;
        if (Input.GetKey(KeyCode.Z)) {
            var delta = mousePos - lastMousePos;
            dragDegrees.x -= delta.y * dragRate;
            dragDegrees.y -= delta.x * dragRate;
        }
        lastMousePos = mousePos;
    }
}