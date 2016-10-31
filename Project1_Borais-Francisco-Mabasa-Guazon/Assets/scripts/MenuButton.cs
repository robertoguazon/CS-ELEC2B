using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler {

	public void OnPointerEnter(PointerEventData eventData) {
        Debug.Log("Button: menu");
        Audio.PlayPressMenuButton();
    }

}
