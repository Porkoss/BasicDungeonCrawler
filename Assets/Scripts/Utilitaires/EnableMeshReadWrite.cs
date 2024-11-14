using UnityEngine;

public class EnableMeshReadWrite : MonoBehaviour
{
    void Start()
    {
        Mesh mesh = Resources.FindObjectsOfTypeAll<Mesh>()[0];
        if (mesh != null && !mesh.isReadable)
        {
            Debug.Log($"Enabling Read/Write for mesh: {mesh.name}");
            mesh.MarkDynamic(); // Enables read/write access
        }
    }
}
