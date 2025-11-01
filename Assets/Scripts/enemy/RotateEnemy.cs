using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class RotateEnemy : MonoBehaviour
{
    [Header("Orbit Settings")]
    public Transform centerPoint; // The point to orbit around
    public float radius = 5f;     // Distance from the center
    public float angularSpeed = 30f; // Degrees per second
    public bool clockwise = true;

    [Header("Rotation Settings")]
    public bool lookAtCenter = true; // If true, object will face the center

    public bool flyAway = false;

    private float angle; // Current angle in degrees
    public float angleDif = 5f;
    public int angleOffset;

    void Start()
    {
        centerPoint = gameObject.transform.parent.transform;
        radius = radius + Random.Range(-0.8f, 1f)*3;

        // Random value between -15 and +40 for speed
        angularSpeed = angularSpeed + Random.Range(-1f, 1f)*25;
    }
    void Update()
    {
        if (flyAway == true)
        {
            transform.position += transform.up * angularSpeed * Time.deltaTime;
            return;
        } 
        // Increase or decrease the angle over time
        angle += (clockwise ? -1 : 1) * angularSpeed * Time.deltaTime;

        // Convert angle to radians
        float rad = angle * Mathf.Deg2Rad;

        // Calculate new position
        Vector3 newPos = centerPoint.position + new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * radius;
        transform.position = newPos;

        // Rotate to face the center
        if (lookAtCenter)
        {
            Vector3 directionToCenter = centerPoint.position - transform.position;
            float angleToCenter = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg + angleOffset;
            transform.rotation = Quaternion.Euler(0, 0, angleToCenter + angleDif * Random.value * Mathf.Sin(rad)); // Adjust so forward is toward center
        }
    }
}