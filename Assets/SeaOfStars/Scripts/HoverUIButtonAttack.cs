using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverUIButtonAttack : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private CanvasGroup _canvasGroup;
    private Image img;

    private void Start()
    {
        if (!img)
        {
            img = GetComponent<Image>();
        }
    }

    private void OnDisable()
    {
        if (_canvasGroup)
        {
            _canvasGroup.gameObject.SetActive(false);
            _canvasGroup.DOFade(0f, 0.2f);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        img.DOColor(Color.green, 0.3f).SetEase(Ease.Linear);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        img.DOColor(Color.white, 0.3f).SetEase(Ease.Linear);
    }

    public void ShowUI()
    {
        if (_canvasGroup)
        {
            _canvasGroup.gameObject.SetActive(true);
            _canvasGroup.DOFade(1f, 0.2f);
        }
    }
}
