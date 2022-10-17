using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConsumableAsset : ScriptableObject
{
    // Start is called before the first frame update
    virtual public bool consume(GameObject consumer) { return true; }

}

