using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PenguinWalk : MonoBehaviour
{
    public Tilemap tilemap;
    public float moveSpeed = 2f;

    private List<Vector3Int> path;
    private int currentStep = 0;
    private bool moving = false;

    public void SetPath(List<Vector3Int> newPath)
    {
        path = newPath;
        currentStep = 0;
        moving = true;
        transform.position = tilemap.GetCellCenterWorld(path[0]);
    }

    private void Update()
    {
        if (!moving || path == null || currentStep >= path.Count)
            return;

        Vector3 targetPos = tilemap.GetCellCenterWorld(path[currentStep]);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.01f)
        {
            HandleTileLogic(path[currentStep]);
            currentStep++;
        }
    }

    private void HandleTileLogic(Vector3Int cell)
    {
        var tile = tilemap.GetTile(cell) as PathTile;
        if (tile == null) return;

        // Example logic
        if (tile.isBreakable)
        {
            tilemap.SetTile(cell, null); // Remove tile
        }

        // Add logic for one-way, blockers, etc.
    }
}
