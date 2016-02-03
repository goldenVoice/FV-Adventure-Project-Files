using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using UnityEngine.UI;

public class WaterCost_indicator : MonoBehaviour {

	public TextAsset _xml;

	// Use this for initialization
	void Start () {
		_xml = Resources.Load<TextAsset> ("Hero_info") as TextAsset; // hanapin yung .xml mula sa path tapos i load
		XmlDocument xmlDoc = new XmlDocument (); 
		xmlDoc.LoadXml(_xml.text);
		string heroWater = xmlDoc.SelectSingleNode("//Hero[@name='" + gameObject.name + "']/Water").InnerText;
		//	get the water cost of by this format ex: "//Hero[@name='circle_carrot']/Water)" then you get the water using the innerText method
		//Debug.Log (gameObject.name + ": " + heroWater);

		// you then get the waterCostText gameObject, then set the hero's water cost there
		gameObject.transform.GetChild (0).GetComponent<Text> ().text = heroWater;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
