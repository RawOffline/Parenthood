using UnityEngine;

public class OnChild : MonoBehaviour
{
    public GameObject parent;

    private bool isTouching = false;
    private bool ignoring = false;
    private void Start()
    {
    }
    void Update()
    {
        isTouching = Physics2D.OverlapCircle(transform.position, 1f, 11);

        if (isTouching && (parent.transform.position.y - 0.1f) > transform.position.y)
        {
            ignoring = true;
            
        }
        else
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), parent.GetComponent<Collider2D>(), false);
        }

        if (ignoring)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), parent.GetComponent<Collider2D>(), true);
        }
    }
}
