using UnityEngine;

public class ScaleDown : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(GameManager.Instance.characterChoice == 0) gameObject.transform.localScale = new UnityEngine.Vector3(0.1f, 0.1f, 0.1f);
    }
}
