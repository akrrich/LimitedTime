using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DijkstraPathfinding : MonoBehaviour
{
    public List<PatrolNode> allNodes;

    public List<PatrolNode> FindShortestPath(PatrolNode startNode, PatrolNode targetNode)
    {
        // Crear un diccionario para almacenar las distancias mínimas desde el nodo de inicio
        Dictionary<PatrolNode, float> distances = new Dictionary<PatrolNode, float>();
        // Crear un diccionario para almacenar los nodos predecesores
        Dictionary<PatrolNode, PatrolNode> previousNodes = new Dictionary<PatrolNode, PatrolNode>();
        // Crear una lista para almacenar los nodos no visitados
        SortedList<float, PatrolNode> unvisitedNodes = new SortedList<float, PatrolNode>();

        // Inicializar las distancias y los nodos
        foreach (var node in allNodes)
        {
            if (node == startNode)
            {
                distances[node] = 0; // La distancia desde el nodo de inicio a sí mismo es 0
                unvisitedNodes.Add(0, node);
            }
            else
            {
                distances[node] = float.PositiveInfinity; // Los demás nodos están a una distancia infinita
                unvisitedNodes.Add(float.PositiveInfinity, node); // Agregarlos a la lista como no visitados
            }
            previousNodes[node] = null; // No hay predecesores al inicio
        }

        // Mientras haya nodos no visitados
        while (unvisitedNodes.Count > 0)
        {
            // Obtener el nodo con la distancia más baja
            var currentNode = unvisitedNodes.Values[0];
            unvisitedNodes.RemoveAt(0); // Lo eliminamos de la lista de no visitados

            // Si llegamos al nodo objetivo, podemos reconstruir el camino
            if (currentNode == targetNode)
            {
                List<PatrolNode> path = new List<PatrolNode>();
                while (previousNodes[currentNode] != null)
                {
                    path.Add(currentNode);
                    currentNode = previousNodes[currentNode];
                }
                path.Reverse();
                return path;
            }

            // Explorar los nodos vecinos
            foreach (var neighbor in currentNode.connectedNodes)
            {
                if (neighbor == null) continue;

                // Calcular la nueva distancia al vecino
                float tentativeDistance = distances[currentNode] + currentNode.connectionCosts[currentNode.connectedNodes.IndexOf(neighbor)];

                // Si encontramos una distancia más corta al vecino, actualizamos la distancia
                if (tentativeDistance < distances[neighbor])
                {
                    distances[neighbor] = tentativeDistance;
                    previousNodes[neighbor] = currentNode;

                    // Si el vecino no está en la lista de no visitados y tiene una distancia finita, lo agregamos
                    if (tentativeDistance < float.PositiveInfinity && !unvisitedNodes.ContainsValue(neighbor))
                    {
                        unvisitedNodes.Add(tentativeDistance, neighbor);
                    }
                }
            }
        }

        // Si no se encuentra ningún camino
        return null;
    }
}
