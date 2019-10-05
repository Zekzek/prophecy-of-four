using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RelationshipData
{
    [SerializeField]
    private string characterId;
    public string CharacterId { get { return characterId; } }

    [SerializeField]
    private List<RelationshipFeelingData> feelings;
    public List<RelationshipFeelingData> Feelings { get { return feelings; } }
}
