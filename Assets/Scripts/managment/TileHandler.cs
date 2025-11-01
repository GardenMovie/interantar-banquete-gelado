using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using System.Data.Common;
using System.Collections.Generic;
using Unity.VisualScripting; // ← new input system

public class TileHandler : MonoBehaviour
{
    public Tilemap tilemap;
    public Color clickedColor = Color.red;
    private Camera mainCamera;
    public List<Vector3Int> Caminho = new List<Vector3Int>();
    public PenguinWalk Player;
    public static TileHandler Instance { get; private set; }

    void Awake()
    {
        if (tilemap == null)
            tilemap = GetComponent<Tilemap>();
        mainCamera = Camera.main;

        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }


    private void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Player.SetPath(Caminho);
            Debug.Log(string.Join(", ", Caminho));
            Caminho = new List<Vector3Int>();
        }
        if (Mouse.current.leftButton.isPressed)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePosition);
            Vector3Int cellPos = tilemap.WorldToCell(worldPos);

            if (tilemap.HasTile(cellPos) && tilemap.GetTile(cellPos) is PathTile && Caminho.Contains(cellPos) == false)
            {
                if (Caminho.Count > 0)
                {
                    Vector3Int lastTilePos = Caminho[Caminho.Count - 1];
                    if (RelativeDir(lastTilePos, cellPos) == false) return;
                }
                Caminho.Add(cellPos);
                tilemap.SetTileFlags(cellPos, TileFlags.None);
                tilemap.SetColor(cellPos, clickedColor);

            }
        }
    }

    public bool RelativeDir(Vector3Int prevTilePos, Vector3Int tilePos)
    {
        PathTile currTile = (PathTile)tilemap.GetTile(tilePos);
        PathTile prevTile = (PathTile)tilemap.GetTile(prevTilePos);

        Vector3Int delta = tilePos - prevTilePos ;
        if (Mathf.Abs(delta.x) + Mathf.Abs(delta.y) != 1) return false;

        if (currTile.isOneWay)
        {
            if (delta.x == 1)
            {
                return !currTile.leftDir && prevTile.rightDir;
            }
            if (delta.x == -1)
            {
                return !currTile.rightDir && prevTile.leftDir;
            }
            if (delta.y == 1)
            {
                return !currTile.downDir && prevTile.upDir;
            }
            if (delta.y == -1)
            {
                return !currTile.upDir && prevTile.downDir;
            }
        }

        if (delta.x == 1)
        {
            return currTile.leftDir && prevTile.rightDir;
        }
        if (delta.x == -1)
        {
            return currTile.rightDir && prevTile.leftDir;
        }
        if (delta.y == 1)
        {
            return currTile.downDir && prevTile.upDir;
        }
        if (delta.y == -1)
        {
            return currTile.upDir && prevTile.downDir;
        }
        return false;
    }


}
