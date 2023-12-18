using UnityEngine;

public class LonelyScript : MonoBehaviour
{
    private Renderer lonelyRenderer;
    private Material uniqueMaterial;
    public Transform child;
    private float currentAlpha = 1f;

    private float maxDistance = 3f;
    private float speed = 3f;

    private void Start()
    {
        lonelyRenderer = GetComponent<Renderer>();
        uniqueMaterial = lonelyRenderer.material;
    }

    void Update()
    {
        float distance = Mathf.Abs(child.position.x - transform.position.x);

        if (distance > maxDistance)
        {
            currentAlpha += speed * Time.deltaTime;
        } 
        else
        {
            currentAlpha -= (speed * 5) * Time.deltaTime;
        }


        uniqueMaterial.SetFloat("_CircleSize", currentAlpha);
    }
}


