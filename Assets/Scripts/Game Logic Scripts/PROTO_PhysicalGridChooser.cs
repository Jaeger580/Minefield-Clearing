using System.Collections.Generic;
using UnityEngine;

public class PROTO_PhysicalGridChooser : MonoBehaviour
{
    //PURELY FOR THE PROTOTYPE, replace later with a proper algorithm
    [SerializeField] private List<PhysicalGrid> possibleLayouts = new();

    private void Awake()
    {
        foreach (var layout in possibleLayouts) layout.enabled = false;

        var chosenLayoutIndex = Random.Range(0, possibleLayouts.Count);

        possibleLayouts[chosenLayoutIndex].enabled = true;
    }
}