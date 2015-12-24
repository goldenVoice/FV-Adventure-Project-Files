using UnityEngine;
using System.Collections;

public class testCoRoutine : MonoBehaviour {

	// Use this for initialization
	void Start () {

      // ganto tumawag ng coRoutine, basta ka-kailanganin mo sya pag nag access
      // ng pause, kase para makapag pause kailangan ng yield na method.
	   StartCoroutine(WaitAndPrint());
     StartCoroutine(Example());
     // pwede mo to lagay kahet saan, for example,
     // if(pinindot_yung_button){
     //    StartCoroutine(IEnumerator functionName);
     // }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
  // naguguluhan paren kung pano gagana
  IEnumerator WaitAndPrint() {
        yield return new WaitForSeconds(5);
        print("WaitAndPrint " + Time.time);
    }
    IEnumerator Example() {
        print("Starting " + Time.time);
        yield return WaitAndPrint();
        print("Done " + Time.time);
    }
}
