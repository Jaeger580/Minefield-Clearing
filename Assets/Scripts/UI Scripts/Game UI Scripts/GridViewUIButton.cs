using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GridViewUIButton : MonoBehaviour
{
    [SerializeField] private TMP_Text minesNearbyText;
    [SerializeField] private GameObject flagUIObject;
    [ReadOnly] public PhysicalTile physicalTile;
    private GameObject minesNearbyObject;

    public delegate void OnButtonFlagChanged(bool flagShowing);
    public OnButtonFlagChanged ButtonFlagChangedEvent;

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

    public void ChangeColor()
    {

    }

    public void ToggleFlag(bool flagged)
    {
        if (flagUIObject.activeInHierarchy == flagged) return;

        flagUIObject.SetActive(flagged);
        ButtonFlagChangedEvent?.Invoke(flagUIObject.activeInHierarchy);
    }

    public void ToggleFlag()
    {
        flagUIObject.SetActive(!flagUIObject.activeInHierarchy);
        ButtonFlagChangedEvent?.Invoke(flagUIObject.activeInHierarchy);
        physicalTile.UpdateFlagPlacement(flagUIObject.activeInHierarchy);
    }
}