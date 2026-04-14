using System.Collections;
using UnityEngine;

public class VidaRana : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] private int vidaMaxima = 100;
    [SerializeField] private int vidaActual;

    [Header("Daño")]
    [SerializeField] private float tiempoInvulnerable = 0.5f;
    private bool puedeRecibirDaño = true;

    private void Awake()
    {
        vidaActual = vidaMaxima;
    }

    public void TomarDaño(int dano)
    {
        // Evita recibir daño múltiples veces seguidas
        if (!puedeRecibirDaño) return;

        vidaActual = Mathf.Clamp(vidaActual - dano, 0, vidaMaxima);

        Debug.Log("Vida actual: " + vidaActual);

        if (vidaActual <= 0)
        {
            MuerteJugador();
            return;
        }

        StartCoroutine(Invulnerabilidad());
    }

    private IEnumerator Invulnerabilidad()
    {
        puedeRecibirDaño = false;
        yield return new WaitForSeconds(tiempoInvulnerable);
        puedeRecibirDaño = true;
    }

    private void MuerteJugador()
    {
        Debug.Log("Jugador muerto");
        Destroy(gameObject);
    }

    // 🔹 Métodos para UI
    public int ObtenerVidaActual()
    {
        return vidaActual;
    }

    public int ObtenerVidaMaxima()
    {
        return vidaMaxima;
    }
}