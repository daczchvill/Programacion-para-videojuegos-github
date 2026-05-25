using UnityEngine;
using System.Collections;

public class SpiderCollectible : MonoBehaviour
{
    public int value = 1;
    public float respawnTime = 5f;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AtaqueRana"))
        {
            FrogGameManager.Instance.AddFlies(value);

            Destroy(other.gameObject);

            // Iniciar respawn desde el GameManager (que nunca se desactiva)
            FrogGameManager.Instance.StartCoroutine(RespawnSpider());
        }
    }

    IEnumerator RespawnSpider()
    {
        gameObject.SetActive(false);

        yield return new WaitForSeconds(respawnTime);

        transform.position = originalPosition;
        transform.rotation = originalRotation;

        gameObject.SetActive(true);
    }
}


