using System;
using Unity.Mathematics;
using UnityEngine;

public class PoolBase_Component : MonoBehaviour
{
    public Action onDisable;

    protected Transform parentTransform;
    public Transform ParentTransform 
    {
        set { parentTransform = value; }
    }
    protected virtual void OnDisable() 
    {
        onDisable?.Invoke();
    }
    
}
