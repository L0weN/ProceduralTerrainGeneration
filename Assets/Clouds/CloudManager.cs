using UnityEngine;

public class CloudManager : MonoBehaviour
{
    private void OnDrawGizmosSelected()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        Gizmos.DrawLine(Vector3.zero, Vector3.forward);
    }
}
