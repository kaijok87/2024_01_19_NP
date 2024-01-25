using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasePool<T> :MonoBehaviour where T : PoolBase_Component
{
    /// <summary>
    /// 공용으로 사용될 베이스 클래스
    /// </summary>
    [SerializeField]
    T _originComponent;

    [SerializeField]
    int init_capasity = 2;

    T[] _pool_datas;
    
    Queue<T> _queue;

    protected Transform _pool_parent;

    /// <summary>
    /// 풀을 초기화 하기위한 함수 
    /// </summary>
    public void InitPool()
    {
        if (_pool_datas == null) 
        {
            SetParentTransform();
            _pool_datas = new T[init_capasity];
            _queue = new Queue<T>(init_capasity);
            GeneratePoolData(0,init_capasity,_pool_datas);
        }
        else 
        {
            int dataLength = _pool_datas.Length;
            for (int i = 0; i < dataLength; i++)
            {
                _pool_datas[i].gameObject.SetActive(false);
            }
        }
    }

    protected virtual void SetParentTransform() 
    {

    }

    /// <summary>
    /// 풀에서 데이터를 꺼내기위한 로직
    /// </summary>
    /// <returns>큐에 적제된 데이터 순차적으로 반환</returns>
    public T GetPoolObject() 
    {
        T returnObject = null;
        if (_queue.Count > 0)
        {
            returnObject = _queue.Dequeue();
        }
        else 
        {
            PoolExpansion();
            returnObject = GetPoolObject();
        }

        return returnObject;
    }
    /// <summary>
    /// 큐의 데이터가 부족해질경우 확장하기위한로직
    /// </summary>
    private void PoolExpansion() 
    {
        int newCapasity = init_capasity * 2;
        T[] expansionDatas = new T[newCapasity];
        for (int i = 0; i < init_capasity; i++)
        {
            expansionDatas[i] = _pool_datas[i];
        }
        GeneratePoolData(init_capasity,newCapasity,expansionDatas);
        init_capasity = newCapasity;
        _pool_datas = expansionDatas;
    }

    /// <summary>
    /// 풀에대한 정보를 생성및 초기화 작업후 배열및 큐에 담아두는작업
    /// </summary>
    /// <param name="startIndex"></param>
    /// <param name="endIndex"></param>
    /// <param name="newArray"></param>
    private void GeneratePoolData(int startIndex, int endIndex, T[] newArray)
    {
        /*
         풀이 생성되는 위치가 비활성화 되있으면 유니티에서 OnDIsable 함수를 실행하지 않음으로 체크하기위한 로직 추가
         */
        Transform checkParent = _pool_parent;
        bool checkActiveObject = true;
        while (checkParent != null)  
        {
            checkActiveObject = checkParent.gameObject.activeSelf;
            if (!checkActiveObject) break;
            checkParent = checkParent.parent;
        }

        PoolBase_Component poolObject;

        for (int i = startIndex; i < endIndex; i++)
        {
            poolObject = Instantiate(_originComponent,_pool_parent); //생성하게되면 Awake 와 OnEnable 함수가 자동으로 실행이된다.
            poolObject.name = $"{typeof(T).Name}_{i}";
            T data = poolObject.GetComponent<T>();
            data.onDisable += () => { 
                _queue.Enqueue(data);
            };
            data.ParentTransform = _pool_parent;
            data.gameObject.SetActive(false);
            newArray[i] = data;
            if (!checkActiveObject) 
            {
                _queue.Enqueue(data);
            }
        }
    }
}
