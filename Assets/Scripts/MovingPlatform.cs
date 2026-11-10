using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector2 pointA;
    public Vector2 pointB;
    public float speed = 1;

    private Vector2 nextPoint;

    private void Awake()
    {
        nextPoint = pointA;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, nextPoint, speed * Time.deltaTime);
        if (transform.position == new Vector3(nextPoint.x, nextPoint.y, 0))
        {
            // Change nextPoint to the opposite of what it was currently
            if (nextPoint == pointA)
            {
                nextPoint = pointB;
            }
            else
            {
                nextPoint = pointA;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(pointA, 0.5f);
        Gizmos.DrawLine(transform.position, pointA);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(pointB, 0.5f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(pointA, pointB);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "Player")
    //    {
    //        collision.transform.parent = transform;
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log(collision.contacts[0].normal);
            if (collision.contacts[0].normal == Vector2.down)
            {
                collision.transform.parent = transform;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.transform.parent = null;
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.tag == "Player")
    //    {
    //        collision.transform.parent = null;
    //    }
    //}
}
