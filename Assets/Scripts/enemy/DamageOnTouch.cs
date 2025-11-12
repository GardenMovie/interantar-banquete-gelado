using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            GameManager.Instance.AddHealth(-1);
        }
    }
}
