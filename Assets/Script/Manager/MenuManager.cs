using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {
	
	private GameManager _GameManager;
	
	private int _buttonPosX  = (int)(Screen.width  * 0.165f);
	private int _buttonPosY  = (int)(Screen.height * 0.10f);
	private int _buttonSizeX = (int)(Screen.width  * 0.66f);
	private int _buttonSizeY = (int)(Screen.height * 0.10f);
	private int _offsetY     = (int)(Screen.height * 0.05f);
	
	private string _buttonNewGameString = "New Game (Erase Save)";
	private int   _confirmTry          = 0;
	
	// Use this for initialization
	void Start () {
		_GameManager = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameManager>();
		_GameManager.ChangeState("Menu");
	}
	
	void OnGUI()
	{
		float _boxPosX;
		float _boxPosY;
		if(GUI.Button(new Rect(_buttonPosX, _buttonPosY, _buttonSizeX, _buttonSizeY), _buttonNewGameString))
		{
			if(PlayerPrefs.GetInt ("IsSaveExist") == 1)
			{
				if(_confirmTry == 0)
				{
					_confirmTry++;
					_buttonNewGameString = "Are you sure you want to erase your save?";
				}
				else if(_confirmTry == 1)
				{
					_confirmTry++;
					_buttonNewGameString = "LAST CHANCE UNTIL SAVE RESET, YES?";
				}
				else
				{
					PlayerPrefs.DeleteAll();
					NewGame();
				}
			}
			else
			{
				
				NewGame();
			}	
		}
		if(PlayerPrefs.GetInt ("IsSaveExist") == 0) //If save exist
		{
			GUI.enabled = false;
		}
		
		if(GUI.Button(new Rect(_buttonPosX, _buttonPosY + _buttonSizeY + _offsetY, _buttonSizeX, _buttonSizeY), "Load Game"))
		{
			StartGame();
		}
		GUI.enabled = true;
		
		
		// Saved stats Display
		_boxPosX = _buttonPosX;
		_boxPosY = _buttonPosY + 2 * _buttonSizeY + 2 *_offsetY;
		GUI.Box(new Rect(_buttonPosX, _boxPosY, _buttonSizeX, _buttonSizeY*3),"");
		
		if(PlayerPrefs.GetInt ("IsSaveExist") == 0) //If save exist
		{
			GUI.Label(new Rect(_buttonPosX, _boxPosY + 0.5f*_offsetY, _buttonSizeX, 25.0f),"Save not found. Start a new game.");
		}								   
		else							
		{								   
			GUI.Label(new Rect(_buttonPosX, _boxPosY           , _buttonSizeX, 25.0f),"==> SAVE FOUND <== ");
			GUI.Label(new Rect(_buttonPosX, _boxPosY + 1f*25.0f, _buttonSizeX, 25.0f),"Total Skill Level : " + Character.CalculateSavedSkillLevel());
			GUI.Label(new Rect(_buttonPosX, _boxPosY + 2f*25.0f, _buttonSizeX, 25.0f),"Dungeon level : " + PlayerPrefs.GetInt ("MaxDungeonLevel"));
			GUI.Label(new Rect(_buttonPosX, _boxPosY + 3f*25.0f, _buttonSizeX, 25.0f),"Influence : "     + PlayerPrefs.GetInt ("InfluencePoints"));
		}
	}
	
	void NewGame()
	{
		PlayerPrefs.DeleteAll();
		ItemInventory.EquipWeapon (Inventory.WeaponList[(int)WeaponName.RockSword]);
		ItemInventory.AddItem(Inventory.WeaponList[(int)WeaponName.Hammer]);
		if(Input.GetKey(KeyCode.LeftShift))
		{
			_GameManager.MaxDungeonLevel = 20;
			Character.InfluencePoints = 500;
			Inventory.RessourceList[(int)RessourceName.Wood].CurValue = 1000;
			Inventory.RessourceList[(int)RessourceName.Coin].CurValue = 1000;
		}
		StartGame();
		
	}
	void StartGame()
	{
		Application.LoadLevel("Camp");
		GameObject.FindGameObjectWithTag("PlayerMaster").GetComponent<PlayerHUD>().enabled = true;
		GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameManager>().IniGame ();
		GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(-20.0f,0.75f,-10.0f);
		//GameObject.FindGameObjectWithTag("Player").transform.rotation.SetLookRotation(Vector3.back);
	}
}
