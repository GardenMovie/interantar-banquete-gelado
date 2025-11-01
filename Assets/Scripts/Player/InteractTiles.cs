using System.Numerics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InteractTiles : MonoBehaviour
{
    public Tilemap tilemapMain;
    void Update()
    {
        PathTile tilePlaced = (PathTile) tilemapMain.GetComponent<Tilemap>().GetTile(Vector3Int.FloorToInt(gameObject.transform.position)); 
        Debug.Log(tilePlaced);
    }
}
