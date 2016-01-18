
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class NextScene : MonoBehaviour {

  public void LoadNextScene (string sceneName) {
      Application.LoadLevel(sceneName);
  }
  
	public void LoadNextScene_fade(string sceneName){
		StartCoroutine(fade_scene(sceneName) );
	}

	public IEnumerator fade_scene(string sceneName){
		float fadeTime = GameObject.FindObjectOfType<Fading>().BeginFade(1);
		yield return new WaitForSeconds(fadeTime);
		Application.LoadLevel(sceneName);

	}

	public void loadHeroSelectScene(){
		Application.LoadLevel ("heroSelect");
		PlayerPrefs.SetString ("levelToLoad", EventSystem.current.currentSelectedGameObject.name);
				
	}

}
