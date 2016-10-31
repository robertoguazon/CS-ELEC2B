using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GameButton : MonoBehaviour, IPointerEnterHandler {

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Button: game");
        Audio.PlayPressGameButton();
    }
}
