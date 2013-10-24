using UnityEngine;
using System.Collections;
using System; // For Enum class;
static class Character {
	
	static private TextureManager _TextureManager =  GameObject.FindGameObjectWithTag("GameMaster").GetComponent<TextureManager>();
	
	private static int _baseHP     = 80;
	private static int _maxHP      = _baseHP;
	private static int _curHP      = _maxHP;
	private static int _baseMP     = 100;
	private static int _maxMP      = _baseMP;
	private static int _curMP      = _maxMP;
	private static int _baseDamage = 8;
	private static int _curDamage  = _baseDamage;
	private static float _elapsedTimeManaRegen;
	private static float _regenTimer = 0.25f;
	private static int _regenValue = 2;
	private static int _influencePoints = 0;
	
	private static 	float _hpExpPerLevel = 1.5f;
	private static 	int _damagePerLevel = 2;
	
	private static 	int _iceMagicModPerLevel = 2;
	private static 	int _fireMagicModePerLevel = 2;
	
	private static GameManager _GameManager = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameManager>();
	public static Stat[] StatList;
	public static Skill[] SkillList;
	public static Task[] TaskList;
	public static Spell[] SpellList;
	static public int CurHP
	{
		get {return _curHP; }
		set {_curHP = value; }
	}
	
	static public int MaxHP
	{
		get {return _maxHP; }
		set {_maxHP = value; }
	}
	
	static public int CurMP
	{
		get {return _curMP; }
		set {_curMP = value; }
	}
	
	static public int MaxMP
	{
		get {return _maxMP; }
		set {_maxMP = value; }
	}
	
	static public int CurDamage
	{
		get {return _curDamage; }
		set {_curDamage = value; }
	}
	
	static public int InfluencePoints
	{
		get {return _influencePoints; }
		set {_influencePoints = value; }
	}
	
	//public static Task ActiveTask = TaskList[(int)TaskName.Spartan0];
	public static Task ActiveTask;
	
	public static void IniCharacter()
	{
		StatList  = new Stat [Enum.GetValues(typeof(StatName) ).Length];	
		SkillList = new Skill[Enum.GetValues(typeof(SkillName)).Length];
		TaskList  = new Task [Enum.GetValues(typeof(TaskName) ).Length];
		SpellList = new Spell[Enum.GetValues(typeof(SpellName)).Length];
		
		IniStatList();
		IniSkillList();
		IniTaskList();	
		IniSpellList();
	}
	
	private static void IniStatList()
	{
		for(int i = 0; i < StatList.Length; i++)
		{
			StatList[i] = new Stat();
			StatList[i].Name = ((StatName)i).ToString();
			if(StatList[i].Name == "Strength")
			{
				StatList[i].Level  = 0;
				StatList[i].CurExp = 0;
			}
			else if(StatList[i].Name == "Dexterity")
			{
				StatList[i].Level  = 1;
				StatList[i].CurExp = 1;
			}
			else if(StatList[i].Name == "Agility")
			{
				StatList[i].Level  = 1;
				StatList[i].CurExp = 5;
			}
			else if(StatList[i].Name == "Intelligence")
			{
				StatList[i].Level  = 5;
				StatList[i].CurExp = 1;
			}			
			
		}
	}
	
	private static void IniSkillList()
	{
		for(int i = 0; i < SkillList.Length; i++)
		{
			SkillList[i] = new Skill();
			SkillList[i].Name = ((SkillName)i).ToString();
			if(SkillList[i].Name == "Crafter")
			{
				SkillList[i].Unlocked = false;
				//SkillList[i].Level  = 0;
				SkillList[i].CurExp = 0;
			}
			else if(SkillList[i].Name == "Lumberjack")
			{
				SkillList[i].Unlocked = true;
				//SkillList[i].Level  = 0;
				SkillList[i].CurExp = 0;
			}
			else if(SkillList[i].Name == "Miner")
			{
				SkillList[i].Unlocked = true;
				//SkillList[i].Level  = 0;
				SkillList[i].CurExp = 0;
			}
			else if(SkillList[i].Name == "Fighter")
			{
				SkillList[i].Unlocked = true;
				//SkillList[i].Level  = 0;
				SkillList[i].CurExp = 0;
			}
			else if(SkillList[i].Name == "Constitution")
			{
				SkillList[i].Unlocked = true;
				//SkillList[i].Level  = 0;
				SkillList[i].CurExp = 0;
			}
			else if(SkillList[i].Name == "IceMage")
			{
				SkillList[i].Name     = "Ice Mage";
				SkillList[i].Unlocked = true;
				//SkillList[i].Level  = 0;
				SkillList[i].CurExp = 0;
			}
			else if(SkillList[i].Name == "FireMage")
			{
				SkillList[i].Name     = "Fire Mage";
				SkillList[i].Unlocked = true;
				//SkillList[i].Level  = 0;
				SkillList[i].CurExp = 0;
			}
			
		}
	}
	
