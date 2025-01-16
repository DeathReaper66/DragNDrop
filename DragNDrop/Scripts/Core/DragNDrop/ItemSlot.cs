using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    private RectTransform _rectTransform;
    private Image _image;
    private bool _isOccupied = false;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag && !_isOccupied)
        {
            eventData.pointerDrag.GetComponent<DragNDrop>().OnPutInSlot(_rectTransform, this);
            _isOccupied = true;
            _image.raycastTarget = false;
        }
    }

    public void OnSelect()
    {
        _isOccupied = false;
        _image.raycastTarget = true;
    }
}
