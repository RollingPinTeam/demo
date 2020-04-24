using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UnitUI : ListItem<Unit>
{
    public Image bgImage;
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
        nameText.text = unit.name;
        hpText.text = "Hp:" + unit.hp;
        atkText.text = "Atk:" + unit.atk;
        posText.text = unit.pos == PosType.Front ? "前排" : "后排";
        skillUIs.SetItems(unit.skills);
    }

    public override void SetFocus(bool active)
    {
        bgImage.color = active ? activeColor : unActiveColor;
    }

    public void ExecuteAction(string key)
    {
        var skill = skillUIs.Find(item => item.data.shortKey == key);
        skill.Execute(data);
    }
}
