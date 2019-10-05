using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Inventoryable : MonoBehaviour
{

    [SerializeField]
    private bool inventoried;

    public delegate void OnDrop();

    private Interactable interactable;
    private Action pickupAction;
    private Action dropAction;
    private List<Action> onDropActions = new List<Action>();

    void Awake()
    {
        pickupAction = new Action("Pick up", () => PickUp());
        dropAction = new Action("Drop", () => Drop());
    }

    void Start()
    {
        interactable = GetComponent<Interactable>();
        if (!interactable)
            Debug.LogError("No interactable found on " + gameObject);

        if (inventoried)
            PickUp();
        else
            Drop();
    }

    public void PickUp()
    {
        inventoried = true;
        interactable.RemoveAction(pickupAction);
        interactable.AddAction(dropAction);
    }

    private void Drop()
    {
        inventoried = false;
        interactable.AddAction(pickupAction);
        interactable.RemoveAction(dropAction);
        foreach (Action action in onDropActions)
            action.Activate();
    }

    public void AddOnDropAction(Action action)
    {
        onDropActions.Add(action);
    }

    public void RemoveOnDropAction(Action action)
    {
        onDropActions.Remove(action);
    }
}
