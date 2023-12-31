using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.U2D.Animation;

[Serializable]
public class AttributeResolvers 
{
    public int labelCount;
    public SpriteResolver[] resolvers;
}

public class PetSpriteAttributes : MonoBehaviour
{
    public PetAttributeResolverDict resolvers;
    public void Resolve(PetAttributeType attribute, int index)
    {
        foreach(var resolver in resolvers[attribute].resolvers)
        {
            resolver.SetCategoryAndLabel(resolver.GetCategory(), index.ToString());
        }
    }

}

[Serializable]
public class AttributeResolverStorage : SerializableDictionary.Storage<AttributeResolvers> {}
[Serializable]
public class PetAttributeResolverDict : SerializableDictionary<PetAttributeType, AttributeResolvers, AttributeResolverStorage> {}

