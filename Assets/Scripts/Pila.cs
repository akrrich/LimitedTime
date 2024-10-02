using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pila<T>
{
    private List<T> elements = new List<T>();

    public void Push(T elemento)
    {
        elements.Add(elemento);
    }

    public T Pop()
    {
        if (elements.Count == 0)
        {
            throw new Exception("Pila vacia");
        }

        T elemento = elements[elements.Count - 1];
        elements.RemoveAt(elements.Count - 1);

        return elemento;
    }

    public T Peek()
    {
        if (elements.Count == 0)
        {
            throw new Exception("Pila vacia");
        }

        return elements[elements.Count - 1];
    }

    public bool Empty()
    {
        return elements.Count == 0;
    }
}
