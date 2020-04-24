using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChooseHeroAction : SelectItem<UnitUI, Unit>
{
    public ChooseHeroAction(ListView<UnitUI, Unit> heros, int index) : base(heros, index)
    {
        events.Add(InputCmd.Action1, ctx => BattleUI.inst.ExecuteHeroAction("Q"));
        events.Add(InputCmd.Action2, ctx => BattleUI.inst.ExecuteHeroAction("W"));
        events.Add(InputCmd.Action3, ctx => BattleUI.inst.ExecuteHeroAction("E"));
        events.Add(InputCmd.Action4, ctx => BattleUI.inst.ExecuteHeroAction("R"));
        events.Add(InputCmd.Tab, ctx => BattleUI.inst.ShowComboTip());
    }
}

public class BattleUI : MonoBehaviour
{  
    public static BattleUI inst;
    public GameObject comboTip;
    public TextMeshProUGUI tipText;
    public TextMeshProUGUI introText;
    public ChooseSkillUI chooseSkillUI;
    public GameObject heroParent;
    public GameObject heroPrototype;
    public GameObject enemyParent;
    public GameObject enemyPrototype;
    public GameObject elementParent;
    public GameObject elementPrototype;
    public ListView<UnitUI, Unit> heroUIs { get; private set; } = new ListView<UnitUI, Unit>();
    public ListView<UnitUI, Unit> enemyUIs { get; private set; } = new ListView<UnitUI, Unit>();
    public ListView<ElementUI, ElementType> elementUIs { get; private set; } = new ListView<ElementUI, ElementType>();

    private void Awake()
    {
        comboTip.SetActive(false);
        inst = this;
        heroUIs.Init(heroParent, heroPrototype);
        enemyUIs.Init(enemyParent, enemyPrototype);
        elementUIs.Init(elementParent, elementPrototype);
        UnitCfg.Init();
        InputMgr.inst.Init();
        ModelMgr.inst.Init();

        elementUIs.SetItems(ModelMgr.inst.elements);
        heroUIs.SetItems(ModelMgr.inst.heros);
        enemyUIs.SetItems(ModelMgr.inst.enemys);
        enemyUIs.go.SetActive(false);
    }

    public void StartBattle()
    {
        enemyUIs.go.SetActive(true);
        InputMgr.inst.Push(new ChooseHeroAction(heroUIs, 0));
    }

    private void TurnStart()
    {

    }

    private void TurnEnd()
    {
    }

    public void ExecuteHeroAction(string key)
    {
        heroUIs.currItem.ExecuteAction(key);
    }

    public void ShowSkillInfo(SkillUI skill)
    {
        var root = introText.gameObject.transform.parent.gameObject;
        root.SetActive(skill != null);
        if (skill != null)
        {
            introText.text = skill.data.GetDesc();
        }
    }

    public void ShowComboTip()
    {
        comboTip.SetActive(!comboTip.activeSelf);
    }

    public void ShowTip(string text)
    {
        StartCoroutine(ShowTipCo(text));
    }

    private IEnumerator ShowTipCo(string text)
    {
        tipText.gameObject.SetActive(true);
        tipText.text = text;
        yield return new WaitForSeconds(2);
        tipText.gameObject.SetActive(false);
    }
}
