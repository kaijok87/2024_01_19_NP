using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestBase : MonoBehaviour
{
    protected TestInput inputSystem;

    protected virtual void Awake()
    {
        inputSystem = new();
    }
    protected virtual void OnEnable()
    {
        inputSystem.TestButton.Enable();
        inputSystem.TestButton._1.performed += Button1;
        inputSystem.TestButton._2.performed += Button2;
        inputSystem.TestButton._3.performed += Button3;
        inputSystem.TestButton._4.performed += Button4;
        inputSystem.TestButton._5.performed += Button5;
        inputSystem.TestButton.LeftClick.performed += LeftClick;
        inputSystem.TestButton.RightClick.performed += RightClick;
    }


    protected virtual void OnDisable()
    {
        inputSystem.TestButton.RightClick.performed -= RightClick;
        inputSystem.TestButton.LeftClick.performed -= LeftClick;
        inputSystem.TestButton._5.performed -= Button5;
        inputSystem.TestButton._4.performed -= Button4;
        inputSystem.TestButton._3.performed -= Button3;
        inputSystem.TestButton._2.performed -= Button2;
        inputSystem.TestButton._1.performed -= Button1;
        inputSystem.TestButton.Disable();
    }
    protected virtual void RightClick(InputAction.CallbackContext context)
    {
    }

    protected virtual void LeftClick(InputAction.CallbackContext context)
    {
    }

    protected virtual void Button5(InputAction.CallbackContext context)
    {
    }

    protected virtual void Button4(InputAction.CallbackContext context)
    {
    }

    protected virtual void Button3(InputAction.CallbackContext context)
    {
    }

    protected virtual void Button2(InputAction.CallbackContext context)
    {
    }
    protected virtual void Button1(InputAction.CallbackContext context)
    {
    }
}
