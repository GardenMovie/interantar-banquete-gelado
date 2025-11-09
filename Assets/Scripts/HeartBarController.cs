// ...existing code...
using UnityEngine;

public class HeartBarController : MonoBehaviour
{
    public GameObject heartPrefab; // assign your heart prefab (should be a UI element if parent uses HorizontalLayoutGroup)

    void OnEnable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnHealthChanged += UpdateHearts;
    }

    void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnHealthChanged -= UpdateHearts;
    }

    void Start()
    {
        if (GameManager.Instance != null)
            UpdateHearts(GameManager.Instance.health);
    }

    void UpdateHearts(int currentHealth)
    {
        if (heartPrefab == null)
        {
            Debug.LogWarning("HeartBarController: heartPrefab is not assigned.");
            return;
        }

        int n = Mathf.Max(0, currentHealth);

        // Remove existing children
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        // Instantiate n prefabs as children. Parent should handle layout (HorizontalLayoutGroup).
        for (int i = 0; i < n; i++)
        {
            var heart = Instantiate(heartPrefab, transform);
            // keep prefab's local settings; for UI prefabs ensure RectTransform behaves correctly:
            heart.transform.localScale = Vector3.one;
        }
    }
}
//