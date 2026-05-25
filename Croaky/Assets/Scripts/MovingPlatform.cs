using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    public float moveSpeed = 2f;

    private Vector3 target;

    void Start()
    {
        target = pointB.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            if (target == pointB.position)
            {
                target = pointA.position;
            }
            else
            {
                target = pointB.position;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }

    private void OnDrawGizmos()
{
    if (pointA != null && pointB != null)
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(pointA.position, pointB.position);
    }
}
}