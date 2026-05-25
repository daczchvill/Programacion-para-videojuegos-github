using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform currentPoint;

    public float moveSpeed = 3f;
    public float rotateSpeed = 3f;

    void LateUpdate()
    {
        if (currentPoint == null) return;

        // mover cámara
        transform.position = Vector3.Lerp(
            transform.position,
            currentPoint.position,
            moveSpeed * Time.deltaTime
        );

        // rotar cámara
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            currentPoint.rotation,
            rotateSpeed * Time.deltaTime
        );
    }

    public void SetCameraPoint(Transform newPoint)
    {
        currentPoint = newPoint;
    }
}