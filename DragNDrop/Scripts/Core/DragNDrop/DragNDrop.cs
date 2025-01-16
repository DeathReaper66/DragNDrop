using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragNDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas _parentCanvas;
    [SerializeField] private float _gravityScale;
    [NonSerialized] public ItemSlot _currentSlot;

    private Rigidbody2D _rb;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private Image _image;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _image = GetComponent<Image>();

        DragNDropManager.Instance.OnSelectItem += () => CantInteract();
        DragNDropManager.Instance.OnDropItem += () => CanInteract();
    }

    public void OnPutInSlot(RectTransform rectTransform, ItemSlot itemSlot)
    {
        _currentSlot = itemSlot;
        _rb.bodyType = RigidbodyType2D.Kinematic;
        _rectTransform.anchoredPosition = rectTransform.anchoredPosition;
        _rectTransform.position = new Vector3(_rectTransform.position.x, _rectTransform.position.y, rectTransform.position.z);
    }

    private void SetGravity(float value)
    {
        _rb.gravityScale = value;
        _rb.velocity = Vector3.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _parentCanvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DragNDropManager.Instance.OnSelectItem?.Invoke();

        if (_currentSlot)
        {
            _currentSlot.OnSelect();
            _currentSlot = null;
        }

        SetGravity(0f);
        _rb.bodyType = RigidbodyType2D.Dynamic;
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragNDropManager.Instance.OnDropItem?.Invoke();

        SetGravity(_gravityScale);
        _canvasGroup.blocksRaycasts = true;
    }

    private void CanInteract()
    {
        _image.raycastTarget = true;
    }

    private void CantInteract()
    {
        _image.raycastTarget = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

}
