using UnityEngine;

public class LonelyScript : MonoBehaviour
{
    private Renderer lonelyRenderer;
    private CheckpointManager checkpoint;
    private Material uniqueMaterial;
    public Transform child;
    public Transform parent;
    private float currentSize = 0f;

    private float maxDistance = 8f;
    private float speed = 0.7f;

    private void Start()
    {
        checkpoint = FindObjectOfType<CheckpointManager>();
        lonelyRenderer = GetComponent<Renderer>();
        uniqueMaterial = lonelyRenderer.material;
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, parent.position, 3 * Time.deltaTime);
        float distance = Mathf.Abs(child.position.x - parent.position.x);

        if (distance > maxDistance)
        {
            currentSize += speed * Time.deltaTime;
            if (currentSize > 10)
            {
                speed = 5;
            }
            if(currentSize > 25)
            {
                currentSize = 0;
                checkpoint.LoadCheckpoint();
            }
        }
        else
        {
            currentSize -= (speed * 5) * Time.deltaTime;
        }

        if (currentSize < 0)
        {
            currentSize = 0;
        }

        uniqueMaterial.SetFloat("_CircleSize", currentSize);
    }

}


