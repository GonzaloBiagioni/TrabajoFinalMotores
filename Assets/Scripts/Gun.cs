using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public bool canAutoFire;
    public float fireRate;
    public int currentAmmo,pickupAmount;
    public float zoomAmount;
    public string gunName;



    [HideInInspector]
    public float fireCounter;

    void Start()
    {
        
    }

    
    void Update()
    {
        if(fireCounter > 0)
        {
            fireCounter -= Time.deltaTime;
        }
    }
    public void GetAmmo()
    {
        currentAmmo += pickupAmount;

        UIController.Instance.ammoText.text = "Balas: " + currentAmmo;
    }    
}
