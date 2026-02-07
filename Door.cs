using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3 openPosition;
    public float speed = 2f;
    private bool open = false;

    public void OpenDoor()
    {
        open = true;
    }

    public bool IsFullyOpen => open && Vector3.Distance(transform.position, openPosition) < 0.02f;

    void Update()
    {
        if (open)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                openPosition,
                speed * Time.deltaTime
            );
        }
    }
}
