using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cola<T> : MonoBehaviour
{
    private List<T> elementos = new List<T>();

    public void Enqueue(T elemento)
    {
        elementos.Add(elemento);
    }

    public T Dequeue()
    {
        if (elementos.Count == 0)
        {
            throw new Exception("Cola vacia");
        }

        T elemento = elementos[0];
        elementos.RemoveAt(0);

        return elemento;
    }

    public T Peek()
    {
        if (elementos.Count == 0)
        {
            throw new Exception("Cola vacia");
        }

        return elementos[0];
    }

    public bool Empty()
    {
        return elementos.Count == 0;
    }
}
