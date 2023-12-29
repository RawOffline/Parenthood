using Cinemachine;
using UnityEngine;

public class ZoomOutTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            virtualCamera.Priority = 20;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            virtualCamera.Priority = 1;
        }
    }

}
