using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float bounceFource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            PlayerController.Instance.Bounce(bounceFource);
            AudioManager.Instance.PlaySFX(6);
        }
    }

}
