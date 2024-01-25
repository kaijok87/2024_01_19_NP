using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Base_Pool : BasePool<PoolBase_Component>
{
    protected override void SetParentTransform()
    {
        _pool_parent = transform;
    }
}
