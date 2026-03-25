using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player; 

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.transform.position.x + offset.x, transform.position.y, transform.position.z);
        }
    }
}