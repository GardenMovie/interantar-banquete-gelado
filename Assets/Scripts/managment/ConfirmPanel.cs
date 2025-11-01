using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmPanel : MonoBehaviour
{
    public static ConfirmPanel Instance { get; private set; }

    public GameObject panel;
    public TMP_Text messageText;
    public Button confirmButton;
    public Button closeButton;

    private string currentUrl;

    void Awake()
    {
        Instance = this;

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        panel.SetActive(false);
        panel.transform.localPosition = new Vector3(0, 0, 0);
        confirmButton.onClick.AddListener(OnConfirm);
        closeButton.onClick.AddListener(Hide);
    }

    public void Show(string message, string url)
    {
        messageText.text = message;
        currentUrl = url;
        panel.SetActive(true);
    }

    private void OnConfirm()
    {
        if (!string.IsNullOrEmpty(currentUrl))
            if (currentUrl.ToLower() == "quit")
            {
                Debug.Log("quitting");
                Application.Quit();
            }
            else
            {
                Application.OpenURL(currentUrl);
            }
        Hide();
    }

    private void Hide()
    {
        panel.SetActive(false);
    }
}
