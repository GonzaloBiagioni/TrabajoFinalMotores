using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    private bool collected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !collected)
        {
            PlayerController.Instance.activeGun.GetAmmo();

            Destroy(gameObject);

            collected = true;

            AudioManager.Instance.PlaySFX(2);
        }
    }
}
