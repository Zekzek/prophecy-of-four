using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    [SerializeField]
    private List<Action> actions;

    void OnCollider(Collider collider)
    {

    }

    public void AddAction(Action action)
    {
        if (!actions.Contains(action))
            actions.Add(action);
    }

    public void RemoveAction(Action action)
    {
        actions.Remove(action);
    }
}