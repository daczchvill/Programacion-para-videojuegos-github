using UnityEngine;

public class SaludEnemigo : MonoBehaviour
{
    public int vida = 3; // El cubo aguanta 3 golpes

    // esto se activa cuando algo con un Collider toca al cubo
    private void OnCollisionEnter(Collision collision)
    {
        // Revisamos si lo que nos tocó tiene la etiqueta "AtaqueRana"
        if (collision.gameObject.CompareTag("AtaqueRana"))
        {
            vida--; // se resta 1 de vida
            Debug.Log("¡Golpe recibido! Vida restante: " + vida);

            if (vida <= 0)
            {
                Murió();
            }
        }
    }

    // Esta función detecta objetos marcados como "Is Trigger"
    private void OnTriggerEnter(Collider other)
    {
        // Revisamos si lo que entró en nuestro espacio tiene la etiqueta "AtaqueRana"
        if (other.CompareTag("AtaqueRana"))
        {
            vida--;
            Debug.Log("¡Golpe de Lengua! Vida restante: " + vida);

            if (vida <= 0)
            {
                Murió();
            }
        }
    }

    void Murió()
    {
        Debug.Log("Enemigo derrotado");
        Destroy(gameObject); // El cubo desaparece
    }
}