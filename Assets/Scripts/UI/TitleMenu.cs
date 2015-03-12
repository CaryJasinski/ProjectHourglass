using UnityEngine;
using System.Collections;

public class TitleMenu : MonoBehaviour {

	public Canvas titleMenuCanvas;

	void Start () 
	{
	}

	public void EnableOverlay(bool enabled)
	{
		titleMenuCanvas.enabled = enabled;
	}

	public void PlayButton()
	{
		UIManager.manager.GetComponent<UIManager>().currentUIState = UIManager.UIState.PlayerHUD;
		Application.LoadLevel("Prototype");
	}
}
