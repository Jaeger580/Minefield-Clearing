using UnityEngine;

public class PhysicalTile : MonoBehaviour
{
    [SerializeField] private GameObject mineObject;
    [SerializeField] private bool hasMine = false;
    public bool IsMine => hasMine;

    [ReadOnly] public int numAdjacentMines = 0;

    public delegate void OnAdjacentMinesUpdated(int val);
    public OnAdjacentMinesUpdated AdjacentMinesUpdatedEvent;

    public delegate void OnTileEntered();
    public OnTileEntered TileEnteredEvent;

    public delegate void OnFlagChanged(bool flagShowing);
    public OnFlagChanged FlagChangedEvent;

    public void OnDisable()
    {
        AdjacentMinesUpdatedEvent = null;
        TileEnteredEvent = null;
        FlagChangedEvent = null;
    }

    public void SetMine()
    {
        hasMine = true;
        mineObject.SetActive(true);
    }

    public void SetAdjacentMines(int inVicinity)
    {
        numAdjacentMines = hasMine ? inVicinity - 1 : inVicinity;
        AdjacentMinesUpdatedEvent?.Invoke(numAdjacentMines);
    }

    private void OnTriggerEnter(Collider other)
    {
        TileEnteredEvent?.Invoke();
        if (hasMine) GameOverScreen.instance.DisplayGameOver(false);
    }

    public void UpdateFlagPlacement(bool flagShowing)
    {
        FlagChangedEvent?.Invoke(flagShowing);
    }
}