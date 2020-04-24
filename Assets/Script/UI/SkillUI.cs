using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SkillUI : ListItem<Skill>
{  
    public Image bgImage;
    public TextMeshProUGUI nameText;
    public GameObject parent;
    public GameObject prototype;
    private ListView<ElementUI, ElementType> elementUIs = new ListView<ElementUI, ElementType>();
    private Color unActiveColor;
    private Color activeColor;

    private void Awake()
    {
        elementUIs.Init(parent, prototype);
        unActiveColor = bgImage.color;
        activeColor = Color.green;
        activeColor.a = unActiveColor.a;
    }

    public void Execute(Unit caster)
    {
        var check = ModelMgr.inst.CheckElementEnough(data.GetElements());
        if (check)
        {
        }
        else
        {
            BattleUI.inst.ShowTip("技能所需元素不足");
        }
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
    }
}
