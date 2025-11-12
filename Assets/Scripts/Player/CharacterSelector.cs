using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public int index = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void BtnClicked()
    {
        GameManager.Instance.characterChoice = index;
        
    }
}
