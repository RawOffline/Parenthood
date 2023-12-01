using UnityEngine;

public class GrabChild : MonoBehaviour
{
    public GrabArea grabArea;
    private MotherMovementTemp motherMovement;
    public GameObject child;
    private Rigidbody2D childRb;
    private float forceDecrease = 2f;
    private int repeatCount = 0; // Variable to track the number of repetitions
    public LayerMask childLayer;
    public LayerMask parentLayer;

    void Start()
    {
        grabArea = GetComponentInChildren<GrabArea>();
        motherMovement = GetComponent<MotherMovementTemp>();
        childRb = child?.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (grabArea.ChildGrab && child != null && motherMovement.isDashing)
        {
            IgnoreCollisionBetweenLayers(childLayer, parentLayer, true);
            AttachChildToMother();
            childRb.isKinematic = true;
        }
        else if (motherMovement.IsGrounded() && child != null)
        {
            IgnoreCollisionBetweenLayers(childLayer, parentLayer, false);
            DetachChildFromMother();
            childRb.isKinematic = false;
        }
    }

    void IgnoreCollisionBetweenLayers(LayerMask layer1, LayerMask layer2, bool ignore)
    {
        Collider2D[] colliders1 = Physics2D.OverlapCircleAll(transform.position, 100f, layer1);
        Collider2D[] colliders2 = Physics2D.OverlapCircleAll(transform.position, 100f, layer2);

        foreach (Collider2D collider1 in colliders1)
        {
            foreach (Collider2D collider2 in colliders2)
            {
                Physics2D.IgnoreCollision(collider1, collider2, ignore);
            }
        }
    }

    void AttachChildToMother()
    {
        if (child != null)
        {
            child.transform.SetParent(transform);
            MoveChildInAir();
        }
    }

    void DetachChildFromMother()
    {

        if (child != null && child.transform.parent == transform)
        {
            child.transform.SetParent(null);
            CancelInvoke("AddForceToChild");
            InvokeRepeating("AddForceToChild", 0, 0.2f);
        }
    }

    void AddForceToChild()
    {
        childRb?.AddForce(new Vector2(transform.localScale.x * forceDecrease, forceDecrease / 2f), ForceMode2D.Impulse);
        forceDecrease -= 1f;
        repeatCount++;

        if (repeatCount >= 3)
        {
            CancelInvoke("AddForceToChild");
            forceDecrease = 3;
            repeatCount = 0;
        }
    }

    void MoveChildInAir()
    {
        if (child != null)
        {
            Vector2 startPosition = child.transform.localPosition;
            child.transform.localPosition = Vector2.Lerp(startPosition, new Vector2(0.7f, 0f), Time.deltaTime * 7);
        }
    }
}
