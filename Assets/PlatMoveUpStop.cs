using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatMoveUpStop : MonoBehaviour
{
    public float speed = 2.0f;
    public float maxHeightSmall = 5.0f; // Adjust the maximum height as needed
    public float maxHeightLarge = 10.0f; // Adjust the maximum height as needed
    public bool isSmall;
    public bool isLarge;

    public void Update()
    {
        MoveUP();
    }

    public void MoveUP()
    {
        if (isSmall)
        {

        // Move the platform upwards
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // If the platform reaches the maximum height, stop moving
        if (transform.position.y >= maxHeightSmall)
        {
            // Set the position to the exact maximum height to prevent overshooting
            transform.position = new Vector3(transform.position.x, maxHeightSmall, transform.position.z);

            // Optionally, you can stop further movement by disabling the script or setting speed to 0
            // enabled = false; // Uncomment this line to disable the script
            // speed = 0; // Uncomment this line to set the speed to 0
        }
        }

        if(isLarge)
        {
            // Move the platform upwards
            transform.Translate(Vector3.up * speed * Time.deltaTime);

            // If the platform reaches the maximum height, stop moving
            if (transform.position.y >= maxHeightLarge)
            {
                // Set the position to the exact maximum height to prevent overshooting
                transform.position = new Vector3(transform.position.x, maxHeightLarge, transform.position.z);

                // Optionally, you can stop further movement by disabling the script or setting speed to 0
                // enabled = false; // Uncomment this line to disable the script
                // speed = 0; // Uncomment this line to set the speed to 0
            }
        }
    }
}
