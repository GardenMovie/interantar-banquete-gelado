using System.Collections.Generic;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int child = 0; child < gameObject.transform.childCount-1; child++)
        {
            patrolPoints.Add(gameObject.transform.GetChild(child).transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, patrolPoints[patrolSection], moveSpeed * Time.deltaTime);
        if (enemy.transform.position == patrolPoints[patrolSection])
        {
            patrolSection = (patrolSection == patrolPoints.Count - 1) ? 0 : patrolSection + 1;
        }
    }
}
