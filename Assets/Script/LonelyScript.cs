using UnityEngine;

public class LonelyScript : MonoBehaviour
{
    private Renderer lonelyRenderer;
    private Material uniqueMaterial;
    public Transform child;
    public Transform parent;
    private float currentSize = 0f;

    private float maxDistance = 3f;
    private float speed = 0.5f;

    private void Start()
    {
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
        } 
        else
        {
            currentSize -= (speed * 5) * Time.deltaTime;
        }


        uniqueMaterial.SetFloat("_CircleSize", currentSize);
    }
}


