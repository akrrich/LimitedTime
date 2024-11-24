using UnityEngine;

public class SpawnerPosition : MonoBehaviour
{
    [SerializeField] private int id;

    private void Start()
    {
        AbstractFactory.CreateObject(id, transform);
    }
}
