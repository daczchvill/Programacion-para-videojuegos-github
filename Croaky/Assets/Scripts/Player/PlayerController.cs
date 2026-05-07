using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float jumpHeight = 1.5f;
    public float jumpDuration = 0.4f;
    public float tileSize = 3f;

    private bool isMoving = false;
    private Vector3 startPosition;
    private Vector3 gridPosition;

    private Rigidbody rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();


        // 🔲 Alinear al grid desde el inicio
        gridPosition = SnapToGrid(transform.position);
        transform.position = gridPosition;

        startPosition = gridPosition;
    }

    void Update()
    {
        if (isMoving) return;

        Vector3 dir = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.UpArrow))
            dir = Vector3.forward;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            dir = Vector3.back;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            dir = Vector3.left;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            dir = Vector3.right;

        if (dir != Vector3.zero)
        {
            StartCoroutine(Jump(dir));
        }
    }

    IEnumerator Jump(Vector3 direction)
    {
        isMoving = true;

        Vector3 start = gridPosition;
        Vector3 end = start + direction * tileSize;

        float time = 0;

        // 🔁 Rotación hacia donde salta
        transform.forward = direction;

        animator.SetTrigger("Jump");

        while (time < jumpDuration)
        {
            float t = time / jumpDuration;

            // Movimiento horizontal
            Vector3 pos = Vector3.Lerp(start, end, t);

            // Arco del salto
            float height = Mathf.Sin(t * Mathf.PI) * jumpHeight;

            transform.position = pos + Vector3.up * height;

            time += Time.deltaTime;
            yield return null;
        }

        // 🔲 SNAP FINAL LIMPIO
        gridPosition = SnapToGrid(end);
        transform.position = gridPosition;

        isMoving = false;
    }

    // 🔲 Función clave: alinear al grid
    Vector3 SnapToGrid(Vector3 pos)
    {
        float x = Mathf.Round(pos.x / tileSize) * tileSize;
        float z = Mathf.Round(pos.z / tileSize) * tileSize;

        return new Vector3(x, pos.y, z);
    }

    // 💧 Si toca agua → reinicio
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            ResetPlayer();
        }
    }

    void ResetPlayer()
    {
        StopAllCoroutines();
        isMoving = false;

        gridPosition = startPosition;
        transform.position = startPosition;
    }
}