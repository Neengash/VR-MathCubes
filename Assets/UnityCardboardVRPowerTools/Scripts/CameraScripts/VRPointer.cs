using UnityEngine;
using UnityEngine.UI;

public class VRPointer : MonoBehaviour
{
    public bool ActiveVRInteractions = true;
    public static VRPointer instance;

    [SerializeField] private float pointerDistance = 5;
    [SerializeField] Color baseColor = new Color(1f, 1f, 1f, 1f);
    [SerializeField] Image pointerImg; 

    private VRInteraction gazedObject = null;
    private RaycastHit hit;

    private void Awake() {
        instance = this;
    }

    void Update() {
        if (ActiveVRInteractions) {
            RaycastInteractions();
        }
    }

    void RaycastInteractions() {
        if (Physics.Raycast(transform.position, transform.forward, out hit, pointerDistance)) {
            ObjectInSight();
            if (Input.GetButtonDown("Fire1")) {
                ObjectInteraction();
            }
        } else {
            NoObjectInSight();
        }
    }

    void ObjectInSight() {
        // Can't use null conditional on gazedObject in the if because it could raise MissingReferenceException
        if (hit.transform.gameObject != (gazedObject == null ? null : gazedObject.gameObject)) {
            if (gazedObject != null) { gazedObject.OnPointerExit(); }
            gazedObject = hit.transform.gameObject.GetComponent<VRInteraction>();
            gazedObject?.OnPointerEnter(); 
        }

        SetColor(gazedObject != null ? gazedObject.holdColor : baseColor);
    }

    void NoObjectInSight() {
        gazedObject?.OnPointerExit();
        gazedObject = null;

        SetColor();
    }

    void ObjectInteraction() {
        if (gazedObject != null) {
            gazedObject?.OnPointerClick();

            SetColor(gazedObject.clickColor);
        }
    }
    
    public void SetColor() { pointerImg.color = baseColor; }
    public void SetColor(Color color) { pointerImg.color = color; }
}
