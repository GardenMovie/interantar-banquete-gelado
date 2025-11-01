using System.Collections.Generic;
using UnityEngine;

public class Hider : MonoBehaviour
{
    // public List<GameObject> children = new List<GameObject>
    void Update()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (i != GameManager.Instance.globeTargetIndex + 1)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}