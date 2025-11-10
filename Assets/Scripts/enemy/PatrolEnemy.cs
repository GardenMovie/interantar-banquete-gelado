using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PatrolEnemy : MonoBehaviour
{

    public float moveSpeed = 1;
    public GameObject enemy;

    private List<Vector3> patrolPoints = new List<Vector3>();
    private int patrolSection = 0;
    private Vector3 enemyPos;
    private Vector3 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int child = 0; child < gameObject.transform.childCount - 1; child++)
        {
            patrolPoints.Add(gameObject.transform.GetChild(child).transform.position);
            gameObject.transform.GetChild(child).gameObject.SetActive(false);
        }

        moveSpeed = Random.Range(moveSpeed * 0.8f, moveSpeed * 1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, patrolPoints[patrolSection], moveSpeed * Time.deltaTime);
        direction = (patrolPoints[patrolSection] - enemy.transform.position).normalized;

        // Instant snap (use if you want no smoothing):
        if (direction.sqrMagnitude > 0.0001f)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // right-facing sprite
            // if sprite faces up, use: angle -= 90f;
            angle += 90f;
            enemy.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        if (enemy.transform.position == patrolPoints[patrolSection])
        {
            patrolSection = (patrolSection == patrolPoints.Count - 1) ? 0 : patrolSection + 1;
        }
    }
}
