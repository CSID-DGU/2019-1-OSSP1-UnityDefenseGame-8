using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarScript : MonoBehaviour {

    public int maxHealth; //The maximum amount of health that the player can possess
    private Image fillingImage; //The reference to "Health_Bar_Filling" Image component
    private int health;  //The current amount of health of the player
    public Color[] health_bar_color;    // 초록, 노랑, 빨강 헬스바 색상

    void Start () {
        //Get the reference to the filling image
        fillingImage = GetComponentInChildren<Image>();
        //set the health to the maximum
        health = maxHealth;
        //Update the graphics of the Health Bar.
        updateHealthBar();
    }

    //Function to apply damage to the player
    public bool ApplyDamage(int value) {
        // 음수 데미지 허용 안 함
        if (value < 0) value = 0;
        //Apply damage to the player
        health -= value;
        Debug.Log(health);
        //Check if the player has still health and update the Health Bar
        if(health > 0) {
            updateHealthBar();
            return false;
        }

        //In case the player has no health left, set health to zero and return true
        health = 0;
        updateHealthBar();
        return true;
    }

    // test// 혹시?
    
    void Update()
    {
        updateHealthBar();
    }
    

    // 플레이어의 HP를 치료하는 함수
    public void ApplyHealing(int value)
    {
        if (value < 0) value = 0;
        health += value;

        // 최대 HP에 제한
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }
	
    //Function to update the Health Bar Graphic
    void updateHealthBar() {
        //Calculate the percentage (from 0% to 100%) of the current amount of health of the player
        float percentage = health * 1f / maxHealth;
        //Assign the percentage to the fillingAmount variable of the "Health_Bar_Filling"
        fillingImage.fillAmount = percentage;

        // HP가 40% 이상, 20% 이상, 20%미만일 때를 기준으로 각각 HealthBar의 색상 변경
        if(percentage > 0.4f)
        {
            fillingImage.color = health_bar_color[0];
        }
        else if(percentage > 0.2f){
            fillingImage.color = health_bar_color[1];
        }
        else
        {
            fillingImage.color = health_bar_color[2];
        }
    }
}
