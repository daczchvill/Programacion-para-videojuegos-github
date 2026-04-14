using UnityEngine;
using System.Collections;

public class FrogAttackController : MonoBehaviour
{
    [Header("Configuración de Baba")]
    public GameObject slimeBallPrefab;
    public Transform firePoint;
    public float slimeSpeed = 10f;

    [Header("Configuración de Lengua")]
    public GameObject tongueObject; // Aquí arrastraremos el TonguePivot
    public float tongueMaxRange = 5f; 
    public float tongueSpeed = 20f;   
    private bool isAttackingWithTongue = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            LanzarBaba();
        }

        if (Input.GetMouseButtonDown(0) && !isAttackingWithTongue)
        {
            StartCoroutine(AtaqueLengua());
        }
    }

    void LanzarBaba()
    {
        GameObject ball = Instantiate(slimeBallPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Mantenemos rb.linearVelocity si estás en Unity 6, o rb.velocity si es versión anterior
            rb.linearVelocity = firePoint.forward * slimeSpeed;
        }
        Destroy(ball, 3f);
    }

    IEnumerator AtaqueLengua()
    {
        isAttackingWithTongue = true;
        tongueObject.SetActive(true); 

        // Escala inicial: ancho 0.1, alto 0.1, largo 0
        Vector3 escalaInicial = new Vector3(0.1f, 0.1f, 0f);
        // Escala final: ancho 0.1, alto 0.1, largo definido por el rango
        Vector3 escalaFinal = new Vector3(0.1f, 0.1f, tongueMaxRange);

        float t = 0;
        // Extender
        while (t < 1f)
        {
            t += Time.deltaTime * tongueSpeed;
            tongueObject.transform.localScale = Vector3.Lerp(escalaInicial, escalaFinal, t);
            yield return null;
        }

        // Retraer
        while (t > 0f)
        {
            t -= Time.deltaTime * tongueSpeed;
            tongueObject.transform.localScale = Vector3.Lerp(escalaInicial, escalaFinal, t);
            yield return null;
        }

        tongueObject.SetActive(false); 
        isAttackingWithTongue = false;
    }
}