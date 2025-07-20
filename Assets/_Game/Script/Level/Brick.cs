using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : ColorObject
{
    [HideInInspector] public Stage stage;

    public override void OnDespawn()
    {
        Pools.Instance.Despawn(this);
    }
}
