using UnityEngine;

public class CollectOnTouch : MonoBehaviour
{
    public int valueScore = 1;
    public bool requireStrong = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            GameManager.Instance.AddScore(valueScore);
            Destroy(gameObject);
        }
    }
}
