using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicBarsController : MonoBehaviour
{
    public static CinematicBarsController Instance { get; private set; }
    [SerializeField] private GameObject cinematicBarContainerGO;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        // cheeeky singleton
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

    }
    public void ShowBars()
    {
        cinematicBarContainerGO.SetActive(true);
    }

    public void HideBars()
    {
        if (cinematicBarContainerGO.activeSelf)
        {
            StartCoroutine("HideBarsAndDisable");
        }
    }

    private IEnumerator HideBarsAndDisable()
    {
        animator.SetTrigger("HideCinematicBars");
        yield return new WaitForSeconds(3);
        cinematicBarContainerGO.SetActive(false);
    }
}
