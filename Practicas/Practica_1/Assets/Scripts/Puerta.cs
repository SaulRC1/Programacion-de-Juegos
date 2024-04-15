using UnityEngine;

public class Puerta : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private ControlCamara cam;

    private void Awake()
    {
        cam = Camera.main.GetComponent<ControlCamara>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.transform.position.x < transform.position.x)
            {
                cam.MoveToNewRoom(nextRoom);
            } else
            {
                cam.MoveToNewRoom(previousRoom);
            }
        }
    }
}
