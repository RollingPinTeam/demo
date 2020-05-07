using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;

public class SelectTarget : SelectItem<UnitUI, Unit>
{
    private SkillUI skill;
    private int excludeIndex;

    public SelectTarget(ListView<UnitUI, Unit> list, SkillUI skill, int index, int excludeIndex = -1) : base(list, index)
    {
        this.skill = skill;
        this.excludeIndex = excludeIndex;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (0 <= excludeIndex && excludeIndex < list.count)
        {
            list.Get(excludeIndex).SetFocusActive(false);
            if (excludeIndex == index)
            {
                var index = list.FindIndex(item => item.focusActive);
                list.SetFocus(index);
            }
        }

        skill.SetFocus(true);
    }

    public override void OnExit()
    {
        base.OnExit();
        skill.SetFocus(false);
    }

    protected override void OnCancel()
    {
        InputMgr.inst.Pop();
    }

    protected override void OnChoose(UnitUI unit)
    {
        InputMgr.inst.Pop();
        skill.Cast(unit.data);
    }
}

public class ComfirmTarget : Interaction
{
    private ListView<UnitUI, Unit> list;
    private List<UnitUI> units;
    private SkillUI skill;

    public ComfirmTarget(ListView<UnitUI, Unit> list, List<UnitUI> units, SkillUI skill)
    {
        this.list = list;
        this.units = units;
        this.skill = skill;
    }

    public override void OnEnter()
    {
        list.SaveStash();
        events.Add(InputCmd.Submit, ctx => 
        {
            InputMgr.inst.Pop();
            skill.Cast();
        });

        events.Add(InputCmd.Cancel, ctx => InputMgr.inst.Pop());
        units.ForEach(item => item.SetFocus(true));
        skill.SetFocus(true);
    }

    public override void OnExit()
    {
        units.ForEach(item => item.SetFocus(false));
        list.ApplyStash();
        skill.SetFocus(false);
    }
}

public class SkillUI : ListItem<Skill>
{  
    public Image bgImage;
    public TextMeshProUGUI nameText;
    public GameObject parent;
    public GameObject prototype;
    private ListView<ElementUI, ElementType> elementUIs = new ListView<ElementUI, ElementType>();
    private Color unActiveColor;
    private Color activeColor;
    private UnitUI owner;
    private Action onFinish;

    private void Awake()
    {
        elementUIs.Init(parent, prototype);
        unActiveColor = bgImage.color;
        activeColor = Color.green;
        activeColor.a = unActiveColor.a;
    }

    public void Execute(UnitUI owner, Action onFinish)
    {
        this.owner = owner;
        this.onFinish = onFinish;
        var cost = data.GetElements();
        var check = ModelMgr.inst.CheckElementEnough(cost);
        if (check)
        {
            SetFocus(true);
            var heros = BattleUI.inst.heroUIs;
            var enemys = BattleUI.inst.enemyUIs;
            var units = new List<UnitUI>();
            
            switch(data.GetSelectType())
            {
                case SelectType.Self: 
                    units.Add(owner);
                    InputMgr.inst.Push(new ComfirmTarget(heros, units, this));
                    break;
                case SelectType.Firend:
                    InputMgr.inst.Push(new SelectTarget(heros, this, heros.currIndex, heros.currIndex));
                    break;
                case SelectType.FirendWithMe:
                    InputMgr.inst.Push(new SelectTarget(heros, this, heros.currIndex));
                    break;
                case SelectType.Enemy:  
                    InputMgr.inst.Push(new SelectTarget(enemys, this, 0));
                    break;
                case SelectType.AllFirend:
                    heros.ForEach(item => units.Add(item));
                    InputMgr.inst.Push(new ComfirmTarget(heros, units, this));
                    break;
                case SelectType.AllEnemy:
                    enemys.ForEach(item => units.Add(item));
                    InputMgr.inst.Push(new ComfirmTarget(heros, units, this));
                    break;
                case SelectType.All:
                    heros.ForEach(item => units.Add(item));
                    enemys.ForEach(item => units.Add(item));
                    InputMgr.inst.Push(new ComfirmTarget(heros, units, this));
                    break;
            }
        }
        else
        {
            BattleUI.inst.ShowTip("技能所需元素不足");
        }
    }

    public void Cast(Unit target = null)
    {
        data.Cast(owner.data, target);
        onFinish();
    }

    public override void Refresh(Skill skill)
    {
        base.Refresh(skill);
        var name = data.GetName();
        var isEmpty = string.IsNullOrEmpty(data.shortKey);
        nameText.text = isEmpty ? name : string.Format("{0}-{1}", data.shortKey, name);
        elementUIs.SetItems(data.GetElements());
    }

    public override void SetFocus(bool active)
    {
        bgImage.color = active ? activeColor : unActiveColor;
        BattleUI.inst.ShowSkillInfo(active ? this : null);
    }
}
