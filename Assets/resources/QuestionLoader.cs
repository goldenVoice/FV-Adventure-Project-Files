//using UnityEngine;
////using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Text;
//using System.Xml;
//using System.IO;
//using UnityEngine.UI;
//
//public class QuestionLoader : MonoBehaviour {
//
//	bool englishAvailable;
//	bool mathAvailable;
//	bool scienceAvailable;
//	bool filipinoAvailable;
//
//
//	public List<DataQuestion> questionData;
//	RandomScript random;	// script na random
//
//	public AskTheQuestion askTheQuestion;
//	public int counter;
//
//	public Canvas questionCanvas;
//	public Canvas congratsCanvas;
//
//	public Button a;
//	public Button b;
//	public Button c;
//	public Button d;
//
//	public Text congratsMessage;
//
//	//public Player player;
//
//
//	void Start () {
//
//		questionData = new List<DataQuestion> ();
//		bool keme;
//		Debug.Log (Path.Combine (Application.dataPath, "Resources/questions.xml") );
//		string path = Path.Combine (Application.dataPath, "Resources/questions.xml");
//
//		TextAsset _xml = Resources.Load<TextAsset> ("questions"); // hanapin yung .xml mula sa path tapos i load
//		XmlDocument xmlDoc = new XmlDocument (); 
//		xmlDoc.LoadXml(_xml.text);
//		XmlNodeList itemsList = xmlDoc.GetElementsByTagName ("Question"); // array of the level nodes.
//
//		foreach (XmlNode item in itemsList) {
//			//  new variable every loop (questionData1), dito pupunta lahat ng para sa element for each loop
//			DataQuestion questionData1 = new DataQuestion (
//			   (item.SelectSingleNode ("questionToAsk").InnerText),	// invoke the constructor using new
//			  (item.SelectSingleNode ("a").InnerText),
//			  (item.SelectSingleNode ("b").InnerText),
//			  (item.SelectSingleNode ("c").InnerText),
//			  (item.SelectSingleNode ("d").InnerText),
//			  (item.SelectSingleNode ("correctAnswer").InnerText),
//			  (item.SelectSingleNode ("category").InnerText)
//			);
//
//			questionData.Add (questionData1);	// after ng initialization ng 
//		}
//
//		// generate question
//		counter = 0;
//		generateQuestion();
//
//	}
//
//	int returnRange(){
//		int index = 0;
//		bool questionFound = false;
//		
//		while(questionFound == false){
//			index = Random.Range(0, questionData.Count);
//			if(questionData[index].available == true){
//					if( categoryAvailable(questionData[index].category) ){		// check if the current category is NOT YET selected
//						questionFound = true;
//						questionData[index].available = false;
//						return index;
//					}
//			}
//		}
//		return -1; // 
//	}	
//	void printTheQuestion(int index){
//		askTheQuestion.theQuestion.text = questionData [index].theQuestion;
//		askTheQuestion.a.text = "A. " + questionData [index].a;
//		askTheQuestion.b.text = "B. " + questionData [index].b;
//		askTheQuestion.c.text = "C. " + questionData [index].c;
//		askTheQuestion.d.text = "D. " + questionData [index].d;
//		askTheQuestion.correctAns = questionData [index].correctAns;
//		
//	}
//
//	public void checkAnswer(Button button){
//		if (button.name == askTheQuestion.correctAns) {
//			//TOdo: add player score
//			button.image.color = Color.green;
//			Time.timeScale = 0f;
//			// show congratulation message
//			congratsMessage.text = "Your Answer is correct!";
//			congratsCanvas.enabled = true;
//			// hide the spells
//		}
//		else {
//			// bawas heart
//		//	player.curHealth -= 1;
//			button.image.color = Color.red;
//			Time.timeScale = 0f;
//			showCorrectAnswer(questionCanvas);
//			// show error message
//			congratsMessage.text = "Your Answer is Wrong!";
//			congratsCanvas.enabled = true;
//			// hide the spells
//		}	
//	}
//
//	public void generateQuestion(){
//		// look for index of list in the
//		Debug.Log ("Counter: " + counter + ", questionData.count: " + questionData.Count);
//		int index = returnRange();		// note kapag -1 may error sa logic ng function na to
//		if (counter < (questionData.Count) - 1) {	// check kung yung number of questions generated, ay di pa lumalampas sa dami ng questions encoded.
//			printTheQuestion (index);				// kailangan talaga may -1 kase yung .count -1 na ang panghuling value na meron ang list mo, pag tinanggal mo mag ka crush ang unity.
//			counter++;
//		}
//		else {
//			// do nothing, just close the question window
//			Debug.Log ("Wala ng question to display");
//			questionCanvas.enabled = false;
//		}
//
//		// set the colors of the button back to white
//		a.image.color = Color.white;
//		b.image.color = Color.white;
//		c.image.color = Color.white;
//		d.image.color = Color.white;
//		// hide the congratulation message
//		congratsCanvas.enabled = false;
//		// show the spells
//	}
//
//	public void showCorrectAnswer(Canvas questionCanvas){
//		string ButtonName = askTheQuestion.correctAns;	// halimbawa letter a yung sagot, ang button name
//		Button correctButton = GameObject.Find (ButtonName).GetComponent<Button>();
////		Transform findButton = questionCanvas.transform.FindChild(ButtonName);			// ng hahanapin mo sa findChild ay 'a' which you stored in ButtonName variable.
////		Button correctButton = (Button) findButton.gameObject.GetComponent<Button>();	// yung findButton, ang nakuha mo lang ay Transform, kaya findButton.gameObject, kinuha mo naman yung gameObject na naka attach tong Transform component, which is yung button na hinahanap naten,
////														// kaya ini-store naten sya as a button,
//		correctButton.image.color = Color.green;
//	}
//
//	bool categoryAvailable(string category){
//		if (category == "English" && englishAvailable == true){
//					Debug.Log("English");
//					englishAvailable = false;		// the category is no longer available so false		
//					return true;		// the 
//		}
//		else if((category == "Filipino") && englishAvailable == true){
//			Debug.Log("filipino");
//					filipinoAvailable = false;
//					return true;
//		}
//		else if((category == "Math") && mathAvailable == true){
//			Debug.Log("math");
//					mathAvailable = false;
//					return true;
//		}
//		else if((category == "Science") && scienceAvailable == true){
//			Debug.Log("science");
//					scienceAvailable = false;
//					return true;
//		}
//		if (	!englishAvailable && !filipinoAvailable && !mathAvailable && !scienceAvailable){		// if all the categories are selected. renew the categoriesAvailable, by making them true
//					englishAvailable = true;
//					mathAvailable = true;
//					filipinoAvailable = true;
//					scienceAvailable = true;
//		}
//
//		Debug.Log("Dapat lalabas sa ikaapat na try");
//		return false;
//
//	}
//}
//	
//	
