using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;		// dont forget to include this

											// also this 3 always include them, they specify the events for
									  // begin of dragging , while dragging, end of dragging
public class DragManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	GameManagerBehavior gameManager;
	public GameObject heroPrefab;
	public LayerMask potholeLayerMask;
	Vector3 rayStartPosition;
	Vector3 rayEndPosition;

	GameObject myEventSystem;

	public PotholeManager currentPothole;

	public bool drop;

	[HideInInspector]
	public bool dragBegin;
	public bool dragEnd;

	[HideInInspector]
	public GameObject heroPreview;

	GameObject[] potholes_list;

	GameObject list_pothole;

	void Start(){
		dragBegin = false;
		dragEnd = false;

		myEventSystem = GameObject.Find("EventSystem");
		gameManager = (GameManagerBehavior) FindObjectOfType(typeof(GameManagerBehavior));
		// get the list of potholes from the Gameobject list_pothole then get the Potholes_list script, then access the potholesList array variable, which is the list of potholes
		list_pothole = GameObject.Find("list_pothole");
	}

	void Update () {
		if(list_pothole == null){
			list_pothole = GameObject.Find("list_pothole");
			if(list_pothole != null){
				potholes_list = list_pothole.GetComponent<Potholes_list>().potholesList;
			}
		}
		if(gameManager == null){
			gameManager = (GameManagerBehavior) FindObjectOfType(typeof(GameManagerBehavior));
		}
		if(myEventSystem == null){
			myEventSystem = GameObject.Find("EventSystem");
		}
	}

	public void OnBeginDrag(PointerEventData eventData){
		dragBegin = true;
		dragEnd = false;
		gameManager.currentSelectedHero = heroPrefab;
		// loop through all the current potholes in the scene
		foreach(GameObject pothole in potholes_list){
			// make them the 'listener' of this script. Para alam ng pothole kung anong hero ang kasalukuyang dina drag ng user
			pothole.GetComponent<PotholeManager>().dragManager = this;
		Debug.Log("hey drag begun");

		}
//		Debug.Log(Input.mousePosition);
//		Debug.Log ("Event data: " + eventData.position);
//		Debug.Log (Camera.main.ScreenToViewportPoint(Input.mousePosition));
//		Debug.Log (Camera.main.WorldToScreenPoint(Input.mousePosition));
//		Debug.Log (Camera.main.ScreenToWorldPoint(Input.mousePosition));
//		Debug.Log (Camera.main.ViewportToScreenPoint(Input.mousePosition));
//		Debug.Log(this.transform.GetComponentInParent<RectTransform>().position);

		// show the hero preview, by getting the position of the circle.
		heroPreview = (GameObject) Instantiate(heroPrefab, this.transform.GetComponentInParent<RectTransform>().position, heroPrefab.transform.rotation);
		heroPreview.transform.GetChild(1).GetComponent<Animator>().SetBool("preview", true);
		heroPreview.transform.GetChild (2).renderer.enabled = true;
		heroPreview.transform.GetChild(1).renderer.sortingLayerName = "hero_preview";
//		Debug.Log (heroPreview.transform.GetChild (2).gameObject.name);
//		Debug.Log(Input.mousePosition);
//		Debug.Log("Value of RectTransform: " + this.transform.GetComponentInParent<RectTransform>().position);

	}

	public void OnDrag(PointerEventData eventData){
		// the hero preview will follow the user's mouse position using ScreenToWorldPoint, and setting z axis = 10f so it will be on top
		heroPreview.transform.position = (Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f) ) );;
//		Debug.Log (Camera.main.ScreenToViewportPoint(Input.mousePosition));
//		this.transform.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData){
		dragBegin = false;
		dragEnd = true;
		Destroy(heroPreview);
		if(currentPothole != null){
			currentPothole.drop = true;
			
		}
		foreach(GameObject pothole in potholes_list){
			// when the drag is over set all the potholes that will 'listen' to dragManager to null, kase nga tapos na yung drag di nila kailangan mag abang ng dragManager
			pothole.GetComponent<PotholeManager>().dragManager = null;
		}
		// if the drag has ended and the user is not on any pothole, set the currentSelected hero into null. WALANG selected kase tapos na yung drag, di na naka select yung hero
		if(currentPothole == null){
			gameManager.currentSelectedHero = null;
		}
		// deselect the selected hero circle
		myEventSystem .GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
	}

	public void ActivateScript(){
		this.enabled = true;
	}
}
