using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

public enum IngredientType
{
    [Description("Frog Leg")] FrogLeg,
    [Description("Eye")] Eye,
    [Description("Dead Bug")] DeadBug,
    [Description("Egg")] Egg,
    [Description("Feather")] Feather,
    [Description("Tooth")] Tooth,
    [Description("Dry Leaf")] DryLeaf,
    [Description("Mushroom")] Mushroom,
    Cleanser,
    None
}

public class Ingredient : MonoBehaviour
{
    public IngredientType type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
