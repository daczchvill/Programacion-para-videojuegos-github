using UnityEngine;
using TMPro;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
    private bool activated = false;

    public GameObject checkpointText;
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !activated)
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                player.SetCheckpoint(player.transform.position);

                activated = true;

                // mostrar texto
                StartCoroutine(ShowCheckpointText());

                // reproducir sonido
                if (audioSource != null)
                {
                    audioSource.Play();
                }
            }
        }
    }

    IEnumerator ShowCheckpointText()
    {
        checkpointText.SetActive(true);

        yield return new WaitForSeconds(2f);

        checkpointText.SetActive(false);
    }
}