using UnityEngine;

public class Daño : MonoBehaviour
{
    private float ultimoDaño;
    [SerializeField] private int dañoPorToque = 1;
    [SerializeField] private float cooldown = 0.5f;


    void OnTriggerEnter(Collider collision)
    {
        if (Time.time < ultimoDaño + cooldown) return;

        if (collision.TryGetComponent(out VidaRana vidaRana))
        {
            vidaRana.TomarDaño(dañoPorToque);
            ultimoDaño = Time.time;
        }
    }
}