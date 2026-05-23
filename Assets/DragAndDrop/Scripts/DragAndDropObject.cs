using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DragAndDropObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool isHovering { get; private set; }
    public bool isBeingDragged { get; private set; }
    Canvas canvas;
    public static event Action<DragAndDropObject> OnDraggableChanged;
    public DropZone currentTarget { get; private set; }
    public DropZone previousTarget { get; private set; }
    public event Action<DropZone> OnDropZoneChanged;
    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isBeingDragged = true;
        transform.SetParent(canvas.transform);
        OnDraggableChanged?.Invoke(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isBeingDragged = false;
        OnDraggableChanged?.Invoke(null);
        if (currentTarget && currentTarget.CanAcceptItem(this))
        {
            transform.SetParent(currentTarget.transform);
            previousTarget = currentTarget;
            currentTarget = null;
            OnDropZoneChanged?.Invoke(currentTarget);
           
        }
        else if(previousTarget)
        {
            transform.SetParent(previousTarget.transform);
            transform.position = previousTarget.GetNearestLocation(transform.position);
        }

    }

    private void Update()
    {
        if (!isBeingDragged) return;
        transform.position = Pointer.current.position.ReadValue();
    }

    public void SetTarget(DropZone dropZone)
    {
        if (dropZone == previousTarget) return;
        previousTarget = dropZone;
    }
}
