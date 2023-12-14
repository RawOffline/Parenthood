using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ParticleSystem particleEffect;

    public void OnPointerEnter(PointerEventData eventData)
    {
        particleEffect.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        particleEffect.Stop();
    }
}
