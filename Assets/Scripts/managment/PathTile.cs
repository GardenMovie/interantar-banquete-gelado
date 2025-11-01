
using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Tiles/PathTile")]
public class PathTile : Tile
{
    public enum Direction { Up, Down, Left, Right }
    public Direction currentDirection;

    public bool upDir = false;
    public bool leftDir = false;
    public bool downDir = false;
    public bool rightDir = false;

    public bool isOneWay = false;
    public bool isBreakable = false;
    public bool isWalkable = true;

    public int rotations;
    public GameObject Colider;

    // Default direction vector for your arrow tile (e.g. pointing right)
    private Vector3 defaultDirection = Vector3.right;
    private Vector3Int tilePos;
    private Tilemap tileMap;

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        tileMap = tilemap.GetComponent<Tilemap>();
        tilePos = position;
        GetTileDirection(tilePos, tileMap);
        // Debug.Log(rotations);
        return base.StartUp(position, tilemap, go);
    }

    public string RelativeDir(Vector3Int prevTilePos, Vector3Int tilePos)
    {
        // Debug.Log("Dirs " + upDir + leftDir + downDir+ rightDir);
        Vector3Int delta = tilePos - prevTilePos ;
        if (Mathf.Abs(delta.x) + Mathf.Abs(delta.y) == 1)
        {
            if (delta.x == 1) return "right";
            if (delta.x == -1) return "left";
            if (delta.y == 1) return "up";
            if (delta.y == -1) return "down";
        }
        return "not";
    }

    public static bool AreAdjacentNESW(Vector3Int a, Vector3Int b)
    {
        Vector3Int delta = a - b;

        // Check if they are 1 unit apart in exactly one axis (x or y), and same on the others
        return (Mathf.Abs(delta.x) == 1 && delta.y == 0 && delta.z == 0) ||
            (Mathf.Abs(delta.y) == 1 && delta.x == 0 && delta.z == 0);
    }

    public void GetTileDirection(Vector3Int tilePos, Tilemap tilemap)
    {
        Matrix4x4 matrix = tilemap.GetTransformMatrix(tilePos);
        Quaternion rotation = Quaternion.LookRotation(
            matrix.GetColumn(2),
            matrix.GetColumn(1)
        );
        Vector3 rotatedDirection = rotation * defaultDirection;
        Vector2 dir2D = new Vector2(rotatedDirection.x, rotatedDirection.y).normalized;

        // Calculate how many 90 degree clockwise rotations the tile has
        if (Vector2.Dot(dir2D, Vector2.up) > 0.9f) rotations = 1;
        else if (Vector2.Dot(dir2D, Vector2.left) > 0.9f) rotations = 2;
        else if (Vector2.Dot(dir2D, Vector2.down) > 0.9f) rotations = 3;
        else rotations = 0;  // right is default

        // for (int i = 0; i < rotations; i++)
        //     RotateDirections90Clockwise();
    }

    private void RotateDirections90Clockwise()
    {
        bool temp = upDir;
        upDir = leftDir;
        leftDir = downDir;
        downDir = rightDir;
        rightDir = temp;
    }
}