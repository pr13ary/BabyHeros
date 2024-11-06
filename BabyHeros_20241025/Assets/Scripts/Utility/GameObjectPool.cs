using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;

public class GameObjectPool<T> where T : Component
{
    Queue<T> m_pool = new Queue<T>();
    Func<T> m_createFunc;
    int m_count = 0;

    public GameObjectPool(int count, Func<T> createFunc)
    {
        m_count = count;
        m_createFunc = createFunc;
        Allocation();
    }

    public T Get()
    {
        if(m_pool.Count > 0)
        {
            return m_pool.Dequeue();
        }
        return m_createFunc();
    }

    public void Set(T obj)
    {
        m_pool.Enqueue(obj);
    }

    void Allocation()
    {
        for(int i = 0; i < m_count; i++)
        {
            m_pool.Enqueue(m_createFunc());
        }
    }
}
