using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public List<PatrolNode> nodes; // Lista de todos los nodos del grafo

    // M�todo para encontrar el camino m�s corto entre dos nodos usando Dijkstra
    public List<PatrolNode> FindShortestPath(PatrolNode startNode, PatrolNode targetNode)
    {
        // Diccionario para rastrear el costo m�s bajo para llegar a cada nodo
        Dictionary<PatrolNode, float> costs = new Dictionary<PatrolNode, float>();
        Dictionary<PatrolNode, PatrolNode> parents = new Dictionary<PatrolNode, PatrolNode>();
        HashSet<PatrolNode> visited = new HashSet<PatrolNode>();

        foreach (var node in nodes)
        {
            costs[node] = float.MaxValue; // Costo infinito inicial
        }
        costs[startNode] = 0;

        while (visited.Count < nodes.Count)
        {
            PatrolNode currentNode = null;
            float lowestCost = float.MaxValue;

            // Encontrar el nodo no visitado con el costo m�s bajo
            foreach (var node in nodes)
            {
                if (!visited.Contains(node) && costs[node] < lowestCost)
                {
                    lowestCost = costs[node];
                    currentNode = node;
                }
            }

            if (currentNode == null || currentNode == targetNode)
            {
                break; // Si no hay m�s nodos o llegamos al objetivo, salimos
            }

            visited.Add(currentNode);

            // Revisar vecinos y actualizar costos
            for (int i = 0; i < currentNode.connectedNodes.Count; i++)
            {
                var neighbor = currentNode.connectedNodes[i];
                if (visited.Contains(neighbor)) continue;

                float newCost = costs[currentNode] + currentNode.connectionCosts[i];
                if (newCost < costs[neighbor])
                {
                    costs[neighbor] = newCost;
                    parents[neighbor] = currentNode;
                }
            }
        }

        // Reconstruir el camino
        List<PatrolNode> path = new List<PatrolNode>();
        PatrolNode step = targetNode;

        while (parents.ContainsKey(step))
        {
            path.Insert(0, step);
            step = parents[step];
        }

        if (step == startNode) // Si alcanzamos el nodo inicial, el camino es v�lido
        {
            path.Insert(0, startNode);
        }

        return path;
    }

    // M�todo recursivo para encontrar el camino

}
