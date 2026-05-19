using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.right;
    public float moveDistance = 6f;
    public float moveSpeed = .3f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position = startPosition +
            moveDirection * Mathf.Sin(Time.time * moveSpeed) * moveDistance;
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
}