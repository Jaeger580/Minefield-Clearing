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
	[Min(1)]
    [SerializeField] private Vector2Int dims;
	[Range(0f, 1f)]
	[SerializeField] private float minePercentChance;
	 
    [Serializable] public class GridMap : Map<Vector2Int, bool> { }
	public GridMap MineGrid = new();    //Public because the editor needs access

    private void Start()
    {
        /*
         * foreach(pair in MineGrid.dictionary)
         *		Instantiate(tile prefab under some parent)
         *		place it based on pair's key (key = x,y coordinate)
         *			-> tile.SetPosition(new Vector3(xvalue * some offset, 0, yvalue * some offset))
         *		toggle whether it's supposed to be a mine based on pair's value (value = is it a mine?)
         *		Everything else is handled by prefab itself
         *		
         *		int numAdjMines = Physics.checkarea on the mine layer
         *		set my prefab text numAdjMines
         *		disable UI for it (enables on enter)
         */
    }

    public void RandomizeMines()
    {
		foreach(var pair in MineGrid.DictionaryData)
        {
			MineGrid.ValuesList[MineGrid.KeysList.IndexOf(pair.Key)] = UnityEngine.Random.Range(0f, 0.99f) < minePercentChance;
		}
		MineGrid.UpdateDictionary();
    }

	public void ClearMines()
	{
		foreach (var pair in MineGrid.DictionaryData)
		{
			MineGrid.ValuesList[MineGrid.KeysList.IndexOf(pair.Key)] = false;
		}
		MineGrid.UpdateDictionary();
	}

	[ContextMenu("Print Mines To Console")]
	private void PrintMinesToConsole()
	{
		foreach (var pair in MineGrid.DictionaryData)
		{//Purely for debugging purposes - checks whether the mines line up with the toggleboxes in the inspector
			if (pair.Value) print($"MINE AT: ({pair.Key.x}, {pair.Key.y})");
		}
	}
}
