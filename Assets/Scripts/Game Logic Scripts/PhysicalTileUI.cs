using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class PhysicalTileUI : MonoBehaviour
{
    [SerializeField] private PhysicalTile physicalTile;
    [SerializeField] private TMP_Text minesNearbyText;
    private Transform mainCamTrans;
    private GameObject minesNearbyObject;

    private void Start()
    {
        physicalTile.AdjacentMinesUpdatedEvent += NearbyMinesUpdated;   //if BEFORE mines set
        minesNearbyText.text = $"{physicalTile.numAdjacentMines}";      //if AFTER mines set

        physicalTile.TileEnteredEvent += RevealTile;

        minesNearbyObject = minesNearbyText.gameObject;
        if (minesNearbyObject.activeInHierarchy) minesNearbyObject.SetActive(false);

        mainCamTrans = Camera.main.transform;

    }

    private void NearbyMinesUpdated(int nearby)
    {
        minesNearbyText.text = $"{nearby}";
    }

    private void Update()
    {
        if (!gameObject.activeInHierarchy) return;
        var lookAtPos = transform.position - mainCamTrans.position;

        lookAtPos.y = 0f;

        transform.LookAt(lookAtPos);
    }

    public void RevealTile()
    {
        if (!minesNearbyObject.activeInHierarchy) minesNearbyObject.SetActive(true);
    }
}
