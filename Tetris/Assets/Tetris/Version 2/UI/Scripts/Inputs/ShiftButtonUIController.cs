using UnityEngine;
using UnityEngine.EventSystems;

public class ShiftButtonUIController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        EventManager.CallOnInputButton(Direction.ShiftDown);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        EventManager.CallOnInputButton(Direction.Down);
    }
}
