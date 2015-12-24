using UnityEngine;
using System.Collections;

public class HeroData : MonoBehaviour {

  	public int cost;
  	public GameObject bullet;
  	public float fireRate;
	public float refundPercent;
	public int waterRefund;
	public ElementManager.Element heroElement;
  	
	double result;
	// Use this for initialization
	void Start () {
		refundPercent = refundPercent / 100;
//		print (refundPercent);
		result = cost * refundPercent;		// refund is 40% of the hero's cost, as defined in the inspector
		waterRefund = Mathf.RoundToInt((float)result);	// cast the result into a float then round it to an int
														
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
