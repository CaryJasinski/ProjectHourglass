using UnityEngine;
using System.Collections;

public class PlayerHUD : MonoBehaviour {
	
	public Canvas playerHUDCanvas;
	
	void Start () 
	{
	}
	
	public void EnableOverlay(bool enabled)
	{
		playerHUDCanvas.enabled = enabled;
	}
}
