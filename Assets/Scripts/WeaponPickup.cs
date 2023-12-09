using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public string theGun;
    private bool collected;
    void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !collected)
        {
            PlayerController.Instance.AddGun(theGun);

            Destroy(gameObject);

            collected = true;

            AudioManager.Instance.PlaySFX(5);
        }
    }
}
