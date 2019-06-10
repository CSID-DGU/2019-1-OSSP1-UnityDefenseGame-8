using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class TradeCupcakeTowers : MonoBehaviour, IPointerClickHandler
{
    protected static SugarMeterScript sugarMeter;
    protected static CupcakeTowerScript currentActiveTower;

    // Start is called before the first frame update
    void Start()
    {
        if(sugarMeter == null)
        {
            sugarMeter = FindObjectOfType<SugarMeterScript>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SetActiveTower(CupcakeTowerScript cupcakeTower)
    {
        currentActiveTower = cupcakeTower;
    }

    public abstract void OnPointerClick(PointerEventData eventData);
}
