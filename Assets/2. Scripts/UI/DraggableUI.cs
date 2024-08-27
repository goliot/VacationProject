using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private Vector2 dragOffset;

    protected virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>(); // �θ� Canvas�� ã���ϴ�
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // �巡�� ���� ��
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = false; // �ٸ� UI���� �浹 ����
        }

        // ���� ���콺 ��ġ�� UI�� �ǹ� ���� �������� ����մϴ�
        Vector2 localPointerPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent.GetComponent<RectTransform>(),
            eventData.position,
            canvas.worldCamera,
            out localPointerPos
        );
        dragOffset = rectTransform.anchoredPosition - localPointerPos;

        rectTransform.SetAsLastSibling(); // ���� ���� �̵�
    }

    public void OnDrag(PointerEventData eventData)
    {
        // �巡�� ��
        if (canvas != null && rectTransform != null)
        {
            Vector2 localPointerPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform.parent.GetComponent<RectTransform>(),
                eventData.position,
                canvas.worldCamera,
                out localPointerPos
            );
            rectTransform.anchoredPosition = localPointerPos + dragOffset; // ���콺 ��ġ�� UI�� �ǹ��� ����
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // �巡�� ���� ��
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = true; // �浹 ���� ����
        }
    }
}
