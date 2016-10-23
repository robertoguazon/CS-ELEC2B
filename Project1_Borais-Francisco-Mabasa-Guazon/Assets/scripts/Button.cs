using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerEnterHandler {

	public void OnPointerEnter(PointerEventData eventData) {
        Audio.PlayPressButton();
    }

}
