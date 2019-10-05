using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Flammable : MonoBehaviour
{
    [SerializeField]
    private bool burning;
    [SerializeField]
    private float burnDuration = 2f;

    private Interactable interactable;
    private Cooldown burnCooldown;
    private Action extinguishAction;
    private Action igniteAction;

    void Awake()
    {
        extinguishAction = new Action("Extinguish", () => StopFire());
        igniteAction = new Action("Ignite", () => StartFire());
        burnCooldown = new Cooldown(burnDuration, () => StopFire());
    }

    void Start()
    {
        interactable = GetComponent<Interactable>();
        if (!interactable)
            Debug.LogError("No interactable found on " + gameObject);

        if (burning)
            StartFire();
        else
            StopFire();
    }

    void Update()
    {
        burnCooldown.Update(Time.deltaTime);
    }

    private void StartFire()
    {
        burning = true;
        interactable.AddAction(extinguishAction);
        interactable.RemoveAction(igniteAction);
        burnCooldown.Start();
    }

    private void StopFire()
    {
        burning = false;
        interactable.RemoveAction(extinguishAction);
        interactable.AddAction(igniteAction);
    }
}