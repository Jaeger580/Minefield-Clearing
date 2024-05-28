using UnityEngine;
using TMPro;

public class GridViewUIButton : MonoBehaviour
{
    [SerializeField] private TMP_Text minesNearbyText;
    [ReadOnly] public PhysicalTile physicalTile;
    private GameObject minesNearbyObject;

    private void Start()
    {
        physicalTile.AdjacentMinesUpdatedEvent += NearbyMinesUpdated;   //if BEFORE mines set
        minesNearbyText.text = $"{physicalTile.numAdjacentMines}";      //if AFTER mines set

        minesNearbyObject = minesNearbyText.gameObject;
        if (minesNearbyObject.activeInHierarchy) minesNearbyObject.SetActive(false);
    }

    private void NearbyMinesUpdated(int nearby)
    {
        minesNearbyText.text = $"{nearby}";
    }

    public void RevealTile()
    {
        if (!minesNearbyObject.activeInHierarchy) minesNearbyObject.SetActive(true);
    }
}