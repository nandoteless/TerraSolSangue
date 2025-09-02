using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private RectTransform joystickBackground;
    private RectTransform joystickHandle;
    private Vector2 inputVector;

    void Start()
    {
        
        joystickBackground = GetComponent<RectTransform>();
        joystickHandle = transform.Find("joystickHandle").GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBackground,
            eventData.position,
            eventData.pressEventCamera,
            out pos
        );

        pos.x = (pos.x / joystickBackground.sizeDelta.x) * 2;
        pos.y = (pos.y / joystickBackground.sizeDelta.y) * 2;

        inputVector = new Vector2(pos.x, pos.y);
        inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

        joystickHandle.anchoredPosition = new Vector2(
            inputVector.x * (joystickBackground.sizeDelta.x / 3),
            inputVector.y * (joystickBackground.sizeDelta.y / 3)
        );
    }

    public void OnPointerDown(PointerEventData eventData) => OnDrag(eventData);

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
    }

    public float Horizontal() => inputVector.x;
    public float Vertical() => inputVector.y;
    public Vector2 Direction() => inputVector;
}
