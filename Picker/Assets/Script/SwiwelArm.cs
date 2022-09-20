using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwiwelArm : MonoBehaviour
{
    private bool Turn;
    [SerializeField] private float turnValue;
    public void StartTurning()
    {
        Turn = true;
    }

    void Update()
    {
        if(Turn)
            transform.Rotate(0,0,turnValue,Space.Self);
    }
}
