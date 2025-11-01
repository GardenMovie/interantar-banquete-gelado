using UnityEngine;
using UnityEngine.UIElements;

public class AutoCenter : MonoBehaviour
{

    public GameObject panel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(gameObject.transform.position);
        // gameObject.SetActive(false);
        GetComponent<CanvasGroup>().alpha = 1;
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
