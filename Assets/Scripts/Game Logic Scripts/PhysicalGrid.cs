using System;
using System.Collections.Generic;
using UnityEngine;

//ATW 5/23/24
[Serializable]
public class Map<TKey, TValue>
{
	[SerializeField]
	private List<TKey> keysList = new();
	public List<TKey> KeysList
	{
		get { return keysList; }
		set { keysList = value; }
	}

	[SerializeField]
	private List<TValue> valuesList = new();
	public List<TValue> ValuesList
	{
		get { return valuesList; }
		set { valuesList = value; }
	}

	private Dictionary<TKey, TValue> dictionaryData = new();
	public Dictionary<TKey, TValue> DictionaryData
	{
		get { return dictionaryData; }
		set { dictionaryData = value; }
	}

	public void UpdateDictionary()
    {
		DictionaryData.Clear();
		for(int i = 0; i < keysList.Count; i++)
        {
			DictionaryData.TryAdd(KeysList[i], ValuesList[i]);
        } 
    }
}

public class PhysicalGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int dims;
	[Range(0f, 1f)]
	[SerializeField] private float minePercentChance;
	 
    [Serializable] public class GridMap : Map<Vector2Int, bool> { }
	//[HideInInspector]
	public GridMap mineGrid = new();

	private void Start()
    {
        foreach (var pair in mineGrid.DictionaryData)
        {
            if (pair.Value) print($"MINE AT: ({pair.Key.x}, {pair.Key.y})");
        }
    }

	public void RandomizeMines()
    {
		foreach(var pair in mineGrid.DictionaryData)
        {
			mineGrid.ValuesList[mineGrid.KeysList.IndexOf(pair.Key)] = UnityEngine.Random.Range(0f, 0.99f) < minePercentChance;
		}
		mineGrid.UpdateDictionary();
    }
	public void ClearMines()
	{
		foreach (var pair in mineGrid.DictionaryData)
		{
			mineGrid.ValuesList[mineGrid.KeysList.IndexOf(pair.Key)] = false;
		}
		mineGrid.UpdateDictionary();
	}
}
