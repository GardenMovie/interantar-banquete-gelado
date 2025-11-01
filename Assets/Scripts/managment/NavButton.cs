using Unity.VisualScripting;
using UnityEngine;

public class NavButton : MonoBehaviour
{
    public enum Options {TrocaCena, Dialog, Toggler, Nothing}
    public Options TipoBotao = Options.TrocaCena;
    public AudioClip click;

    public GameObject targetObject;

    public int globalTargetSet;
    public int addNextSet;
    public string stringToSend = "Tem certeza que deseja ?";
    public string urlToSend = "quit";



    public void OnButtonClicked()
    {
        GameManager.Instance.PlaySFX(click);
        switch (TipoBotao)
        {
            case Options.Nothing: return;
            case Options.Dialog:
                ConfirmPanel.Instance.Show(stringToSend, urlToSend);
                // targetObject.GetComponent<ConfirmPanel>().Show(stringToSend, urlToSend);
                return;
            case Options.Toggler:
                targetObject.SetActive(true);
                return;
            default:
                GameManager.Instance.globeTargetIndex = (globalTargetSet != 0) ? globalTargetSet : 0;
                GameManager.Instance.ChangeScene(stringToSend);
                return;
        }
    }
}