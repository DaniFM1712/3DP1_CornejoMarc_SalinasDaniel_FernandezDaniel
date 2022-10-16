using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorTextController : UIController
{
    public override void UpdateText(float current, float max)
    {
        gameObject.GetComponent<Text>().text = "ARMOR: " + (int)current + " / " + (int)max;
    }

}
