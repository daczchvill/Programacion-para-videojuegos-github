using UnityEngine;
using TMPro;
using System.Collections;

public class FrogGameManager : MonoBehaviour
{
    public IEnumerator PopScore()
{
    Vector3 originalScale = scoreText.transform.localScale;
    Vector3 popScale = originalScale * 1.2f;

    // Escala hacia arriba
    scoreText.transform.localScale = popScale;

    // Pequeña pausa
    yield return new WaitForSeconds(0.1f);

    // Vuelve a su tamaño normal
    scoreText.transform.localScale = originalScale;
}
    public static FrogGameManager Instance;

    public int flies = 0;

    // Referencia al texto UI
    public TextMeshProUGUI scoreText;

    // Sonido de recolección
    public AudioClip collectSound;
    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Actualizar el texto al iniciar
        if (scoreText != null)
            scoreText.text = "spiders: " + flies;
    }

    public void AddFlies(int amount)
    {
        flies += amount;

        if (scoreText != null)
            scoreText.text = "spiders: " + flies;

        PlayCollectSound();

        // Activar efecto POP
        StartCoroutine(PopScore());

        Debug.Log("Arañas recolectadas: " + flies);
    }

    public void PlayCollectSound()
    {
        if (collectSound != null)
            audioSource.PlayOneShot(collectSound);
    }
}


