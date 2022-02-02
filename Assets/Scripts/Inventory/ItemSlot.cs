using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Profiling.Experimental;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private int ownerId;
    [SerializeField] private Item heldItem;
    public Item HeldItem => heldItem;

    private static Sequence _fadeSequence;
    private static Sequence _riseSequence;

    private void Awake()
    {
        if (!heldItem) { return; }
        
        heldItem.SetSlot(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!eventData.pointerDrag 
            || heldItem != null
            || !eventData.pointerDrag.TryGetComponent(out Item newItem)) { return; }
        if (!GameController.Instance.TradeCheck(newItem.Cost, newItem.CurrentSlot.ownerId, ownerId)) { return; }

        if (newItem.CurrentSlot.ownerId != ownerId)
        {
            UIController.Instance.FloatingText.transform.position = transform.position;
            UIController.Instance.FloatingText.color = Color.white;
            UIController.Instance.FloatingText.text = $"-${newItem.Cost}";

            _fadeSequence?.Kill();
            _fadeSequence = DOTween.Sequence();
            
            _riseSequence?.Kill();
            _riseSequence = DOTween.Sequence();
            
            _fadeSequence.Append(UIController.Instance.FloatingText.DOColor(Color.clear, 1f));
            _riseSequence.Append(UIController.Instance.FloatingText.transform.DOMove(transform.position + Vector3.up * 2f, 1f));
        }

        if (newItem.CurrentSlot != null) { newItem.CurrentSlot.heldItem = null; }
        heldItem = newItem;
        heldItem.SetSlot(this);
    }
}
