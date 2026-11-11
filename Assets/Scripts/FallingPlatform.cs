using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float timeToFall = 1;
    public bool animatedFall;
    public bool respawn;
    public float respawnTime = 1;

    private Vector3 startPos;
    private float startRot;

    private void Awake()
    {
        startPos = transform.position;
        startRot = transform.rotation.z;
    }

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

        GetComponent<Collider2D>().enabled = false;
        if (animatedFall)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Rigidbody2D>().AddTorque(0.5f, ForceMode2D.Impulse);
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }

        if (respawn)
        {
            StartCoroutine(RespawnDelayed());
        }
    }

    private IEnumerator RespawnDelayed()
    {
        yield return new WaitForSeconds(respawnTime);

        GetComponent <Collider2D>().enabled = true;
        if (animatedFall)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            GetComponent<Rigidbody2D>().angularVelocity = 0;
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            transform.position = startPos;
            transform.eulerAngles = new Vector3(0, 0, startRot);
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
