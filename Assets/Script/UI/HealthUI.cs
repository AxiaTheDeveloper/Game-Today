using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField]private PlayerIdentity playerIdentity;
    [SerializeField]private Slider slider;
    [SerializeField]private Image sliderImage;
    private void Start() {
        playerIdentity.OnChangeHealth += playerIdentity_OnChangeHealth;
    }

    private void playerIdentity_OnChangeHealth(object sender, PlayerIdentity.OnChangeHealthEventArgs e)
    {
        slider.value = e.playerHealthNormalized;
        if(e.playerHealthNormalized >= 0.6f){
            sliderImage.color = new Color32(50,255,50,255);

        }
        else if(e.playerHealthNormalized >= 0.3f){
            sliderImage.color = new Color32(255,170,92,255);

        }
        else{
            sliderImage.color = new Color32(255,26,0,255);
        }
    }
}
