using UnityEngine;
using System.Collections;

public class LaserBeam : MonoBehaviour {

	public LineRenderer laser;

//	public Transform laserHit;

	public Transform point2;		// the other end

	float timeCounter;
	float DPS_countingInterval;
	public float DPSinterval;		// ilang seconds bago mag damage ule? Lets say, every 0.7 secs dumadamage
	public float maxSeconds;		// maximum duration of the tornado

	public GameObject target;

	float startTime;

	public LaserBehavior laserBehavior;

	// Use this for initialization
	void Start () {
		laser.SetPosition (1, new Vector3(0f, 0f, 0f) );		// reset position to the object position
		laser.enabled = true;
		laser.useWorldSpace = true;
		timeCounter = Time.time;
		startTime = Time.time;
		Debug.Log ("tis is calleds");
	//	laserBehavior = transform.parent.GetComponent<LaserBehavior> ();
	}
	
	// Update is called once per frame
	void Update () {
	//	if (Input.GetMouseButtonDown(0)) {
		//laser.enabled = true;
		float timeInterval = Time.time - startTime;		
		DPS_countingInterval = Time.time - timeCounter;

		//Debug.Log (timeInterval);
		if(timeInterval >= maxSeconds){
			this.enabled = false;
			laser.enabled = false;

		}
		
		if (target == null) {
			this.enabled = false;
			laser.enabled = false;
		} else {
			point2.position = target.transform.parent.position;
			laser.SetPosition (0, transform.position);
			laser.SetPosition (1, point2.position);
		}

		if(DPS_countingInterval >= DPSinterval){		// maxSeconds = 2; dps interval = 0.5; pero ang dame lang ng bes na mababawasan sya ay 3 times. hindi 4 times (kase 2 / 0.5 = 4) there something about this computation na ganon yung nangyayare. and im too lazy now to figure out why
			if(target != null){
				laserBehavior.SendMessage("ApplyDamage", target);
				timeCounter = Time.time;
			}
		}
		//	Debug.DrawLine(transform.position, hit.point);
		//	laserHit.position = hit.point;
//			Debug.Log("hello its mee");
//			Ray2D ray = new Ray2D(transform.position, transform.right);
//			RaycastHit2D hit;
//
//			laser.SetPosition (0, ray.origin);
//
//			hit = Physics2D.Raycast(ray.origin, Vector2.right, 100f);
//
//			if(hit.collider){
//				laser.SetPosition(1, hit.point);
//			}
//			else{
//				laser.SetPosition(1, ray.GetPoint(100f) );
//				//yield return null;
//			}
//			laser.enabled = false;
//		}
	}

	public void restartTime(){
		timeCounter = Time.time;
		startTime = Time.time;
		laser.SetPosition (0, new Vector3(0f, 0f, 0f) );		// reset position to the object position
		laser.SetPosition (1, new Vector3(0f, 0f, 0f) );		// reset position to the object position
		laser.enabled = true;
	}
}
