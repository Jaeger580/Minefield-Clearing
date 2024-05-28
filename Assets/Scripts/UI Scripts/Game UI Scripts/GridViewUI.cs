using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridViewUI : MonoBehaviour
{
    [SerializeField] private GridViewUIButton gridButtonPrefab;
    [SerializeField] private Transform gridViewButtonParent;

    public void GenerateAndBindButton(PhysicalTile relatedTile)
    {
        var newButton = Instantiate(gridButtonPrefab, gridViewButtonParent);
        newButton.physicalTile = relatedTile;
        newButton.name = $"{relatedTile.name} Button";
    }

    public void UpdateContainerSize(Vector2Int dims)
    {
        var parentRect = gridViewButtonParent as RectTransform;

        parentRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, dims.x * 50f);
        parentRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, dims.y * 50f);
    }
}
