using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class ChooseSkill : SelectItem<SkillUI, Skill>
{
    private ChooseSkillUI chooseSkillUI;

    public ChooseSkill(
        ListView<SkillUI, Skill> skillUIs, 
        int startIndex, 
        ChooseSkillUI chooseSkillUI) : base(skillUIs, startIndex)
    {
        this.chooseSkillUI = chooseSkillUI;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        events.Add(InputCmd.Tab, ctx => BattleUI.inst.ShowComboTip());
        BattleUI.inst.heroUIs.SaveStash();
        BattleUI.inst.heroUIs.SetFocus(0);
        chooseSkillUI.ShowUnitInfo(BattleUI.inst.heroUIs.currItem);
    }

    public override void OnExit()
    {
        base.OnExit();
        BattleUI.inst.heroUIs.ApplyStash();
        BattleUI.inst.ShowSkillInfo(null);
        chooseSkillUI.gameObject.SetActive(false);
        BattleUI.inst.StartBattle();
    }

    protected override void OnChoose(SkillUI skill)
    {
        var heroUIs = BattleUI.inst.heroUIs;
        var hero = ModelMgr.inst.heros[heroUIs.currIndex];
        hero.AddSkill(skill.data.GetID());
        heroUIs.SetItem(heroUIs.currIndex, hero);
        
        if (hero.IsFullSkill())
        {
            if (heroUIs.currIndex < heroUIs.count - 1)
            {
                BattleUI.inst.heroUIs.MoveFocus(MoveDir.Right);
            }
            else
            {
                InputMgr.inst.Pop();
            }
        }
    }
}

public class ChooseSkillUI : MonoBehaviour
{
    public TextMeshProUGUI tipText;
    public GameObject parent;
    public GameObject prototype;
    private ListView<SkillUI, Skill> skillUIs = new ListView<SkillUI, Skill>();

    private void Awake()
    {   
        var args = new List<Skill>();
        skillUIs.Init(parent, prototype);
        var ids = Enum.GetValues(typeof(SkillID));
        
        foreach(var id in ids)
        {
            args.Add(Skill.Create((SkillID)id));
        }

        skillUIs.SetItems(args);
        BattleUI.inst.StartBattle();
        InputMgr.inst.Push(new ChooseSkill(skillUIs, 0, this));
        InputMgr.inst.Pop();
    }

    public void ShowUnitInfo(UnitUI unit)
    {  
        var text = string.Format("请为\"{0}\"选择合适的技能组合", unit.nameText.text);
        tipText.text = unit != null ? text : string.Empty;
    }
}
