using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTextScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // text의 내용을 지운다.
        GetComponent<Text>().text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
