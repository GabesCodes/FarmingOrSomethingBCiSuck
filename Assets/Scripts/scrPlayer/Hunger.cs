using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hunger : MonoBehaviour
{
    public PlayerScript player;
    private FoodHandler foodHandler;

    public TextMeshProUGUI hungerDisplay;

    public float hungerStat;
    public float hungerMax;
    public float hungerDrainSpeed;
    public float hungerDrainSpeedModifier;
    public bool canDrainHunger;


    // Start is called before the first frame update
    void Start()
    {
        PlayerScript player = GetComponent<PlayerScript>();
        FoodHandler foodhandler = GetComponent<FoodHandler>();

        hungerMax = 100f;
        hungerStat = 100f;
        hungerDrainSpeed = 0.5f;
        hungerDrainSpeedModifier = 2f;
        canDrainHunger = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (player.isMoving && canDrainHunger)
        {
            StartCoroutine(HungerDrainIE());
        }
    }

    private void RestoreHunger()
    {
        hungerStat += foodHandler.foodRestore; 
    }

    private void HungerDrain()
    {
        
        hungerStat -= hungerDrainSpeed;

        if (player.isSprinting)
        {
            hungerStat -= hungerDrainSpeed * hungerDrainSpeedModifier;
        }

        if (hungerStat <= 0)
        {
            player.isPlayerAlive = false;
            Destroy(player.gameObject);
        }
    }

    IEnumerator HungerDrainIE()
    {    
            HungerDrain();
            canDrainHunger = false;
            yield return new WaitForSeconds(3);
            canDrainHunger = true;
    }
}