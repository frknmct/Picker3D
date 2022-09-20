using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teller : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickerBorderObject"))
        {
            gameManager.ReachedBorder();
        }
    }
}
