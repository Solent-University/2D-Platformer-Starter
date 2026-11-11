using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float timeToFall = 1;
    public bool respawn;
    public float respawnTime = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            if (collision.contacts[0].normal == Vector2.down)
            {
                StartCoroutine(FallDelayed());
            }
        }
    }

    private IEnumerator FallDelayed()
    {
        yield return new WaitForSeconds(timeToFall);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        if (respawn)
        {
            StartCoroutine(RespawnDelayed());
        }
    }

    private IEnumerator RespawnDelayed()
    {
        yield return new WaitForSeconds(respawnTime);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent <Collider2D>().enabled = true;
    }
}
