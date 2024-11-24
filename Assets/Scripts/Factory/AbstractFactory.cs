using System.Collections.Generic;
using UnityEngine;

public class AbstractFactory : MonoBehaviour
{
    [SerializeField] private Objects[] objects;
    private static Dictionary<int, Objects> idObjects = new Dictionary<int, Objects>();

    [SerializeField] private PowerUps[] powerUps;
    private static Dictionary<int, PowerUps> idPowerUps = new Dictionary<int, PowerUps>();


    void Awake()
    {
        foreach (Objects obj in objects)
        {
            idObjects.Add(obj.Id, obj);
        }

        foreach (PowerUps powerUps in powerUps)
        {
            idPowerUps.Add(powerUps.Id, powerUps);
        }
    }

    void OnDestroy()
    {
        idObjects.Clear();
        idPowerUps.Clear();
    }


    public static Objects CreateObject(int id, Transform transform)
    {
        if (!idObjects.TryGetValue(id, out Objects obj))
        {
            return null;
        }

        return Instantiate(obj, transform.position, Quaternion.identity);
    }

    public static PowerUps CreatePowerUp(int id, Transform transform)
    {
        if (!idPowerUps.TryGetValue(id, out PowerUps powerUps))
        {
            return null;
        }

        return Instantiate(powerUps, transform.position, Quaternion.identity);
    }
}
