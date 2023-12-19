using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public List<Transform> coordinates;
    int currentIndex;

    public float waitDuration;
    public float speed = 1f;
    public float percentageDistance;
    [Range(0, 1)] public float startPercentageDistance;

    Transform start;
    Transform end;

    bool move = true;
    Coroutine waitCoroutine;
    private bool loopForwards;

    void Start()
    {
        loopForwards = true;

        start = coordinates[0];
        end = coordinates[1];
        currentIndex = 1;
        percentageDistance = startPercentageDistance;
        move = true;


        if (waitCoroutine != null)
        {
            StopCoroutine(waitCoroutine);
        }
    }

    void FixedUpdate()
    {
        if (move)
        {
            percentageDistance += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(start.position, end.position, percentageDistance);
        }

        if (percentageDistance >= 1)
        {
            waitCoroutine = StartCoroutine(Wait());
            NextCycle();
        }
    }

    void NextCycle()
    {
        percentageDistance = 0;
        start = end;

        if (loopForwards)
        {
            currentIndex++;
        }
        else
        {
            currentIndex--;
        }

        if (currentIndex >= coordinates.Count || currentIndex < 0)
        {
            if (loopForwards)
            {
                currentIndex--;
                loopForwards = false;
            }
            else
            {
                currentIndex++;
                loopForwards = true;
            }
            //currentIndex = 0;
        }

        end = coordinates[currentIndex];
    }

    IEnumerator Wait()
    {
        move = false;
        yield return new WaitForSeconds(waitDuration);
        move = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.CompareTag("child"))
        {
            var parentHandler = other.transform.parent.GetComponent<ParentHandler>();
            if (parentHandler != null)
            {
                parentHandler.SetParent(transform);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.gameObject.CompareTag("child"))
        {
            var playerHandler = other.transform.parent.GetComponent<ParentHandler>();
            if (playerHandler != null)
            {
                playerHandler.SetParent(null);
            }
        }
    }
}

