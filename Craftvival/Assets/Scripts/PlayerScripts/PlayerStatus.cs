using UnityEngine;
using UnityEngine.UI;


public class PlayerStatus : MonoBehaviour
{
    //Script made by Charly
    public PlayerHealth playerHealth;
    public float maxHealth = 200;
    public float maxFood = 200;
    public float maxWater = 200;
    public float food;
    public float water;
    public float healthDecayRate = 5f;
    public float foodDecayRate = 2f;
    public float waterDecayRate = 3f;
    public Slider healthBar;
    public Slider foodBar;
    public Slider waterBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        //Set health, food and water
        playerHealth.health = maxHealth;
        food = maxFood;
        water = maxWater;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        //Lose food and water at the decay rate
        food -= foodDecayRate * Time.deltaTime;
        water -= waterDecayRate * Time.deltaTime;

        //Makes sure the amount of health, food and water doesn't go below 0 or above the maximum
        playerHealth.health = Mathf.Clamp(playerHealth.health, 0, maxHealth);
        food = Mathf.Clamp(food, 0, maxFood);
        water = Mathf.Clamp(water, 0, maxWater);

        //If you either have no food or water
        if (food == 0 || water == 0)
        {
            //Lose health at the decay rate
            playerHealth.TakeDamage(healthDecayRate * Time.deltaTime);
        }

        //Visually shows how much health, food and water you have
        healthBar.value = playerHealth.health / maxHealth;
        foodBar.value = food / maxFood;
        waterBar.value = water / maxWater;
    }
}
