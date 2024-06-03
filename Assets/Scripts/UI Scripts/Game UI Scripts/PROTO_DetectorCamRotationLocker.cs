using UnityEngine;

public class PROTO_DetectorCamRotationLocker : MonoBehaviour
{
    private Quaternion initialRotation;

    private void Awake()
    {
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        transform.rotation = initialRotation;
    }
}