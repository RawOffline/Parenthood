using UnityEngine;

public class BobbingWave : MonoBehaviour
{
    private void Update()
    {
        IsBobbingSmall();
    }
    private void IsBobbingSmall()
    {
        float oscillationY = transform.position.y + Mathf.Sin(Time.time + 10f) * 0.0005f;
        transform.position = new Vector3(transform.position.x, oscillationY, transform.position.z);
    }
}

