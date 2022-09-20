using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftSituation : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Animator barrierField;

    public void RemoveBarrier()
    {
        barrierField.Play("RemoveBarrier");
    }

    public void Finish()
    {
        gameManager.isPickerMoving = true;
    }
}
