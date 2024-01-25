using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PoolTest_Init_Remove : TestBase
{
    public BasePool<PoolBase_Component> enemypool;

    List<Enemy_Base_Object> testEnemyDatas;

    protected override void Awake()
    {
        base.Awake();
        enemypool.InitPool();
        testEnemyDatas = new List<Enemy_Base_Object>(); 
    }
    protected override void Button1(InputAction.CallbackContext context)
    {
        Enemy_Base_Object a =  (Enemy_Base_Object)enemypool.GetPoolObject();
        a.gameObject.SetActive(true);
        testEnemyDatas.Add(a);
        Debug.Log($"{testEnemyDatas.Count} 개 존재");

    }
    protected override void Button2(InputAction.CallbackContext context)
    {
        if (testEnemyDatas.Count > 0)
        {
            testEnemyDatas[0].gameObject.SetActive(false);
            testEnemyDatas.RemoveAt(0);
            Debug.Log($"{testEnemyDatas.Count} 개 남음");
        }
        else 
        {
            Debug.Log("없어");
        }
    }
}
