using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    public int currentHeal = 5;
    public EnemyController theEC;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void DamageEnemy (int damageAmount)
    {
        currentHeal-= damageAmount;
        if (theEC != null)
        {
            theEC.GetShot();
        }

        if (currentHeal <= 0)
        {
            Destroy(gameObject);
            AudioManager.Instance.PlaySFX(8);
        }
    }

}
