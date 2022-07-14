using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCustomersRandomly : MonoBehaviour
{
    float updateTimer = 0.0f;
    float updateTimerMax = 5.0f;

    private void Update()
    {
        if (updateTimer < updateTimerMax)
        {
            updateTimer += Time.deltaTime;
        }
        else
        {
            if (Main.CustomerList.Count > 23)
                return;
            updateTimer = 0.0f;
            updateTimerMax = Random.Range(7.5f, 12.5f);
            Main.SpawnCustomer();
        }
            
    }
}
