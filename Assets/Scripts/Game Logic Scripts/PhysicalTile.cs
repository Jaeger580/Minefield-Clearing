using UnityEngine;

public class PhysicalTile : MonoBehaviour
{
    [SerializeField] private GameObject mineObject;
    [SerializeField] private bool hasMine = false;
    public bool IsMine => hasMine;

    [ReadOnly] public int numAdjacentMines = 0;

    public delegate void OnAdjacentMinesUpdated(int val);
    public OnAdjacentMinesUpdated AdjacentMinesUpdatedEvent;

    public void OnDisable()
    {
        AdjacentMinesUpdatedEvent = null;
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
}