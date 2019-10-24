using System;

public static class Util
{
    public enum DamageType { Physical, Magical, Melee, Ranged, Crushing, Piercing, Slashing, Fire, Cold, Lightning }

    public static int GetDamage(AbilityData ability, Character attacker, Thing defender)
    {
        throw new NotImplementedException();
        /*
        float damageBonus = AbilityModifier.GetAverageModifier(attacker.DamageModifier, ability.DamageTypes);
        float damageResist = AbilityModifier.GetAverageModifier(defender.DamageModifier, ability.DamageTypes);
        float netDamageBonus = Mathf.Max(1 + damageBonus, 0) * Mathf.Max(1 - damageResist, 0);

        float piercingBonus = AbilityModifier.GetSumModifier(attacker.PiercingModifier, ability.DamageTypes);
        float piercingResist = AbilityModifier.GetSumModifier(defender.PiercingModifier, ability.DamageTypes);
        float netPiercingBonus = Mathf.Max(piercingBonus - piercingResist, 0) * UnityEngine.Random.Range(0.5f, 1.5f);

        float offenseBonus = AbilityModifier.GetSumModifier(attacker.OffenseModifier, ability.DamageTypes);
        float offenseResist = AbilityModifier.GetSumModifier(defender.OffenseModifier, ability.DamageTypes);
        offenseResist = Mathf.Max(offenseResist - netPiercingBonus, 0);
        float netOffenseBonus = offenseBonus > offenseResist ? Mathf.Sqrt(offenseBonus - offenseResist) : -Mathf.Sqrt(offenseResist - offenseBonus);
        netOffenseBonus = Mathf.Max(1 + netOffenseBonus / 100, 0) * UnityEngine.Random.Range(0.5f, 1.5f); ;

        float critBonus = AbilityModifier.GetSumModifier(attacker.CritModifier, ability.DamageTypes);
        float critResist = AbilityModifier.GetSumModifier(defender.CritModifier, ability.DamageTypes);
        float netCritBonus = Mathf.Max(1 + critBonus, 0) * Mathf.Max(1 - critResist, 0) * UnityEngine.Random.Range(0.5f, 1.5f); ;

        int totalDamage = (int)(ability.Damage * netDamageBonus * netOffenseBonus);

        return totalDamage;
        */
    }
}