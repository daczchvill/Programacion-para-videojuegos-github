using UnityEngine;
using System.Collections;

public class Sinking : MonoBehaviour
{
    public float sinkDelay = 1f;
    public float sinkSpeed = 2f;

    private bool isTriggered = false;
    private Vector3 startPosition;
    private Coroutine sinkCoroutine;

    void Start()
    {
        // save initial position
        startPosition = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;
            Invoke("StartSinking", sinkDelay);
        }
    }

    void StartSinking()
    {
        sinkCoroutine = StartCoroutine(Sink());
    }

    IEnumerator Sink()
    {
        while (true)
        {
            transform.position += Vector3.down * sinkSpeed * Time.deltaTime;
            yield return null;
        }
    }

    // new function to reset the platform
    public void ResetPlatform()
    {
        // stopp sinking if it's currently sinking
        if (sinkCoroutine != null)
        {
            StopCoroutine(sinkCoroutine);
        }

        CancelInvoke();

        // reestart trigger
        isTriggered = false;

        // go to initial position
        transform.position = startPosition;
    }
}