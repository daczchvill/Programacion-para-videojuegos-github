using UnityEngine;

public class SpiderAttack : MonoBehaviour
{
    public Animator anim;
    public float attackCooldown = 1.5f;
    private bool canAttack = true;

    private void Start()
    {
        if (anim == null)
            anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TryAttack();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TryAttack();
        }
    }

    void TryAttack()
    {
        if (!canAttack) return;

        anim.SetTrigger("Attack");
        canAttack = false;
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    void ResetAttack()
    {
        canAttack = true;
    }
}

