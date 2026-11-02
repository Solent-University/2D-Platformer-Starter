using UnityEngine;
using UnityEngine.Events;

public class TriggerDelegator : MonoBehaviour
{
    public Direction direction;
    public UnityEvent<Direction> enterEvent;
    public UnityEvent<Direction> exitEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enterEvent.Invoke(direction);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        exitEvent.Invoke(direction);
    }
}
