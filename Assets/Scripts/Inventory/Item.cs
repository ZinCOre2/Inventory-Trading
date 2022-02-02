using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image image;
    [SerializeField] private int cost = 100;
    public int Cost => cost;
    [SerializeField] private GameObject costPanel;
    [SerializeField] private Text costText;

    public ItemSlot CurrentSlot { get; private set; }
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;

        costText.text = $"$<b><i><color=#F1DC48>{cost}</color></i></b>";
        
        transform.SetParent(CurrentSlot.transform);
        transform.position = CurrentSlot.transform.position;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        image.DOColor(new Color(0.5f, 1f, 0.5f, 1f), 0.3f);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        image.DOColor(Color.white, 0.3f);
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(UIController.Instance.ItemParent);
        transform.SetAsLastSibling();
        
        image.raycastTarget = false;
        
        costPanel.SetActive(false);

        transform.DOScale(Vector3.one * 1.5f, 0.3f);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        var pos = _camera.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;
        transform.position = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(CurrentSlot.transform);
        transform.position = CurrentSlot.transform.position;
        
        image.raycastTarget = true;
        
        costPanel.SetActive(true);
        
        transform.DOScale(Vector3.one, 0.3f);
    }
    
    
    public void SetSlot(ItemSlot itemSlot)
    {
        CurrentSlot = itemSlot;
    }
}
