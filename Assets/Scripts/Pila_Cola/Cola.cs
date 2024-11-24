using System;
using System.Collections.Generic;

public class Cola<T>
{
    private List<T> elementos = new List<T>();

    public void Enqueue(T elemento)
    {
        elementos.Add(elemento);
    }

    public T Dequeue()
    {
        if (Empty())
        {
            throw new Exception("Cola vacia");
        }

        T elemento = elementos[0];
        elementos.RemoveAt(0);

        return elemento;
    }

    public T Peek()
    {
        if (Empty())
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
