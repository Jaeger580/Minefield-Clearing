using UnityEngine;
using TMPro;

public class PhysicalTileUI : MonoBehaviour
{
    [SerializeField] private PhysicalTile physicalTile;
    [SerializeField] private TMP_Text minesNearbyText;
    private Transform mainCamTrans;

    private void OnEnable()
    {
        physicalTile.AdjacentMinesUpdatedEvent += NearbyMinesUpdated;
    }

    private void Start()
    {
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
}
