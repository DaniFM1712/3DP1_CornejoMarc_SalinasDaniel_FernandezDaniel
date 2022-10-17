using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsTextController : UIController
{
    public override void UpdateText(float current, float max)
    {
        gameObject.GetComponent<Text>().text = "POINTS: "+(int)current;
    }

}
