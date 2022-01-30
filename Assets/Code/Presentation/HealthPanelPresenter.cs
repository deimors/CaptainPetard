using System.Linq;
using UnityEngine;

public class HealthPanelPresenter : MonoBehaviour
{
	public GameObject HeartPrefab;
	public int MaxHealth = 3;

	void Start()
	{
		foreach (var i in Enumerable.Range(0, MaxHealth))
		{
			Instantiate(HeartPrefab, transform);
		}
	}
}
