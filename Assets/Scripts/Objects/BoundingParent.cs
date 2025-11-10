using UnityEngine;

public class BoundingParent : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private GameObject prefab; // prefab to spawn
    [SerializeField] private int amountToSpawn = 5; // how many to spawn

        private float leftX;
        private float rightX;
        private float bottomY;
        private float topY;

    private void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        leftX   = sr.bounds.min.x;
        rightX  = sr.bounds.max.x;
        bottomY = sr.bounds.min.y;
        topY    = sr.bounds.max.y;
        SpawnChildren();
    }

    private void SpawnChildren()
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            GameObject child = Instantiate(prefab, transform);
            child.transform.position = GetRandomPointInBounds();
            child.GetComponent<SpriteRenderer>().flipY = Random.value > 0.5f;

        }
    }

    public Vector3 GetRandomPointInBounds()
    {
        // Pick a random point within boundingSize
        float x = Random.Range(leftX, rightX);
        float y = Random.Range(bottomY , topY);
        float z = gameObject.transform.position.z;

        return new Vector3(x, y, z);
    }
}