	private static void IniTaskList()
	{
		string _taskName;
		for(int i = 0; i < TaskList.Length; i++)
		{
			_taskName = ((TaskName)i).ToString();
			
			TaskList[i] = new Task();
			
			if(_taskName == "Spartan0")
			{
				TaskList[i].Name = _taskName;	
				TaskList[i].Definition = "Talk to Spartan to obtain your first quest.";
				TaskList[i].Requirement = "None";				
				TaskList[i].Reward = "[ITEM]1*Hammer";
				TaskList[i].IsUnlocked = true;
				TaskList[i].IsFinished = false;
			}
			else if(_taskName == "Spartan1")
			{
				TaskList[i].Name = _taskName;
				TaskList[i].Definition = "Repair the wooden cart behind Spartan";
				TaskList[i].Requirement = "None"; //TODO Add Misc requirements
				TaskList[i].Reward = "[DISS]Spartan";
				TaskList[i].IsUnlocked = false;
				TaskList[i].IsFinished = false;
			}
			else if(_taskName == "Spartan2")
			{
				TaskList[i].Name = _taskName;
				TaskList[i].Definition = "Build a crafting table";
				TaskList[i].Requirement = "[BUIL]1*CraftingTable";
				TaskList[i].Reward = "[DISS]Spartan";
				TaskList[i].IsUnlocked = false;
				TaskList[i].IsFinished = false;
			}
			else if(_taskName == "Spartan3")
			{
				TaskList[i].Name = _taskName;
				TaskList[i].Definition = "Craft an Axe and Pickaxe and gather 10 Rock and 10 Wood";
				TaskList[i].Requirement = "[CRAF]1*RockAxe+1*RockPickaxe,[RESS]10*Rock+10*Wood";
				TaskList[i].Reward = "[DISS]Spartan,[RESS]15*Coin";
				TaskList[i].IsUnlocked = false;
				TaskList[i].IsFinished = false;
			}
			else if(_taskName == "Spartan4")
			{
				TaskList[i].Name = _taskName;
				TaskList[i].Definition = "Buy a Sword";
				TaskList[i].Requirement = "[RESC]15*Coin";
				TaskList[i].Reward = "[ITEM]1*RockSword";
				TaskList[i].IsUnlocked = false;
				TaskList[i].IsFinished = false;
			}
			
		}
	}
	
	private static void IniSpellList()
	{
		for(int i = 0; i < SpellList.Length; i++)
		{
			SpellList[i] = new Spell();
			SpellList[i].Name = ((SpellName)i).ToString();
			if(SpellList[i].Name == "IceBolt")
			{
				SpellList[i].Name = "IceBolt";
				SpellList[i].Category  = "Ice";
				SpellList[i].IsUnlocked = true;
				SpellList[i].SpellIcon  = _TextureManager.Icon_Spell_IceBolt;
				
				SpellList[i].DamageBase      = 3;
				SpellList[i].DamagePerLevel  = 2.0f;
				SpellList[i].CdBase          = 0.45f;
				SpellList[i].CdPerLevel      = -0.02f;
				//SpellList[i].RangeBase       = 5.0f;
				//SpellList[i].RangePerLevel   = 0.25f;
				SpellList[i].ManaBase        = 15;
				SpellList[i].ManaPerLevel    = -0.5f;
				
			}
			else if(SpellList[i].Name == "FireBat")
			{
				SpellList[i].Name = "FireBat";
				SpellList[i].Category  = "Fire";
				SpellList[i].IsUnlocked = true;
				SpellList[i].SpellIcon  = _TextureManager.Icon_Spell_FireBat;
				
				SpellList[i].DamageBase      = 6;
				SpellList[i].DamagePerLevel  = 1.6f;
				SpellList[i].CdBase          = 0.55f;
				SpellList[i].CdPerLevel      = -0.025f;
				//SpellList[i].RangeBase       = 5.0f;
				//SpellList[i].RangePerLevel   = 0.25f;
				SpellList[i].ManaBase        = 30;
				SpellList[i].ManaPerLevel    = -1f;
			}
			
			
		}
	}
	
	public static void ProcAction(string _actionProced)
	{
		switch(_actionProced)
		{
			case "Woodcutting": 
				GiveExpToSkill(SkillList[(int)SkillName.Lumberjack],25.0f);
				break;
			case "Mining":
				GiveExpToSkill(SkillList[(int)SkillName.Miner],25.0f);
				break;
			case "Default": 
				Debug.LogWarning ("No Action in GameManager");
				break;
		}
	}
	
	public static void GiveExpToSkill(Skill _SkillToInc, float _expToGive)
	{
		_SkillToInc.CurExp = _SkillToInc.CurExp + _expToGive;	
		if(_SkillToInc.CurExp >= 100.0f)
		{
			 LevelupSkill(_SkillToInc);
		}
	}
	
