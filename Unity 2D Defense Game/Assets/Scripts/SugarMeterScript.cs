using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SugarMeterScript : MonoBehaviour {

    private Text sugarMeter; //Reference to the Text component // 원래는 private
    private int sugar; //Amount of sugar that the player possesses // 임의로 값을 주었음 // 원래는 private

	void Start () {
        //Get the reference to the Sugar_Meter_Text
        sugarMeter = GetComponentInChildren<Text>();
        //Update the Sugar Meter graphic 
        updateSugarMeter(); // 맨처음에 슈거 미터를 처음에 우리가 지정한 값으로 업데이트 해줌
    }
	
    //Function to increase or decrease the amount of sugar
    public void ChangeSugar(int value) { // 슈거를 계속해서 업데이트한다
        //Increase (or decrease, if value is negative) the amount of sugar
        sugar += value;
        //Check if the amount of suguar is negative, is so set it to zero
        if(sugar < 0) {
            sugar = 0;
        }
        //Update the Sugar Meter graphic 
        updateSugarMeter();
    }

    //Function to return the amount of sugur, since it is a private variable
    public int getSugarAmount() {
        return sugar;
    }

    //Function to update the Sugar Meter graphic 
    void updateSugarMeter() {
        //Assign the amount of sugar converted to a string to the text in the Sugar Meter
        sugarMeter.text = sugar.ToString();
    }
}
