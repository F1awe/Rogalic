using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArrowBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool leftArrow;
    [SerializeField] private ControllInterface interfaceController;
    [SerializeField] private Sprite enabled;
    [SerializeField] private Sprite pressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (leftArrow)
        {
            interfaceController.MoveLeft();
        }
        else
        {
            interfaceController.MoveRight();

        }

        GetComponent<Image>().sprite = pressed;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        interfaceController.StopMove();

        GetComponent<Image>().sprite = enabled;
    }
}
