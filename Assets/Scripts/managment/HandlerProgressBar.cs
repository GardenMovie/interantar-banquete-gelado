using UnityEngine;

public class HandlerProgressBar : MonoBehaviour
{
    public int WhatTrack = 0;
    
    float Current;
    float Max;
    GameObject ProgressIndicator;

void Start()
{
    ProgressIndicator = transform.Find("ProgressBar/ProgressIndicator")?.gameObject;
}

void Update()
{
    if (ProgressIndicator != null)
    {
        
        switch (WhatTrack){
            default:
                Current = GameManager.Instance.KrillScore * 1f;
                Max = GameManager.Instance.MaxKrillScore * 1f;
                break;
            case 1:
                Current = GameManager.Instance.FishScore * 1f;
                Max = GameManager.Instance.MaxFishScore * 1f;
                break;
            case 2:
                Current = GameManager.Instance.SquidScore * 1f;
                Max = GameManager.Instance.MaxSquidScore * 1f;
                break;

        }

        Vector3 scale = ProgressIndicator.transform.localScale;
        scale.x = (Current == 0) ? 0 : Current/Max;                 // modify x
        ProgressIndicator.transform.localScale = scale;  // apply back
    }
    else Debug.Log("fuck");
}

}
