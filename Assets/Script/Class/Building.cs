using UnityEngine;
using System.Collections;

public class Building {
	
	private int     _id;
	private string  _name;
	private string  _type = "NoType(DEBUG)"; // Utility, ,Structural, Decoration
	private bool    _isBuildable = true;
	private bool    _isUnlocked = false;
	private int     _nbrBuilt = 0;
	private string  _recipe;
	private GameObject _BuildingPrefab;
	private Vector3    _positionOffset = new Vector3(0.0f,0.0f,0.0f);
	
	
	public int Id
	{
		get {return _id; }
		set {_id = value; }
	}
	
	public string Name
	{
		get {return _name; }
		set {_name = value; }
	}
	
	public string Type
	{
		get {return _type; }
		set {_type = value; }
	}
	
	public bool IsBuildable
	{
		get {return _isBuildable; }
		set {_isBuildable = value; }
	}
	
	public bool IsUnlocked
	{
		get {return _isUnlocked; }
		set {_isUnlocked = value; }
	}
	
	public string Recipe
	{
		get {return _recipe; }
		set {_recipe = value; }
	}
	
		public int NbrBuilt
	{
		get {return _nbrBuilt; }
		set {_nbrBuilt = value; }
	}
	
	public GameObject BuildingPrefab
	{
		get {return _BuildingPrefab; }
		set {_BuildingPrefab = value; }
	}
	
	public Vector3 PositionOffset
	{
		get {return _positionOffset; }
		set {_positionOffset = value; }
	}
}

// Enumeration of all Compound
public enum BuildingName {
	CraftingTable,
	WoodStorage,
	WoodenBarrel,
	WoodenWall,
	WoodenFence01,
	WoodenFence01Curve,
	WoodenFence02,
	WoodenFence03,
	StoneFence, // Remove StoneFence because it has many GO and need an uptade to the building system(Collision, modification on it from player)
	HighPillar,
	Gate,
	Tent,
	FirePillar,
	LowStatue,
	HighStatue,
	GargoyleStatue
}