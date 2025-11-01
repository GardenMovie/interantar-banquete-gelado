using UnityEngine;

public class gameQuit : MonoBehaviour
{
    public void Quit()
    {
        ConfirmPanel.Instance.Show("Tem certeza que quer sair?", "quit");
    }
}