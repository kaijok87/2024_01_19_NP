using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 근접 무기들의 회전을 관리할 클래스
/// </summary>
/// 

public class MeleeWeaponRotate : MonoBehaviour
{
    /// <summary>
    /// 회전 속도
    /// </summary>
    [SerializeField]
    private float rotateSpeed;

    /// <summary>
    /// 무기의 회전값
    /// </summary>
    [SerializeField]
    float weaponAngle;

    /// <summary>
    /// 회전을 진행할 코르틴을 담아둘 변수
    /// </summary>
    IEnumerator rotateFunction;

    /// <summary>
    /// 코루틴루프에서 사용할 반복주기 값
    /// </summary>
    WaitForFixedUpdate rotateCycle;

    private void Awake()
    {
        rotateCycle = new WaitForFixedUpdate();

        InitDataSetting();
    }

    /// <summary>
    /// 기본적으로 초기화할 데이터 모아둘 함수
    /// </summary>
    public void InitDataSetting() 
    {

        //회전로직에서 사용할 초기값을 설정한다.

        //회전로직을 담아둔다.
        rotateFunction = WeaponRotation();
        StartCoroutine(rotateFunction);
    }


    /// <summary>
    /// 근접무기의 회전하기위한 로직을 작성
    /// </summary>
    /// <returns>반복 주기</returns>
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
