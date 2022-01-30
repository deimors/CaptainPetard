using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenPresenter : MonoBehaviour
{
	public string GameSceneName = "DesertLevel Andrew";

	void Update()
	{
		if (Input.anyKeyDown)
		{
			SceneManager.LoadScene(GameSceneName, LoadSceneMode.Single);
		}
	}
}