	public static void LevelupSkill(Skill _SkillToLevel)
	{
		
		int _valuePreLevel = 0;
		int _valuPostLevel = 0;
		
		string _UpdatedStat = "StatNotFound";
		
		_SkillToLevel.CurExp = 0;	
		_SkillToLevel.Level++;
		if(_SkillToLevel.Name == "Fighter")
		{
			_valuePreLevel = Character.CurDamage;
			UpdateStats();
			_valuPostLevel = Character.CurDamage;
			_UpdatedStat = "Damage";
			_GameManager.AddChatLogHUD ("[SKIL] " + _SkillToLevel.Name + " is now level " + _SkillToLevel.Level + " ! " + _UpdatedStat + " " + _valuePreLevel + " -> " + _valuPostLevel);	
		}
		else if(_SkillToLevel.Name == "Constitution")
		{
			_valuePreLevel = Character._maxHP;
			UpdateStats();
			_valuPostLevel = Character._maxHP;	
			_UpdatedStat = "HP";
			_GameManager.AddChatLogHUD ("[SKIL] " + _SkillToLevel.Name + " is now level " + _SkillToLevel.Level + " ! " + _UpdatedStat + " " + _valuePreLevel + " -> " + _valuPostLevel);	
		}
		else if(_SkillToLevel.Name == "IceMage")
		{
			MagicBook.UpdateSpellStats();
			_GameManager.AddChatLogHUD ("[SKIL] " + _SkillToLevel.Name + " is now level " + _SkillToLevel.Level + " ! Damage increased" );	
		}
		else if(_SkillToLevel.Name == "FireMage")
		{
			MagicBook.UpdateSpellStats();
			_GameManager.AddChatLogHUD ("[SKIL] " + _SkillToLevel.Name + " is now level " + _SkillToLevel.Level + " ! Damage increased" );	
		}
		
		
	}
	
	public static void UpdateStats()
	{
		
		_maxHP = _baseHP + (int)(3.0f*Mathf.Pow (SkillList[(int)SkillName.Constitution].Level,1.35f));
		_curHP = _maxHP;
		_curDamage = _baseDamage + (int)(Mathf.Pow(SkillList[(int)SkillName.Fighter].Level,1.5f) * _damagePerLevel);
		
	}
	
	public static void UnlockTask(Task _TaskToUnlock)
	{
		_TaskToUnlock.IsUnlocked = true;
		Character.ActiveTask = _TaskToUnlock;
	}
	
	public static void FinishTask(Task _TaskToFinish)
	{
		_TaskToFinish.IsFinished = true;
		if(Character.ActiveTask == _TaskToFinish)
		{
			Character.ActiveTask = null;
		}
		Character.ActiveTask = _TaskToFinish;
	}
	
	public static void SetActiveTask(Task _TaskToActive)
	{
		Character.ActiveTask = _TaskToActive;
	}
	
	public static void LoseHp(int _hpToLose)
	{
		_curHP -= _hpToLose;
		Character.GiveExpToSkill(Character.SkillList[(int)SkillName.Constitution],20.0f/(Mathf.Pow (Character.SkillList[(int)SkillName.Constitution].Level, 2)));
		Debug.Log ("Lose HP");
		if(_curHP <= 0)
		{
			Debug.Log ("Dead");
			_curHP = 0;
			Die ();
		}
	}
	
	public static void RefillHP()
	{
		_curHP = _maxHP;
	}
	
	public static void GiveMp(int _mpToGive)
	{
		_curMP += _mpToGive;
		if(_curMP >= _maxMP)
		{
			_curMP = _maxMP;
		}
	}
	
	public static void LoseMp(int _mpToLose)
	{
		_curMP -= _mpToLose;
		if(_curMP <= 0)
		{
			_curMP = 0;
		}
	}
	
	public static void RefillMP()
	{
		_curMP = _maxMP;
	}
	
	public static void RegenMP()
	{
		_elapsedTimeManaRegen += Time.deltaTime;
		
		if(_elapsedTimeManaRegen >= _regenTimer)
		{
			_elapsedTimeManaRegen -= _regenTimer;
			GiveMp(_regenValue);
		}
	}
	
	public static void Die()
	{
		_GameManager.AddChatLogHUD("[DIED] You died.");
		_GameManager.ChangeDungeonState("Lose");	
	}
	
	public static void GainInfluences(int _pointToAdd)
	{
		_influencePoints += _pointToAdd;
	}
	
	public static int CalculateSkillLevel()
	{
		int _totalSkillLvl = 0;
		for(int i = 0; i <  Character.SkillList.Length; i++)
		{
			if(Character.SkillList[i].Unlocked == true)
			{
				_totalSkillLvl += Character.SkillList[i].Level;
			}
		}	
		return _totalSkillLvl;
	}
	
	public static int CalculateSavedSkillLevel()
	{
		int _totalSkillLvl = 0;
		for(int i = 0; i <  Character.SkillList.Length; i++)
		{
			if(Character.SkillList[i].Unlocked == true)
			{
				_totalSkillLvl += PlayerPrefs.GetInt ("SkillLvl_" + Character.SkillList[i].Name);
			}
		}	
		return _totalSkillLvl;
	}
}