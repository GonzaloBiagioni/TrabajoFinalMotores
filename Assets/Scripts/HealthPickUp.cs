using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public int healAmmount;
    private bool isCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isCollected)
        {
            PlayerHealthController.Instance.healPlayer (healAmmount);
            Destroy (gameObject);
            isCollected = true;
            AudioManager.Instance.PlaySFX(3);
        }
    }
}
