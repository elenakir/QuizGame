using UnityEngine;

public class CellSpawner : MonoBehaviour
{
    [SerializeField] private Cell _prefab;

    public Cell Spawn()
    {
        return Instantiate(_prefab);
    }
}
