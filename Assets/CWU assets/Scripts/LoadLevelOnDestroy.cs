using UnityEngine;
using System.Collections;

public class LoadLevelOnDestroy : MonoBehaviour {
	public string levelName = "";

	void OnDestroy()
	{
		Application.LoadLevel(levelName);
	}
}
