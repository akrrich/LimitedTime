using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolNode : MonoBehaviour
{
    public List<PatrolNode> connectedNodes; // Lista de nodos conectados a este nodo

    // Método opcional para visualizar los nodos en el editor
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
