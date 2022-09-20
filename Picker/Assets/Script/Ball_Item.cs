using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Item : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private string itemType;
    [SerializeField] private int bonusBallIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickerBorderObject"))
        {
            if (itemType == "Palet")
            {
                gameManager.PaletReveal();
                gameObject.SetActive(false);
            }
            else
            {
                gameManager.AddBonusBalls(bonusBallIndex);
                gameObject.SetActive(false);
            }
            
            
        }
    }
}
