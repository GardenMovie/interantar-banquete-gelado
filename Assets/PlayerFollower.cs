using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset = new Vector3(0,0,-10); // e.g. (0,0,-10) in 2D

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
