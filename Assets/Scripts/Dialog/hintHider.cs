using Unity.Burst.CompilerServices;
using UnityEngine;

public class hintHider : MonoBehaviour
{

    public GameObject hintText;
    public GameObject questionImage;
    public GameObject crossImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    public void OnClicked()
    {
        hintText.SetActive(!hintText.activeSelf);
        crossImage.SetActive(hintText.activeSelf);
        questionImage.SetActive(!hintText.activeSelf);
    }
}
