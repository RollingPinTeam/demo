using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ListSortType
{
    Horizontal,
    Vertical,
    Grid,
}

public enum MoveDir 
{
    Up,
    Down,
    Left,
    Right,
}

public class ListItem<T> : MonoBehaviour
{
    public bool focusActive { get; private set; } = true;
    public T data { get; private set; }
    public virtual void Refresh(T t) { data = t; }
    public virtual void SetFocus(bool active) { }
    public virtual void SetFocusActive(bool active) { this.focusActive = active; }
}

public class ListView<T, TArgs> where T : ListItem<TArgs>
{
    public int count { get { return items.Count; } }
    public T currItem { get { return items[currIndex]; } }
    public int currIndex { get; private set; } = -1;
    public GameObject go { get; private set; }
    private int rowCount = 1;
    private ListSortType sortType;
    private GameObject prototype;
    private List<T> items = new List<T>();

    public void Init(GameObject parent, GameObject prototype)
    {
        go = parent;
        this.prototype = prototype;

        prototype.SetActive(false);
        var grid = parent.GetComponent<GridLayoutGroup>();
        if (grid != null)
        {
            sortType = ListSortType.Grid;
            rowCount = grid.constraintCount;
        }
        else if (parent.GetComponent<HorizontalLayoutGroup>() != null)
        {
            sortType = ListSortType.Horizontal;
        }
        else if (parent.GetComponent<VerticalLayoutGroup>() != null)
        {
            sortType = ListSortType.Vertical;
        }
    }

    public void SetItem(int index, TArgs t)
    {
        if (0 <= index && index < items.Count)
        {
            items[index].Refresh(t);
        }
    }

    public void SetItems(List<TArgs> argsList)
    {
        for(var i = 0; i < items.Count; ++i)
        {
            GameObject.Destroy(items[i].gameObject);
        }

        items.Clear();
        
        if (argsList == null || argsList.Count == 0)
            return;

        for(var i = 0; i < argsList.Count; ++i) 
        {
            var inst = GameObject.Instantiate<GameObject>(prototype, go.transform);
            inst.SetActive(true);
            var item = inst.GetComponent<T>();
            items.Add(item);
            item.Refresh(argsList[i]);
        }
    }

    public void MoveFocus(MoveDir dir) 
    {
        if (sortType == ListSortType.Horizontal && (dir == MoveDir.Up || dir == MoveDir.Down))
            return;
        
        if (sortType == ListSortType.Vertical && (dir == MoveDir.Left || dir == MoveDir.Right))
            return;

        var offset = GetOffset(dir);
        var index = currIndex + offset;
       
        while(0 <= index && index < items.Count && !items[index].focusActive)
        {
            index += offset;
        }

        if (0 <= index && index < items.Count)
        {
            SetFocus(index);
        }
    }

    public void SetFocus(int index)
    {  
        if (currIndex == index || index < 0 || index >= items.Count)
            return;

        if (0 <= currIndex && currIndex < items.Count)
            items[currIndex].SetFocus(false);

        currIndex = index;
        items[currIndex].SetFocus(true);
    }

    public void LostFocus()
    {
        items.ForEach(item => 
        { 
            item.SetFocus(false);
            item.SetFocusActive(true);
        });

        currIndex = -1;
    }

    private int GetOffset(MoveDir dir)
    {
        switch(dir)
        {
            case MoveDir.Up: return -rowCount;
            case MoveDir.Down: return rowCount;
            case MoveDir.Left: return -1;
            case MoveDir.Right: return 1;
        }

        return 1;
    }

    public T Get(int index) { return items[index]; }
    public T Find(Predicate<T> match) { return items.Find(match); }
    public bool Exist(Predicate<T> match) { return items.Exists(match); }
}