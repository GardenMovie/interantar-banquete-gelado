using TMPro;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;

public class FpsCounter : MonoBehaviour
{
    public TextMeshProUGUI fpsText;

    private float deltaTime = 0.0f;
    public int type = 0;

    void Start()
    {
        fpsText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (type == 0)
        {
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            fpsText.text = $"FPS: {Mathf.CeilToInt(fps)}";
        }
        if (type == 2)
        {
            fpsText.text = $"{GameManager.Instance.health}";
        }
        else
        {
            fpsText.text = $"{GameManager.Instance.score}";
        }
    }
}