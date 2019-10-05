using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Inventoryable))]
public class Equipable : MonoBehaviour
{

    [SerializeField]
    private bool equipped;

    private Interactable interactable;
    private Inventoryable inventoryable;
    private Action equipAction;
    private Action unequipAction;

    void Awake()
    {
        equipAction = new Action("Equip", () => Equip());
        unequipAction = new Action("Unequip", () => Unequip());
    }

    void Start()
    {
        interactable = GetComponent<Interactable>();
        if (!interactable)
            Debug.LogError("No interactable found on " + gameObject);
        inventoryable = GetComponent<Inventoryable>();
        inventoryable.AddOnDropAction(unequipAction);

        if (equipped)
            Equip();
        else
            Unequip();
    }

    private void Equip()
    {
        inventoryable.PickUp();

        equipped = true;
        interactable.RemoveAction(equipAction);
        interactable.AddAction(unequipAction);
    }

    private void Unequip()
    {
        equipped = false;
        interactable.AddAction(equipAction);
        interactable.RemoveAction(unequipAction);
    }
}
