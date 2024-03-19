using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellMono : MonoBehaviour
{
    protected Transform Target;

    public void SetTarget(Transform target)
    {
        Target = target;
    }
}
