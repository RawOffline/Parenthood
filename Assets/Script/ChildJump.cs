using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildJumpOverSmall : MonoBehaviour
{
    private Follow follow;
    private SquishAndStretch squishAndStretch;
    private SpriteRenderer sprite;
    private RaycastHit2D stepRay;

    private int jumpIndex = 0;
    private List<Vector3> childrenPositions = new List<Vector3>();

    [SerializeField] private LayerMask childJumpStepLayer;

    private float maxRaycastDistance = 4f;
    private float jumpDistanceThreshold = 0.3f;

    float stepDistance;
    private bool isJumpInProgress = false;
    public bool smallStepRay = false;

    void Start()
    {

        squishAndStretch = GetComponent<SquishAndStretch>();
        follow = GetComponent<Follow>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        RayCast();

        if (stepRay.collider != null)
        {
            smallStepRay = true;
            follow.canJump = false;
            StartCoroutine(StartProcessCoroutine());
        }
        else
        {
            smallStepRay = false;
        }
    }

    private IEnumerator StartProcessCoroutine()
    {
        yield return new WaitForEndOfFrame(); 
        follow.canJump = false;
        if (!isJumpInProgress)  
        {
            InitiateJump();
        }
    }

    private void RayCast()
    {
        stepRay = Physics2D.Raycast(transform.position, Vector2.right * (sprite.flipX ? -1f : 1f), maxRaycastDistance, childJumpStepLayer);
        stepDistance = Mathf.Abs(stepRay.point.x - transform.position.x);

        if (stepRay.collider != null)
        {
            Transform parentTransform = stepRay.collider.transform;
            childrenPositions.Clear();

            foreach (Transform childTransform in parentTransform)
            {
                Vector3 childPosition = childTransform.position;
                childrenPositions.Add(childPosition);
            }
        }
    }


    private void InitiateJump()
    {
        if (jumpIndex < childrenPositions.Count)
        {
            if (stepDistance < jumpDistanceThreshold && follow.isGrounded)
            {
                Sequence sequence = DOTween.Sequence();
                isJumpInProgress = true; 
                for (int i = 0; i <= childrenPositions.Count; i++)
                {
                    squishAndStretch.enabled = false;
                    sequence.Append(transform.DOJump(childrenPositions[i], 0.15f, 1, 0.4f));
                    jumpIndex++;
                    if (jumpIndex >= childrenPositions.Count)
                    {
                        squishAndStretch.enabled = true;
                        Kill();
                        childrenPositions.Clear();
                        isJumpInProgress = false;
                        jumpIndex = 0;
                    }
                }
            }

        }
        else
        {
            isJumpInProgress = false;
        }
    }


    private void OnDisable()
    {
        Kill();
    }

    private void Kill()
    {
        squishAndStretch.enabled = true;
        transform.DOKill();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 11 && isJumpInProgress)
        {
            StopCoroutine(StartProcessCoroutine());
            Kill();
            childrenPositions.Clear();
            isJumpInProgress = false;
            jumpIndex = 0;
        }
    }
}
