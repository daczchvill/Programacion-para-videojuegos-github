using UnityEngine;
using System.Collections;

public class FrogAttackController : MonoBehaviour
{
    [Header("Configuración de Baba")]
    public GameObject slimeBallPrefab;
    public Transform firePoint;
    public float slimeSpeed = 10f;

   
    [Header("Configuración de Sonido")]
    public AudioClip slimeSound; // Aquí arrastras el archivo de audio
    private AudioSource audioSource;


    void Start()
    {
        // Obtenemos el componente Audio Source al iniciar
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            LanzarBaba();
        }
    }

    void LanzarBaba()
    {
            // Reproducir el sonido antes o después de instanciar
        if (audioSource != null && slimeSound != null)
        {
            audioSource.PlayOneShot(slimeSound);
        }
        
        // SOLUCIÓN: Instanciamos el objeto de forma normal
        GameObject ball = Instantiate(slimeBallPrefab, firePoint.position, firePoint.rotation);
        
        // FUERZA BRUTA ANTI-JERARQUÍAS: Nos aseguramos al 100% de que la bola 
        // no sea hija de la rana ni de la plataforma móvil. Nace libre en el mundo.
        ball.transform.SetParent(null);

        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Aseguramos que las físicas actúen en espacio global absoluto
            rb.isKinematic = false; 
            rb.linearVelocity = firePoint.forward * slimeSpeed;
        }
        
        Destroy(ball, 3f);
    }

    
}