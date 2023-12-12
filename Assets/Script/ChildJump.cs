using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildJumpOverSmall : MonoBehaviour
{
    private Follow follow;
    private SpriteRenderer sprite;
    private RaycastHit2D stepRay;

    private int jumpIndex = 0;
    private List<Vector3> childrenPositions = new List<Vector3>();

    [SerializeField] private LayerMask childJumpStepLayer;
    [SerializeField] private LayerMask parentLayer;

    private float maxRaycastDistance = 2f;
    private float jumpDistanceThreshold = 0.3f;

    float stepDistance;
    private bool isJumpInProgress = false;

    void Start()
    {
        follow = GetComponent<Follow>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        RayCast();

        if (stepRay.collider != null)
        {
            Debug.Log("hit");
            StartCoroutine(StartProcessCoroutine());
        }
        else
        {
            follow.canJump = true;
            follow.maxSpeed = 2.5f;
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
                
                isJumpInProgress = true; 
                transform.DOJump(childrenPositions[jumpIndex], 0.15f, 1, 0.5f).OnComplete(AddJumpCount);
            }

        }
        else
        {
            isJumpInProgress = false;
            follow.isFollowing = true;
        }
    }

    private void AddJumpCount()
    {
        isJumpInProgress = false;
        jumpIndex++;

        if (jumpIndex >= 4)
        {
            Kill();
            childrenPositions.Clear();
            jumpIndex = 0;
        }
    }

    private void OnDisable()
    {
        Kill();
    }

    private void Kill()
    {
        transform.DOKill();
    }
}
