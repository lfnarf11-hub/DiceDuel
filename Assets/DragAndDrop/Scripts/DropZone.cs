using System;
using UnityEngine;

public class DropZone : MonoBehaviour
{
  [SerializeField] private RectTransform rectTransform;
  private DragAndDropObject item;
  [SerializeField]
  private LayerMask allowedLayers;
  private void Awake()
  {
    
    DragAndDropObject.OnDraggableChanged += OnDragStateUpdate;
    OnDragStateUpdate(null);
  }

  private void OnDestroy()
  {
    DragAndDropObject.OnDraggableChanged -= OnDragStateUpdate;

  }
  private void Update()
  {
    if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, item.transform.position))
    {
      item.SetTarget(this);
    }
  }

  public bool CanAcceptItem(DragAndDropObject item)
  {
    return item && (allowedLayers & (1 << item.gameObject.layer)) != 0;
  }

  public Vector2 GetNearestLocation(Vector2 position)
  {
    Vector2 origin =  rectTransform.position;
    return new Vector2(
      Mathf.Clamp(position.x, rectTransform.rect.min.x + origin.x, rectTransform.rect.max.x + origin.x),
      Mathf.Clamp(position.y, rectTransform.rect.min.y + origin.y, rectTransform.rect.max.y + origin.y)
    );
  }

  private void OnDragStateUpdate(DragAndDropObject item)
  {
    this.item = item;
    enabled = CanAcceptItem(item);
  }
}
