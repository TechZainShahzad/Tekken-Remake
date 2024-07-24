using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P1_power : MonoBehaviour
{
    public Slider slider_1; 
    public Text textUI_1; 
    public GameObject KO_text;

    public AudioSource KO;

    int power_1;
    bool check_1 = false;

    public Slider slider_2; 
    public Text textUI_2; 

    int power_2;
    bool check_2 = false;

    // Start is called before the first frame update
    void Start()  
    {
          textUI_1.text = MainScreen.instance.textFromInputField1;
          textUI_2.text = MainScreen.instance.textFromInputField2;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        power_1 = P1.instance.getBeatCount();
        power_2 = P2.instance.getBeatCount();

        IncrementSlider_1();
        IncrementSlider_2();
    }

    void IncrementSlider_1()
    {
        if (slider_1 != null)
        {
            if (check_1 == false)
            {
                
                slider_1.value = power_1;

                
                slider_1.value = Mathf.Min(slider_1.value, slider_1.maxValue);

                if (slider_1.value > 9)
                {
                    KO_text.SetActive(true);
                    KO.Play();
                    check_1 = true;
                }
            }
        }
    }

    void IncrementSlider_2()
    {
        if (slider_2 != null)
        {
            if (check_2 == false)
            {
                
                slider_2.value = power_2;

                
                slider_2.value = Mathf.Min(slider_2.value, slider_2.maxValue);

                if (slider_2.value > 9)
                {
                    KO_text.SetActive(true);
                    KO.Play();
                    check_2 = true;
                }
            }
        }   
    }
}
