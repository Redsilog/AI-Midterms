using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class keyRandomizer : MonoBehaviour
{
    public GameObject keyPrefab;
    public int keyCount = 5;
    public Vector3 areaSize = new Vector3(20, 0, 20);
    public NavMeshSurface navMeshSurface;
    public LayerMask groundLayer;

    void Start()
    {
        GenerateKeys();
        if (navMeshSurface != null)
            navMeshSurface.BuildNavMesh();
    }

    void GenerateKeys()
    {
        for (int i = 0; i < keyCount; i++)
        {
            Vector3 localPos = new Vector3(
                Random.Range(-areaSize.x / 2, areaSize.x / 2),
                20f,
                Random.Range(-areaSize.z / 2, areaSize.z / 2)
            );

            Vector3 worldPos = transform.position + localPos;
            Ray ray = new Ray(worldPos, Vector3.down);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 50f, groundLayer))
            {
                GameObject key = Instantiate(keyPrefab, hitInfo.point + Vector3.up * 0.1f, Quaternion.identity);
                key.transform.SetParent(transform);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, 0.5f, 0), areaSize);
    }
}

