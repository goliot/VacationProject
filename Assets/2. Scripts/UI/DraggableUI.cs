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
        canvas = GetComponentInParent<Canvas>(); // 부모 Canvas를 찾습니다
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 드래그 시작 시
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = false; // 다른 UI와의 충돌 방지
        }

        // 현재 마우스 위치와 UI의 피벗 간의 오프셋을 계산합니다
        Vector2 localPointerPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent.GetComponent<RectTransform>(),
            eventData.position,
            canvas.worldCamera,
            out localPointerPos
        );
        dragOffset = rectTransform.anchoredPosition - localPointerPos;

        rectTransform.SetAsLastSibling(); // 가장 위로 이동
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 드래그 중
        if (canvas != null && rectTransform != null)
        {
            Vector2 localPointerPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform.parent.GetComponent<RectTransform>(),
                eventData.position,
                canvas.worldCamera,
                out localPointerPos
            );
            rectTransform.anchoredPosition = localPointerPos + dragOffset; // 마우스 위치와 UI의 피벗을 맞춤
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 드래그 종료 시
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = true; // 충돌 방지 해제
        }
    }
}
