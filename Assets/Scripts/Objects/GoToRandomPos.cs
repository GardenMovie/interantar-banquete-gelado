using UnityEngine;

public class GoToRandomPos : MonoBehaviour
{
    Vector3 randomPoint;
    BoundingParent ParentObject;
    public int moveSpeed = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ParentObject = gameObject.GetComponentInParent<BoundingParent>();
        randomPoint = ParentObject.GetRandomPointInBounds();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, randomPoint, moveSpeed * Time.deltaTime);

        float angle = Mathf.Atan2(randomPoint.y - gameObject.transform.position.y, randomPoint.x - gameObject.transform.position.x) * Mathf.Rad2Deg + 180;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, moveSpeed*2 * Time.deltaTime);

        if (gameObject.transform.position == randomPoint)
        {
            randomPoint = ParentObject.GetRandomPointInBounds();
        }
    }
}
