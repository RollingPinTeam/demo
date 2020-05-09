using System.Collections.Generic;
using UnityEngine;

public enum PosType
{
    Front,
    Back,
}

public class Unit 
{
    public int id { get; private set; }
    public string name { get; private set; }
    public int hp { get; private set; }
    public int maxHp { get; private set; }
    public int atk { get; private set; }
    public int def { get; private set; }
    public int ap { get; private set; }
    public PosType pos { get; private set; }
    public bool isDodge { get; private set; }
    public bool isDefense { get; private set; }
    public bool isCounter { get; private set; }
    public bool isEnforce { get; private set; }
    public Unit coverTarget { get; private set; }
    public Unit coveredTarget { get; private set; }
    public List<Skill> skills { get; private set; } = new List<Skill>();

    public Unit(int id, UnitCfg cfg, PosType posType)
    {
        this.id = id;
        name = cfg.name;
        maxHp = cfg.hp;
        hp = cfg.hp;
        atk = cfg.atk;
        pos = posType;
    }

    public void TurnStart()
    {
        ap = 1;
        isDodge = false;
        isDefense = false;
        isCounter = false;
        if (coverTarget != null) 
        { 
            coverTarget.coveredTarget = null; 
            coverTarget = null;
        }
    }

    public void TurnEnd()
    {
        isEnforce = false;
    }

    public void Cover(Unit target)
    {
        coverTarget = target;
        target.coveredTarget = this;
    }

    public void AddSkill(SkillID id)
    {
        var skill = Skill.Create(id);
        skill.SetShortKey(GetKey(skills.Count));
        skills.Add(skill);
    }

    public void AddDamageType(DamageType type)
    {

    }

    private string GetKey(int index)
    {
        switch(index)
        {
            case 0: return "Q";
            case 1: return "W";
            case 2: return "E";
            case 3: return "R";
        }   

        return string.Empty;
    }

    public void DoAction() { ap -= 1; }
    public void Dodge() { isDodge = true; }
    public void Defense() { isDefense = true; }
    public void Counter() { isCounter = true; }
    public void Enforce() { isEnforce = true; }
    public void ModifyHp(int value) { hp = Mathf.Clamp(hp + value, 0, maxHp); }
    public void ModifyAP(int value) { ap += value; }
    public bool IsFullSkill() { return skills.Count == 4; }
}