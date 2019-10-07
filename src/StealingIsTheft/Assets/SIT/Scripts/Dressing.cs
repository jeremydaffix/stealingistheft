using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dressing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enter dressing");

        if (collision.gameObject.name == "Player")
        {
            if (PlayerController.PlayerInstance.HasShoes && PlayerController.PlayerInstance.HasUnderwear && PlayerController.PlayerInstance.HasPants &&
            PlayerController.PlayerInstance.HasTshirt && PlayerController.PlayerInstance.HasJacket && PlayerController.PlayerInstance.HasHat)
            {
                Debug.Log("dress");

                if (PlayerController.PlayerInstance.HasPincers)
                {
                    PlayerController.PlayerInstance.IsWearingAntiTheft = false;
                    Debug.Log("no more antitheft");
                }

                PlayerController.PlayerInstance.IsNaked = false;

                // some fx
                // change skin

                Debug.LogWarning("FULLY DRESSED!");

                Destroy(gameObject);
            }
        }
    }


}
