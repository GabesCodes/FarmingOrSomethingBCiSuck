using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodHandler : MonoBehaviour
{

    public int foodRestore;

    [SerializeField]
    private FoodTypes foods;



    // Start is called before the first frame update
    void Start()
    {
        foodRestore = foods.foodValue;
    }

    // Update is called once per frame
    void Update()
    {

    }

}


