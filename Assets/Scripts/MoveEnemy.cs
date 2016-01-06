using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveEnemy : MonoBehaviour {

  [HideInInspector]
  public GameObject[] waypoints;
  private int currentWaypoint = 0;
  private float lastWaypointSwitchTime;
  public  float speed = 1.0f;

//	public float currentTimeOnPath;

	// Use this for initialization
	void Start () {
	   lastWaypointSwitchTime = Time.time;
     // initializes lastWayPointSwitchTime to the current time.
	}
	
	// Update is called once per frame
	void Update () {
		MovingTheEnemey();
	}

  private void SwitchIntoMoveDirection(){


		if(waypoints[currentWaypoint + 1].GetComponent<Text>().text == "up"){
			// get the child of ant object which is the ant sprite, then set the animation
			gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("up");
		}
		else if(waypoints[currentWaypoint + 1].GetComponent<Text>().text == "down"){

			gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("down");
		}
		else if(waypoints[currentWaypoint + 1].GetComponent<Text>().text == "right"){
			// unity bug di nag re reset yung ibang trigger so i tried using ResetTrigger , yung i re reset ay ang animation before the animation you want to trigger
			gameObject.transform.GetChild(0).GetComponent<Animator>().ResetTrigger("down");
			gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("right");
		}
		else if(waypoints[currentWaypoint + 1].GetComponent<Text>().text == "left"){
			gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("left");
		}
//    Vector3 newStartPosition = waypoints[currentWaypoint].transform.position;
    Vector3 newEndPosition = waypoints[currentWaypoint + 1].transform.position;
//    Vector3 newDirection = (newEndPosition - newStartPosition);
//
//    float x = newDirection.x;
//    float y = newDirection.y;
//    // this computation below, determines the angle which newDirection points.
//    // kung curious ka pa malaman tignan mo nlng yung tutorial nito.
//    float rotationAngle = Mathf.Atan2 (y, x) * 180 / Mathf.PI;
//
//    GameObject sprite = (GameObject) gameObject.transform.FindChild("Sprite").gameObject;
//    // here you rotate the child instead the parent, so the healthbar remains horizontal
//    sprite.transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);

  }
  
  // this code calculates the length of road not yet traveled by the enemy.
  public float distanceToGoal(){
    float distance = 0;
    distance += Vector3.Distance(gameObject.transform.position, waypoints[currentWaypoint + 1].transform.position);
    for(int i = currentWaypoint + 1; i < waypoints.Length - 1; i++){
      Vector3 startPosition = waypoints[i].transform.position;
      Vector3 endPosition = waypoints[i + 1].transform.position;
      distance += Vector3.Distance(startPosition, endPosition);
    }
    return distance;
  }

	public void MovingTheEnemey(){
		// you retrieve the start and end position of the current path segment
		Vector3 startPosition = waypoints[currentWaypoint].transform.position;
		Vector3 endPosition = waypoints[currentWaypoint + 1].transform.position;
		
		float pathLength = Vector3.Distance(startPosition, endPosition);
		// calculate the whole distance w/ the formula time = distance / speed, then determine the current time on the path.
		float totalTimeForPath = pathLength / speed;
		float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
		// using vector3.Lerp, you interpolate the current position of the enemy between the segment's start and end positions.
		gameObject.transform.position = Vector3.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath);     
		
		// check if the enemy has reached the endPosition, if yes, handle the following possible scenarios.
		if(gameObject.transform.position.Equals(endPosition)) {
			// if the enemy is not yet at the last wayPoint, increase its currentWaypoint.
			if(currentWaypoint < waypoints.Length - 2){
				currentWaypoint++;
				// update lastWayPointSwitchTime, gagamitin mo tong variable na to para dun sa currentTimeOnPath na gagamitin para sa Vector3.Lerp. BASTA angulo.
				lastWaypointSwitchTime = Time.time;
				
				//				print ("keme");
				SwitchIntoMoveDirection();
			}
			// else, the enemy had reached the last waypoint, so destroy it.
			else {
				Destroy(gameObject);
				if(PlayerPrefs.GetInt("vibr") == 1){
					Handheld.Vibrate();
				}
				
				// reference to GameManagerBehavior script to access method deductHealth
				GameManagerBehavior gameManager = (GameManagerBehavior)FindObjectOfType(typeof(GameManagerBehavior));
				if(gameManager.health > 0){   // if health is not yet 0
					gameManager.deductHealth();
					//  AudioSource audioSource = gameObject.GetComponent<AudioSource>();
					//  AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
				}
				else{   // talo yung player
					gameManager.gameOver = true;
					gameManager.didPlayerWin(false);  // di nanalo yung player
					
				}
			}
		}

	}

}
