using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;


public class MovingPlatform : MonoBehaviour
{
    public Transform[] coordinatesTransform = new Transform[2];

    public PathType pathSystem = PathType.CatmullRom;

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
        Vector3[] coordinates = new Vector3[coordinatesTransform.Length];

        for (int i = 0; i < coordinatesTransform.Length; i++)
        {
            coordinates[i] = coordinatesTransform[i].position;
        }
        for (int i = 0; i < coordinates.Length; i++)
        {
            Debug.Log(coordinates[i]);
        }

        //transform.DOPath(coordinates,speed,pathSystem);

        // Create the path with DoTween
        var path = transform.DOPath(coordinates, speed, pathSystem);

        // SetLoops to create a back-and-forth loop
        path.SetLoops(-1, LoopType.Yoyo);

        // Optional: You can add more settings as needed, such as easing functions
        path.SetEase(Ease.InOutQuint);


    }



    IEnumerator Wait()
    {
        move = false;
        yield return new WaitForSeconds(waitDuration);
        move = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("child"))
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
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("child"))
        {
            var playerHandler = other.transform.parent.GetComponent<ParentHandler>();
            if (playerHandler != null)
            {
                playerHandler.SetParent(null);
            }
        }
    }
}

