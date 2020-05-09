using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using CmdAction = System.Action<UnityEngine.InputSystem.InputAction.CallbackContext>;

public enum InputCmd
{
    Move,
    Up,
    Down,
    Left,
    Right,
    Submit,
    Cancel,
    Tab,
    Action1,
    Action2,
    Action3,
    Action4,
}

public class Interaction
{
    protected Dictionary<InputCmd, CmdAction> events = new Dictionary<InputCmd, CmdAction>();

    public void Invoke(InputCmd cmd, InputAction.CallbackContext ctx)
    {
        if (events.ContainsKey(cmd))
        {
            events[cmd].Invoke(ctx);
        }
    }

    public virtual void OnMove(Vector2 vector) {}
    public virtual void OnEnter() {}
    public virtual void OnExit() {}
}

public class ListArgs
{
    public int index = 0;
}

public class SelectItem<TItem, TArgs> : Interaction where TItem : ListItem<TArgs>
{
    protected ListView<TItem, TArgs> list;
    protected int index;

    public SelectItem(ListView<TItem, TArgs> list, int startIndex)
    {
        this.list = list;
        index = startIndex;
    }

    private void MoveFocus(MoveDir dir)
    {
        var lastIndex = list.currIndex;
        list.MoveFocus(dir);
        if (lastIndex != list.currIndex)
        {
            OnSelect(list.currItem);
        }
    }

    public override void OnEnter()
    {
        events.Add(InputCmd.Left, ctx => MoveFocus(MoveDir.Left));
        events.Add(InputCmd.Right, ctx => MoveFocus(MoveDir.Right));
        events.Add(InputCmd.Up, ctx => MoveFocus(MoveDir.Up));
        events.Add(InputCmd.Down, ctx => MoveFocus(MoveDir.Down));
        events.Add(InputCmd.Submit, ctx => OnChoose(list.currItem));
        events.Add(InputCmd.Cancel, ctx => OnCancel());
        list.SaveStash();
        list.SetFocus(index);
    }

    public override void OnExit() { list.ApplyStash(); }
    protected virtual void OnCancel() {}
    protected virtual void OnSelect(TItem t) {}
    protected virtual void OnChoose(TItem t) {}
}