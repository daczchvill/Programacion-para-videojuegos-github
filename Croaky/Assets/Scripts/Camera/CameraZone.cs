using UnityEngine;

public class CameraZone : MonoBehaviour
{
    public Transform cameraPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CameraFollow cam = Camera.main.GetComponent<CameraFollow>();

            if (cam != null)
            {
                cam.SetCameraPoint(cameraPoint);
            }
        }
    }
}