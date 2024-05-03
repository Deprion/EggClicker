using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ListDictionary<T, Y>
{
    [SerializeField] private List<T> keys;
    [SerializeField] private List<Y> values;

    public ListDictionary() : this(10) { }
    public ListDictionary(int size)
    { 
        keys = new List<T>(size);
        values = new List<Y>(size);
    }

    public Y this[T key]
    {
        get 
        {
            return GetValue(key);
        }
        set
        {
            SetValueOrAdd(key, value);
        }
    }

    private Y GetValue(T key)
    { 
        for (int i = 0; i < keys.Count; i++) 
        {
            if (keys[i].Equals(key))
                return values[i];
        }

        throw new Exception($"No such key: {key}");
    }

    public Y GetValueOrDefault(T key)
    {
        for (int i = 0; i < keys.Count; i++)
        {
            if (keys[i].Equals(key))
                return values[i];
        }

        return default(Y);
    }

    private void SetValueOrAdd(T key, Y value)
    {
        for (int i = 0; i < keys.Count; i++)
        {
            if (keys[i].Equals(key))
            {
                values[i] = value;
                return;
            }
        }

        keys.Add(key);
        values.Add(value);
    }

    public void Add(T key, Y value)
    {
        if (Contains(key))
            throw new Exception($"Such key already exist: {key}");

        keys.Add(key);
        values.Add(value);
    }

    public bool Contains(T key)
    {
        for (int i = 0; i < keys.Count; i++)
        {
            if (keys[i].Equals(key))
                return true;
        }

        return false;
    }

    public bool ContainsValue(Y value)
    {
        for (int i = 0; i < keys.Count; i++)
        {
            if (values[i].Equals(value))
                return true;
        }

        return false;
    }
}
