using UnityEngine;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine.Tilemaps;

[CustomGridBrush(false, true, false, "Random Rotating Brush")]
public class RandomRotatingBrush : GridBrush
{
    public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
    {
        if (brushTarget == null) return;
        var tilemap = brushTarget.GetComponent<Tilemap>();
        if (tilemap == null) return;

        base.Paint(gridLayout, brushTarget, position);

        // Random 0, 90, 180, 270 rotation
        int angle = 90 * Random.Range(0, 4);
        Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, angle), Vector3.one);
        tilemap.SetTransformMatrix(position, matrix);
    }
}
