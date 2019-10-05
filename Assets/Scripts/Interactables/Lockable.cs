using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: keys
[RequireComponent(typeof(Interactable))]
public class Lockable : MonoBehaviour
{
    [SerializeField]
    private bool locked;

    private Interactable interactable;
    private Action lockAction;
    private Action unlockAction;

    void Awake()
    {
        lockAction = new Action("Lock", () => Lock());
        unlockAction = new Action("UnLock", () => Unlock());
    }

    void Start()
    {
        interactable = GetComponent<Interactable>();
        if (!interactable)
            Debug.LogError("No interactable found on " + gameObject);

        if (locked)
            Lock();
        else
            Unlock();
    }

    private void Lock()
    {
        locked = true;
        interactable.RemoveAction(lockAction);
        interactable.AddAction(unlockAction);
    }

    private void Unlock()
    {
        locked = false;
        interactable.AddAction(lockAction);
        interactable.RemoveAction(unlockAction);
    }
}