using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PgmChooser : MonoBehaviour
{
    SpriteRenderer PgmImage;
    public float MaxSize;
    public float MinSize;

    public Sprite PgmAdelia;
    public Sprite PgmImperador;
    public Sprite PgmPapua;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PgmImage = gameObject.GetComponent<SpriteRenderer>();
        switch (GameManager.Instance.characterChoice)
        {
            case 0:
                PgmImage.sprite = PgmAdelia;
                MinSize = 0.13f;
                MaxSize = 0.14f;
                break;
            case 1:
                PgmImage.sprite = PgmImperador;
                MinSize = 0.18f;
                MaxSize = 0.19f;
                break;
            case 2:
                PgmImage.sprite = PgmPapua;
                MinSize = 0.15f;
                MaxSize = 0.17f;
                break;
        }

        float FinalSize = Random.Range(MinSize,MaxSize);
        gameObject.transform.localScale = new UnityEngine.Vector3 (FinalSize,FinalSize,FinalSize);
        
        gameObject.transform.Rotate(0,0,Random.Range(0,360));

    }
}
