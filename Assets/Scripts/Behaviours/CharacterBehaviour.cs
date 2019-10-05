using UnityEngine;

public class CharacterBehaviour : ThingBehaviour
{
    public GameObject Goal { get; set; }

    private void Update()
    {
        if (Goal != null)
        {
            Vector3 deltaPos = Goal.transform.position - transform.position;
            if (deltaPos.sqrMagnitude > 1)
            {
                deltaPos.Normalize();
                transform.position += deltaPos * 3 * Time.deltaTime;
            }
        }
    }

    public bool UseAttack(AbilityData ability, Character attacker, Thing defender)
    {
        if (!attacker.Alive)
        {
            Debug.Log(attacker.DisplayName + " is unconscious and can't use " + ability.DisplayName);
            return false;
        }
        if (!defender.Alive)
        {
            Debug.Log(defender.DisplayName + " is already unconscious and can't be hit with " + ability.DisplayName);
            return false;
        }
        foreach (ResourceCostData resourceCost in ability.ResourceCost)
        {
            if (!attacker.HasResource(resourceCost))
            {
                Debug.Log(attacker.DisplayName + " doesn't have enough " + resourceCost.Resource + " to use " + ability.DisplayName);
                return false;
            }
        }

        int damage = Util.GetDamage(ability, attacker, defender);

        Debug.Log(attacker.DisplayName + " uses " + ability.DisplayName + " and deals " + damage + " damage to " + defender.DisplayName);

        attacker.SpendResourcesFor(ability);
        defender.TakeDamage(damage);
        return true;
    }
}