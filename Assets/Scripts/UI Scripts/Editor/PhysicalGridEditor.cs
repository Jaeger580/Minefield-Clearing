using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

//ATW 5/23/24
[CustomEditor(typeof(PhysicalGrid))]
public class PhysicalGridEditor : Editor
{
    public VisualTreeAsset visualTree;

    private PhysicalGrid physicalGrid;

    private PropertyField _dimensionsField, _tileRadiusField;
    private ObjectField _physicalTileParentField, _physicalTilePrefabField;
    private SerializedProperty _vector2Property;

    private VisualElement gridContainer;

    private Button buttonRandomizeMines, buttonClearMines;

    private void OnEnable()
    {
        physicalGrid = (PhysicalGrid)target;                        //set the physical grid to the one we're inspecting
        _vector2Property = serializedObject.FindProperty("dims");   //get that one's dims variable
    }

    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new(); //Make a new root
        visualTree.CloneTree(root); //Make it a clone the tree we made in UIBuilder

        gridContainer = root.Q<VisualElement>("GridContainer");

        _dimensionsField = root.Q<PropertyField>("DimensionsField");
        _dimensionsField.RegisterCallback<ChangeEvent<Vector2Int>>(OnDimsChanged);

        buttonRandomizeMines = root.Q<Button>("RandomizeMines");
        buttonRandomizeMines.RegisterCallback<ClickEvent>((evt) => RandomizeMinesClicked());
        buttonClearMines = root.Q<Button>("ClearMines");
        buttonClearMines.RegisterCallback<ClickEvent>((evt) => ClearMinesClicked());

        physicalGrid.MineGrid.UpdateDictionary();

        return root;
    }

    private void RandomizeMinesClicked()
    {
        physicalGrid.RandomizeMines();
        OnDimsChanged(null);
    }

    private void ClearMinesClicked()
    {
        physicalGrid.ClearMines();
        OnDimsChanged(null);
    }

    private void OnDimsChanged(ChangeEvent<Vector2Int> evt)
    {//Whenever we change the dimensions of the grid,
        gridContainer.Clear();                  //Clear the inspector grid
        var mineGrid = physicalGrid.MineGrid;   //store a reference we reuse
        mineGrid.KeysList.Clear();              //Clear all Key Value Pairs (KVPs) so we can re-add them
        mineGrid.ValuesList.Clear();

        for (int y = 0; y < _vector2Property.vector2IntValue.y; y++)
        {//Grid forloop (left to right, bottom to top)
            for (int x = 0; x < _vector2Property.vector2IntValue.x; x++)
            {
                string key = $"Tile ({x}, {y})";
                int newX = x, newY = y;         //Necessary for callback to work properly (otherwise uses max values, idk why)

                Toggle gridTile = new();
                gridTile.name = key;
                gridTile.AddToClassList("gridTile");    //Hides the label and adds custom style
                gridContainer.Add(gridTile);

                //If the old dictionary already has this tile, set this tile's value equal to the old value
                if (physicalGrid.MineGrid.DictionaryData.TryGetValue(new(newX, newY), out bool val)) gridTile.value = val;

                if (val) gridTile.AddToClassList("mineTile");
                else gridTile.RemoveFromClassList("mineTile");

                mineGrid.KeysList.Add(new(newX, newY));     //Add the tile to the list of tiles
                mineGrid.ValuesList.Add(gridTile.value);    //Add the mine value to the list of mines

                //When you click the grid tile, trigger OnMineTileChanged with specific values
                gridTile.RegisterCallback<ChangeEvent<bool>>((evt) => OnMineTileChanged(new(newX,newY), evt.newValue));
            }
        }

        mineGrid.UpdateDictionary();    //Update the old dictionary with the new Keys and Values lists
        gridContainer.style.width = _vector2Property.vector2IntValue.x * 20f + 10f; //Update the grid container's width
    }

    private void OnMineTileChanged(Vector2Int tile, bool newValue)
    {//When a mine is placed or removed,
        var mineGrid = physicalGrid.MineGrid;

        //Update the Values list at the matching index of the tile to be the new value, then update the dictionary
        mineGrid.ValuesList[mineGrid.KeysList.IndexOf(tile)] = newValue;
        mineGrid.UpdateDictionary();
        var gridTile = gridContainer.Q<Toggle>($"Tile ({tile.x}, {tile.y})");
        if (newValue) gridTile.AddToClassList("mineTile");
        else gridTile.RemoveFromClassList("mineTile");
    }
}
