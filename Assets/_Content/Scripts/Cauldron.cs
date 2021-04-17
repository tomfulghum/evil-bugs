using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grabbable"))
        {
            var ingredient = other.GetComponent<Ingredient>();
            if (ingredient)
            {
                var type = ingredient.type;
                manager.AddIngredient(type);
                Destroy(other.transform.parent.gameObject);
            }
        }
    }
}
