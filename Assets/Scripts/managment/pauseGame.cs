using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public AudioClip click;

    public void changePause()
    {
        GameManager.Instance.PlaySFX(click);
        if (GameManager.Instance.isPaused)
            Resume();
        else
            Pause();
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameManager.Instance.isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameManager.Instance.isPaused = true;
    }
}
