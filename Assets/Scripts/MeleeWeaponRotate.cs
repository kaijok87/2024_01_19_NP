using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���� ������� ȸ���� ������ Ŭ����
/// </summary>
/// 

public class MeleeWeaponRotate : MonoBehaviour
{
    /// <summary>
    /// ȸ�� �ӵ�
    /// </summary>
    [SerializeField]
    private float rotateSpeed;

    /// <summary>
    /// ������ ȸ����
    /// </summary>
    [SerializeField]
    float weaponAngle;

    /// <summary>
    /// ȸ���� ������ �ڸ�ƾ�� ��Ƶ� ����
    /// </summary>
    IEnumerator rotateFunction;

    /// <summary>
    /// �ڷ�ƾ�������� ����� �ݺ��ֱ� ��
    /// </summary>
    WaitForFixedUpdate rotateCycle;

    private void Awake()
    {
        rotateCycle = new WaitForFixedUpdate();

        InitDataSetting();
    }

    /// <summary>
    /// �⺻������ �ʱ�ȭ�� ������ ��Ƶ� �Լ�
    /// </summary>
    public void InitDataSetting() 
    {

        //ȸ���������� ����� �ʱⰪ�� �����Ѵ�.

        //ȸ�������� ��Ƶд�.
        rotateFunction = WeaponRotation();
        StartCoroutine(rotateFunction);
    }


    /// <summary>
    /// ���������� ȸ���ϱ����� ������ �ۼ�
    /// </summary>
    /// <returns>�ݺ� �ֱ�</returns>
    private IEnumerator WeaponRotation()
    {
        weaponAngle = 0.0f;
        Vector3 rotateValue = Vector3.zero;
        do
        {
            if (weaponAngle > 360.0f)
            {
                weaponAngle = 0.0f;
            }
            else 
            {
                weaponAngle += Time.deltaTime * rotateSpeed;
            }
            rotateValue.z = weaponAngle;
            transform.rotation = Quaternion.Euler(rotateValue);

            yield return rotateCycle;
        } while (true);
    }

}
