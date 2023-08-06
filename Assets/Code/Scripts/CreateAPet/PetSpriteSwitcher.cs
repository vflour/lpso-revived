using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.VisualScripting;

public class PetSpriteSwitcher : MonoBehaviour
{
    private PetAttributeType _selectedAttribute = PetAttributeType.None;
    public PetAttributeType selectedAttribute 
    { 
        get { return _selectedAttribute; }

        set
        {
            _selectedAttribute = value;
            AttributeSelected.Invoke();
        }
    }
    
    // Registers a switcher button since you can't do it from visual scripting
    public void RegisterSwitcherButton(ScriptMachine scriptMachine) 
    {
        AttributeSelected.AddListener(() => scriptMachine.TriggerUnityEvent("AttributeSelected"));
    }
    
    public CreateAPetSprites sprites;
    public CreateAPetStands stands;
    public UnityEvent AttributeSelected;

    public PetSpriteAttributes SpriteAttributes => stands.sprites[0].GetComponent<PetSpriteAttributes>();

    public void SwitchAttribute(PetAttributeType attributeType, int adder = 1)
    {
        if (attributeType == PetAttributeType.None) return;

        PetSpriteAttributes attributes = SpriteAttributes;
        int maxAttributes = attributes.resolvers[attributeType].labelCount;
        
        // Switch attrib index, use modulo to go back around
        
        AttributeSelected.Invoke();
        int attributeValue = (sprites.currentPet.attributes[attributeType] + adder + maxAttributes) % maxAttributes;
        sprites.currentPet.attributes[attributeType] = attributeValue;
        
        // Gender isn't a sprite
        if (attributeType != PetAttributeType.Gender)
           attributes.Resolve(attributeType, attributeValue);
    }

    public void SwitchAttribute(int adder)
    {
        SwitchAttribute(selectedAttribute, adder);
    }

    public void SelectAttributeButton(PetAttributeType attributeType)
    {
        
        selectedAttribute = attributeType;
        if (selectedAttribute == attributeType || attributeType == PetAttributeType.Gender)
        {
            SwitchAttribute(attributeType);
        }
    }

}
