using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float jumpHeight = 1.5f;
    public float jumpDuration = 0.4f;
    public float tileSize = 3f;
    public int vida = 10;

    private bool isMoving = false;
    private Vector3 startPosition;
    private Vector3 gridPosition;

    private Rigidbody rb;
    private Animator animator;

    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip waterSound;
    private AudioSource audioSource;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();


        // allign to grid at start
        gridPosition = SnapToGrid(transform.position);
        transform.position = gridPosition;

        startPosition = gridPosition;
    }
    public void SetCheckpoint(Vector3 newCheckpoint)
{
    startPosition = SnapToGrid(newCheckpoint);
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

        Vector3 start = SnapToGrid(transform.position);
        Vector3 end = SnapToGrid(start + direction * tileSize);

        float time = 0;

        // rotation to where is jumping
        transform.forward = direction;

        animator.SetTrigger("Jump");
        audioSource.PlayOneShot(jumpSound);

        while (time < jumpDuration)
        {
            float t = time / jumpDuration;

            // horizontal movement
            Vector3 pos = Vector3.Lerp(start, end, t);

            // vertical jump using sine wave
            float height = Mathf.Sin(t * Mathf.PI) * jumpHeight;

            transform.position = pos + Vector3.up * height;

            time += Time.deltaTime;
            yield return null;
        }

        // snap final open
        gridPosition = SnapToGrid(end);
        transform.position = gridPosition;

        //transform.forward = Vector3.forward;

        isMoving = false;
    }

    // allign grid position to tile size
    Vector3 SnapToGrid(Vector3 pos)
    {
        float x = Mathf.Round(pos.x / tileSize) * tileSize;
        float z = Mathf.Round(pos.z / tileSize) * tileSize;

        return new Vector3(x, pos.y, z);
    }

    // water is bad
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            audioSource.PlayOneShot(waterSound);

            ResetPlayer();
        }
    }

    public void ResetPlayer()
    {

        vida = Mathf.Max(vida - 1, 0);

        if (vida <= 0)
        {
            audioSource.Stop();

            audioSource.PlayOneShot(deathSound);

            StartCoroutine(RestartAfterGameOver());

            return;
        }

        StopAllCoroutines();
        isMoving = false;

        gridPosition = startPosition;
        transform.position = startPosition;

        // reestart sinking platforms
        Sinking[] platforms = FindObjectsOfType<Sinking>();

        foreach (Sinking platform in platforms)
        {
            platform.ResetPlatform();
        }
    }

    IEnumerator RestartAfterGameOver()
    {
        yield return new WaitForSeconds(deathSound.length);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}