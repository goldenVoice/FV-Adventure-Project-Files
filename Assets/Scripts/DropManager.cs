using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropManager : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {

	public void OnDrop(PointerEventData eventData){
		Debug.Log("onDrop");
	}

	public void OnPointerEnter(PointerEventData eventData){
		Debug.Log ("OnPointerEnter");
	}

	public void OnPointerExit(PointerEventData eventData){
		Debug.Log ("OnPointerExit");
	}
}
