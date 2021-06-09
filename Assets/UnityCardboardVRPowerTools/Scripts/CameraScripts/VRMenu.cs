using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

public class VRMenu : MonoBehaviour
{
    [SerializeField] bool FollowCamera = true;
    [SerializeField] Transform VRCameraTransform, ownTransform;
    [SerializeField] Camera m_camera;

    [SerializeField] GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    List<GameObject> hits, previousHits;
    VRInteraction gazedObject;
    VRInteraction hit;

    private void Start() {
        hits = new List<GameObject>();
        previousHits = new List<GameObject>();
        m_EventSystem = FindObjectOfType<EventSystem>();
    }

    private void Update() {
        //Might fix problems with mouse interaction
        //m_PointerEventData.Reset();
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = new Vector2(m_camera.pixelWidth / 2, m_camera.pixelHeight / 2);
        List<RaycastResult> results = new List<RaycastResult>();
        m_Raycaster.Raycast(m_PointerEventData, results);

        hit = null;
        foreach (RaycastResult result in results) {
            if (result.gameObject.GetComponent<VRInteraction>()) {
                hit = result.gameObject.GetComponent<VRInteraction>();
            }
        }

        if (hit != null) {
            ObjectInSight(hit);
            if (Input.GetButtonDown("Fire1")) {
                ObjectInteraction(hit);
            }
        } else if (gazedObject != null) {
            NoObjectInSight();
        }
    }

    private void ObjectInSight(VRInteraction hit) {
        if (hit != gazedObject) {
            if (gazedObject != null) { gazedObject?.OnPointerExit(); }
            gazedObject = hit;
            gazedObject?.OnPointerEnter();
        }
        VRPointer.instance.SetColor(gazedObject.holdColor);
    }

    private void NoObjectInSight() {
        if (gazedObject != null) { gazedObject?.OnPointerExit(); }
        gazedObject = null;

        VRPointer.instance.SetColor();
    }

    private void ObjectInteraction(VRInteraction hit) {
        if (gazedObject != null) {
            gazedObject.OnPointerClick();
            VRPointer.instance.SetColor(gazedObject.clickColor);
        }
    }

    void LateUpdate() {
        if (FollowCamera) {
            ownTransform.rotation = Quaternion.Euler(
                ownTransform.rotation.eulerAngles.x,
                VRCameraTransform.rotation.eulerAngles.y,
                ownTransform.rotation.eulerAngles.z
            );
        }
    }
}
