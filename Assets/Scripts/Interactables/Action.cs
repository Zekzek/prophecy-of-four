using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Action
{
    public delegate void OnAction();

    [SerializeField]
    private string name;
    private OnAction callback;

    public Action(string name, OnAction callback)
    {
        this.name = name;
        this.callback = callback;
    }

    public void Activate()
    {
        callback();
    }
}
