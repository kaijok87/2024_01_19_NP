using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasePool<T> :MonoBehaviour where T : PoolBase_Component
{
    /// <summary>
    /// �������� ���� ���̽� Ŭ����
    /// </summary>
    [SerializeField]
    T _originComponent;

    [SerializeField]
    int init_capasity = 2;

    T[] _pool_datas;
    
    Queue<T> _queue;

    protected Transform _pool_parent;

    /// <summary>
    /// Ǯ�� �ʱ�ȭ �ϱ����� �Լ� 
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
    /// Ǯ���� �����͸� ���������� ����
    /// </summary>
    /// <returns>ť�� ������ ������ ���������� ��ȯ</returns>
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
    /// ť�� �����Ͱ� ����������� Ȯ���ϱ����ѷ���
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
    /// Ǯ������ ������ ������ �ʱ�ȭ �۾��� �迭�� ť�� ��Ƶδ��۾�
    /// </summary>
    /// <param name="startIndex"></param>
    /// <param name="endIndex"></param>
    /// <param name="newArray"></param>
    private void GeneratePoolData(int startIndex, int endIndex, T[] newArray)
    {
        /*
         Ǯ�� �����Ǵ� ��ġ�� ��Ȱ��ȭ �������� ����Ƽ���� OnDIsable �Լ��� �������� �������� üũ�ϱ����� ���� �߰�
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
            poolObject = Instantiate(_originComponent,_pool_parent); //�����ϰԵǸ� Awake �� OnEnable �Լ��� �ڵ����� �����̵ȴ�.
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
