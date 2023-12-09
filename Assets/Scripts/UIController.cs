using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    public Slider healthSlider;
    public TMP_Text healthText, ammoText;
    public Image damageEffect;
    public float damageAlpha = .25f;
    public float damageFadeSpeed = 2f;
    public GameObject pauseScreen;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if(damageEffect.color.a != 0)
        {
            damageEffect.color = new Color(damageEffect.color.r, damageEffect.color.g, damageEffect.color.b, Mathf.MoveTowards(damageEffect.color.a, 0f, damageFadeSpeed * Time.deltaTime));
        }
    }

    public void Showdamage()
    {
        damageEffect.color = new Color(damageEffect.color.r, damageEffect.color.g, damageEffect.color.b, damageAlpha);
    }
}
