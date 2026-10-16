using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePairPrefab;

    public float spawnRate = 2.5f;
    public float spawnX = 8f;

    public float gapSize = 3f;
    public float minY = -1.5f;
    public float maxY = 1.5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnPipe), 1f, spawnRate);
    }

    void SpawnPipe()
    {
        float randomY = Random.Range(minY, maxY);

        GameObject pipes = Instantiate(
            pipePairPrefab,
            new Vector3(spawnX, randomY, 0f),
            Quaternion.identity
        );

        Transform topPipe = pipes.transform.Find("TopPipe");
        Transform bottomPipe = pipes.transform.Find("BottomPipe");

        float pipeHeight = 5f;

        if (topPipe != null)
        {
            topPipe.localPosition = new Vector3(
                0f,
                gapSize / 2f + pipeHeight / 2f,
                0f
            );
        }

        if (bottomPipe != null)
        {
            bottomPipe.localPosition = new Vector3(
                0f,
                -(gapSize / 2f + pipeHeight / 2f),
                0f
            );
        }
    }
}