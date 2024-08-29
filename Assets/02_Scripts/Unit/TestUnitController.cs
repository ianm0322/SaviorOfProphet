using Glorynuts.ProphetsArc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestUnitController : MonoBehaviour
{
    InputAction_GamePlay inputAction;
    InputAction moveAction;

    TestUnit controlledObject;

    private void Awake()
    {
        inputAction = new InputAction_GamePlay();
        moveAction = inputAction.Player.Move;
        controlledObject = this.gameObject.GetComponent<TestUnit>();
    }

    private void OnEnable()
    {
        moveAction.started += OnMoveButtonStart;
        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.started -= OnMoveButtonStart;
        moveAction.Disable();
    }

    private void OnMoveButtonStart(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();

        if(value.x > 0)
        {
            controlledObject.MoveRight();
        }
        else if(value.x < 0)
        {
            controlledObject.MoveLeft();
        }
    }

}
