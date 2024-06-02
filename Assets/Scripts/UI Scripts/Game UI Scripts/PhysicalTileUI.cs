using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class PhysicalTileUI : MonoBehaviour
{
    [SerializeField] private PhysicalTile physicalTile;
    [SerializeField] private TMP_Text minesNearbyText;
    [SerializeField] private GameObject flagObject;
    private GameObject minesNearbyObject;

    private void Start()
    {
        physicalTile.AdjacentMinesUpdatedEvent += NearbyMinesUpdated;   //if BEFORE mines set
        minesNearbyText.text = $"{physicalTile.numAdjacentMines}";      //if AFTER mines set

        physicalTile.TileEnteredEvent += RevealTile;
        physicalTile.FlagChangedEvent += ToggleFlag;

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

    public void ToggleFlag(bool flagged)
    {
        flagObject.SetActive(flagged);
    }

    public void ToggleFlag()
    {
        flagObject.SetActive(!flagObject.activeInHierarchy);
    }
}
