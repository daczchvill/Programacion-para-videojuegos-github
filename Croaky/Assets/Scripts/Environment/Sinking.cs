using UnityEngine;

public class Sinking : MonoBehaviour
{
    public float sinkDelay = 2f; // tiempo antes de hundirse
    public float sinkSpeed = 1f; // velocidad de hundimiento

    private bool isTriggered = false;

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
        // mejorar con animaciones
        StartCoroutine(Sink());
    }

    System.Collections.IEnumerator Sink()
    {
        while (true)
        {
            transform.position += new Vector3(0, -sinkSpeed * Time.deltaTime, 0);
            yield return null;
        }
    }
}
