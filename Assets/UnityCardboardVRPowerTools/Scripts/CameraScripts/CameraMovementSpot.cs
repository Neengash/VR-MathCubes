using UnityEngine;

public class CameraMovementSpot : MonoBehaviour
{
    public static CameraMovementSpot instance;
    [SerializeField, Range(1f, 10f)] float speed = 1f;

    [SerializeField]
    Transform baseTransform; 
    private Vector3 destination;
    private bool moving;

    void Start() {
        instance = this;
        moving = false;
    }

    void Update() {
        if (moving) {
            baseTransform.position = Vector3.MoveTowards(baseTransform.position, destination, speed*Time.deltaTime);
        }

        if (Vector3.Distance(baseTransform.position, destination) < 0.01f) {
            baseTransform.position = destination;
            moving = false;
        }
    }

    public void MoveToPoint(Vector3 newDestination) {
        if (!moving) {
            destination = new Vector3(
                newDestination.x,
                baseTransform.position.y,
                newDestination.z
            );
            moving = true;
        }
    }
}
