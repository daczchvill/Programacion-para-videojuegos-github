using UnityEngine;
using System.Collections;

public class TurtleTrigger : MonoBehaviour
{
    public Animator turtleAnimator;

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

            PlayerController playerController =
                other.GetComponent<PlayerController>();

            playerController.enabled = false;

            turtleAnimator.SetTrigger("attack");

            StartCoroutine(EatPlayer(other.gameObject, playerController));
        }
    }

    IEnumerator EatPlayer(GameObject player, PlayerController playerController)
    {
        // esperar animación
        yield return new WaitForSeconds(0.9f);

        // ocultar rana
        player.SetActive(false);

        yield return new WaitForSeconds(0.4f);

        // volver a mostrar rana
        player.SetActive(true);

        // volver a activar físicas
        Rigidbody rb = player.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.isKinematic = false;
        }

        // activar controller otra vez
        playerController.enabled = true;

        // respawn en checkpoint
        playerController.ResetPlayer();

        // reiniciar animación tortuga
        turtleAnimator.ResetTrigger("attack");
        turtleAnimator.Play("idle tortuga");
    }
}