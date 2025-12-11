using UnityEngine;

public class HideObjective : MonoBehaviour
{
    public GameObject TargetObejct;
    // Update is called once per frame
    void Update()
    {
        TargetObejct.SetActive(GameManager.Instance.Finished);
    }
}
