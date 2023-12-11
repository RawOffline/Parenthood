using System.Collections;
using UnityEngine;

public class ChildJumpOverSmall : MonoBehaviour
{
    Follow follow;
    Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float maxRaycastDistance = 15f;
    [SerializeField] private float wallDistanceThreshold = 7f;
    [SerializeField] private float wallApproachThreshold = 5f;
    [SerializeField] private float jumpDistanceThreshold = 2f;

    void Start()
    {
        follow = GetComponent<Follow>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, new Vector2(1f, 0), maxRaycastDistance, groundLayer);

        if (hitInfo.collider != null)
        {
            float distance = Mathf.Abs(hitInfo.point.x - transform.position.x);

            if (distance < wallDistanceThreshold)
            {
                //follow.canJump = false;
            }

            // Optional: Uncomment this section if you want to handle wall approach
            //if (distance < wallApproachThreshold)
            //{
            //    Debug.Log("Approaching wall");
            //    if (rb.velocity.x > 0f && follow.isGrounded)
            //    {
            //        rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, -0.1f, Time.deltaTime * 3), rb.velocity.y);
            //    }
            //}

            if (distance < jumpDistanceThreshold && follow.isGrounded)
            {
                Collider2D collider = hitInfo.collider;

                Vector3 colliderCenter = collider.bounds.center;
                Vector3 colliderExtents = collider.bounds.extents;

                Vector3 topCenterPosition = new Vector3(colliderCenter.x, colliderCenter.y + colliderExtents.y, colliderCenter.z);

                // Calculate the displacement in both x and y directions
                float displacementX = topCenterPosition.x - transform.position.x;
                float displacementY = topCenterPosition.y - transform.position.y;

                // Calculate the total distance to the topCenterPosition
                float totalDistance = Mathf.Sqrt(displacementX * displacementX + displacementY * displacementY);

                // Calculate the angle of the trajectory (in radians)
                float angle = Mathf.Atan2(displacementY, displacementX);

                // Define the speed or magnitude of the force
                float speed = 1.5f;  // Adjust this value as needed

                // Calculate the horizontal and vertical components of the force
                float forceX = Mathf.Cos(angle) * speed * totalDistance;
                float forceY = Mathf.Sin(angle) * speed * totalDistance;

                // Apply the force
                Vector2 force = new Vector2(forceX, forceY);
                rb.AddForce(force, ForceMode2D.Impulse);
            }

            Transform target;
            float initialAngle;
            void Jump()
            {
                Vector2 targetPosition = new Vector2(target.position.x, target.position.y);
                float gravity = Physics2D.gravity.magnitude;
                float angle = initialAngle * Mathf.Deg2Rad;

                Vector2 position = new Vector2(transform.position.x, transform.position.y);

                // Planar distance between objects
                float distance = Vector2.Distance(targetPosition, position);
                // Distance along the y axis between objects
                float yOffset = position.y - targetPosition.y;

                float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

                Vector2 velocity = new Vector2(initialVelocity * Mathf.Cos(angle), initialVelocity * Mathf.Sin(angle));

                // Rotate our velocity to match the direction between the two objects
                float angleBetweenObjects = Vector2.SignedAngle(Vector2.up, targetPosition - position);
                Vector2 finalVelocity = Quaternion.Euler(0f, 0f, angleBetweenObjects) * velocity;

                rb.AddForce(finalVelocity * rb.mass, ForceMode2D.Impulse);
            }

        }
    }
}
