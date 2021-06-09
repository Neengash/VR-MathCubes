using UnityEngine;

public class SpotDestination : MonoBehaviour
{
    public void startCameraMovement() {
        CameraMovementSpot.instance.MoveToPoint(transform.position);
    }
}
