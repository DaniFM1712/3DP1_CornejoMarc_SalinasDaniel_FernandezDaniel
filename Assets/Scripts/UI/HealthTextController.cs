using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTextController : UIController
{
    public override void UpdateText(float current, float max)
    {
        gameObject.GetComponent<Text>().text = "HEALTH: " + (int)current + " / " + (int)max;
    }
}
