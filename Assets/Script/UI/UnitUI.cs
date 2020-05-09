using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Text;

public class UnitUI : ListItem<Unit>
{
    public Image bgImage;
    public TextMeshProUGUI buffText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI defText;
    public TextMeshProUGUI posText;
    public GameObject skillPanel;
    public GameObject skillPrefab;
    private ListView<SkillUI, Skill> skillUIs = new ListView<SkillUI, Skill>();
    private Color unActiveColor;
    private Color activeColor;

    private void Awake()
    {
        skillUIs.Init(skillPanel, skillPrefab);
        defText.gameObject.SetActive(false);
        unActiveColor = bgImage.color;
        activeColor = Color.green;
        activeColor.a = unActiveColor.a;
    }

    public override void Refresh(Unit unit)
    {
        base.Refresh(unit);
        nameText.text = unit.name;
        hpText.text = "Hp:" + unit.hp;
        atkText.text = "Atk:" + unit.atk;
        posText.text = unit.pos == PosType.Front ? "前排" : "后排";
        skillUIs.SetItems(unit.skills);
        SetFocusActive(unit.ap > 0);
        var buffStr = new StringBuilder();
        if (unit.ap <= 0) { buffStr.Append("，行动结束"); }
        if (unit.isDodge) { buffStr.Append("，闪避Up"); }
        if (unit.isDefense) { buffStr.Append("，防御Up"); }
        if (unit.isCounter) { buffStr.Append("，反击"); }
        if (unit.isEnforce) { buffStr.Append("，双倍伤害"); }
        if (unit.coverTarget != null) { buffStr.AppendFormat("，掩护{0}", unit.coverTarget.name); }
        if (buffStr.Length > 0) { buffStr.Remove(0, 1); }
        buffText.text = buffStr.ToString();
    }

    public override void SetFocus(bool active)
    {
        bgImage.color = active ? activeColor : unActiveColor;
    }

    public void ExecuteAction(string key, Action onFinish)
    {
        var skill = skillUIs.Find(item => item.data.shortKey == key);
        skill.Execute(this, onFinish);
    }
}
