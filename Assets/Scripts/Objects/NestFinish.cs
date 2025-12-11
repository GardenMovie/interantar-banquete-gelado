using UnityEngine;

public class NestFinish : MonoBehaviour
{
    private Collider2D ObjectCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ObjectCollider = GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.Instance.Finished == true) GameManager.Instance.ChangeScene("FinalMenu");
    }

}