using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CrocodileTrigger : MonoBehaviour
{
    public Animator crocodileAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            if (rb != null && !rb.isKinematic)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.isKinematic = true;
                }

            other.GetComponent<PlayerController>().enabled = false;

            crocodileAnimator.SetTrigger("Attack");

            StartCoroutine(EatPlayer(other.gameObject));
        }
    }

    IEnumerator EatPlayer(GameObject player)
    {
        // Esperar mordida
        yield return new WaitForSeconds(0.6f);

        // Ocultar rana
        player.SetActive(false);

        // Esperar final animación
        yield return new WaitForSeconds(0.65f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}