using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveCollection : MonoBehaviour
{
    public PassiveItem[] passives;
    public PassiveItem GetPassiveByNameID(string id)
    {
        foreach (PassiveItem p in passives)
        {
            if ( String.Equals(p.passiveItemData.PassiveName, id,StringComparison.Ordinal))
            {
                return p;
            }
        }
        return null;
    }
}
