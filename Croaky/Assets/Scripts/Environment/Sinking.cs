using UnityEngine;
using System.Collections;

public class Sinking : MonoBehaviour
{
    public float sinkDelay = 2f;
    public float sinkSpeed = 1f;

    private bool isTriggered = false;
    private Vector3 startPosition;
    private Coroutine sinkCoroutine;

    void Start()
    {
        // Guardar posición inicial
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

    // 🔁 NUEVA FUNCIÓN PARA REINICIAR
    public void ResetPlatform()
    {
        // detener hundimiento
        if (sinkCoroutine != null)
        {
            StopCoroutine(sinkCoroutine);
        }

        CancelInvoke();

        // resetear estado
        isTriggered = false;

        // volver a posición inicial
        transform.position = startPosition;
    }
}