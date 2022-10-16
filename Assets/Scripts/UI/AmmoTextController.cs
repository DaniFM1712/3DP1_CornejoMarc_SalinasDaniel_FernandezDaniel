using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoTextController : UIController
{
    public override void UpdateText(float current, float max)
    {
        gameObject.GetComponent<Text>().text = "AMMO: " + (int)current + " / " + (int)max;
    }

}