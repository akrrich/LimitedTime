using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PatrolNode : MonoBehaviour
{
    public List<PatrolNode> connectedNodes; // Lista de nodos conectados a este nodo
    public List<float> connectionCosts; // Costo de cada conexión

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 1f);

        foreach (PatrolNode neighbor in connectedNodes)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, neighbor.transform.position);
        }
    }
    
}
