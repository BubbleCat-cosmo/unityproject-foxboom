using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    private void LateUpdate()
    {
        Vector3 pos = target.position;
        pos.z = -10;
        transform.position = pos;

    }
}
