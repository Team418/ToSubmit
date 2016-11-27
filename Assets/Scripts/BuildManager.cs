using UnityEngine;

public class BuildManager : MonoBehaviour 
{
	public static BuildManager Instance;

	void Awake()
	{
		if (Instance != null) 
		{
			Debug.LogError ("More than 1 BM!");
			return;
		}
		Instance = this;
	}

	private GameObject turretToBuild;
	public GameObject getTurretToBuild()
	{
		return turretToBuild;
	}
		
	public GameObject turretPrefab;

	void Start()
	{
		turretToBuild = turretPrefab;
	}
}
