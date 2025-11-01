using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnEnable : MonoBehaviour
{
    public string TargetScene = "MainMenu";
    void OnEnable()
    {
        SceneManager.LoadScene(TargetScene);
    }
}
