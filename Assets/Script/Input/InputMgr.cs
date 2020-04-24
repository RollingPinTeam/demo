using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class InputMgr
{  
    public static readonly InputMgr inst = new InputMgr();
    private InputControls input;
    private Stack<Interaction> interactions = new Stack<Interaction>();
    
    public void Init()
    {
        input = new InputControls();
        input.Enable();
        input.UI.Navigate.performed += Navigate;
        input.UI.Submit.performed += ctx => Invoke(InputCmd.Submit, ctx);
        input.UI.Cancel.performed += ctx => Invoke(InputCmd.Cancel, ctx);
        input.UI.Tab.performed += ctx => Invoke(InputCmd.Tab, ctx);
        input.UI.Action1.performed += ctx => Invoke(InputCmd.Action1, ctx);
        input.UI.Action2.performed += ctx => Invoke(InputCmd.Action2, ctx);
        input.UI.Action3.performed += ctx => Invoke(InputCmd.Action3, ctx);
        input.UI.Action4.performed += ctx => Invoke(InputCmd.Action4, ctx);
    }

    public void Push(Interaction interaction)
    {   
        interactions.Push(interaction);
    }

    public void Pop()
    {
        var item = interactions.Pop();
        item.OnExit();
    }

    private void Navigate(InputAction.CallbackContext ctx)
    {
        if (interactions.Count <= 0)
            return;

        var vec = ctx.ReadValue<Vector2>();
        if (vec == Vector2.up)
        {
            Invoke(InputCmd.Up, ctx);
        }
        else if (vec == Vector2.down)
        {
            Invoke(InputCmd.Down, ctx);
        }
        else if (vec == Vector2.left)
        {
            Invoke(InputCmd.Left, ctx);
        }
        else if (vec == Vector2.right)
        {
            Invoke(InputCmd.Right, ctx);
        }
    }

    private void Invoke(InputCmd cmd, InputAction.CallbackContext ctx)
    {
        if (interactions.Count > 0)
        {
            var item = interactions.Peek();
            item.Invoke(cmd, ctx);
        }
    }
}
