using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	//singleton with accessor
	public static GameObject manager;
	public UIManager uiManager;
//	public static UIManager GetInstance()
//	{
//		if (!manager)
//		{
//			manager = GameObject.FindObjectOfType(typeof(UIManager));
//			if (!manager)
//				Debug.LogError("There needs to be one active UIManager script on a GameObject in your scene.");
//		}
//		
//		return manager;
//	}

	//create the enum type 'UIState' and assign it's values. 
	public enum UIState	{ TitleMenu, PlayerHUD, Credits } 

	public UIState currentUIState; // store the current UI state, used to determine current state
	public UIState previousUIState; //store the previous UI state, used for checking state change

	private TitleMenu titleMenu;
	private PlayerHUD playerHUD;

	void Start ()
	{
		manager = this.gameObject;
		uiManager = manager.GetComponent<UIManager>();
		//Initialize the currentUIState variable with the state we want the game to start in.
		currentUIState = UIState.TitleMenu;
		//Initialize the previousUIState with a UIState that is different than the currentUIState
		previousUIState = UIState.Credits;

		//Initialize each menu's script
		titleMenu = GetComponent<TitleMenu>();
		playerHUD = GetComponent<PlayerHUD>();
	}

	void Update()
	{
		//Check for state change while level is not loading
		if(!Application.isLoadingLevel && currentUIState != previousUIState)
			ChangeUIState();
	}

	//GUI is displayed based on the current UIstate
	void ChangeUIState()
	{
		if (currentUIState == UIState.TitleMenu)
		{
			titleMenu.EnableOverlay(true);
			previousUIState = UIState.TitleMenu;
		}
		else
			titleMenu.EnableOverlay(false);

		if (currentUIState == UIState.PlayerHUD)
		{
			playerHUD.EnableOverlay(true);
			previousUIState = UIState.PlayerHUD;
		}
		else
			playerHUD.EnableOverlay(false);


	}
}
