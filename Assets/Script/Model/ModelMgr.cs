using System.Collections.Generic;

public class ModelMgr
{
    public static readonly ModelMgr inst = new ModelMgr();
    public List<Unit> heros = new List<Unit>();
    public List<Unit> enemys = new List<Unit>();
    public List<ElementType> elements = new List<ElementType>();
    private int autoId = 0;

    public void Init()
    {
        elements.Add(ElementType.Blue);
        elements.Add(ElementType.Yellow);
        elements.Add(ElementType.Red);
        heros.Add(CreateUnit(1001, PosType.Front));
        heros.Add(CreateUnit(1002, PosType.Front));
        heros.Add(CreateUnit(1003, PosType.Back));
        heros.Add(CreateUnit(1004, PosType.Back));
        enemys.Add(CreateUnit(2001, PosType.Front));
        enemys.Add(CreateUnit(2002, PosType.Back));

        AddHeroSkill(0, SkillID.PhysicDamage);
        AddHeroSkill(0, SkillID.Change);
        AddHeroSkill(0, SkillID.IceDamage);
        AddHeroSkill(0, SkillID.AllIceDamage);
        AddHeroSkill(1, SkillID.PhysicDamage);
        AddHeroSkill(1, SkillID.Counter);
        AddHeroSkill(1, SkillID.Defense);
        AddHeroSkill(1, SkillID.WindDamage);
        AddHeroSkill(2, SkillID.PhysicDamage);
        AddHeroSkill(2, SkillID.Cover);
        AddHeroSkill(2, SkillID.Dodge);
        AddHeroSkill(2, SkillID.FireDamage);
        AddHeroSkill(3, SkillID.PhysicDamage);
        AddHeroSkill(3, SkillID.Enforce);
        AddHeroSkill(3, SkillID.AllFireDamage);
        AddHeroSkill(3, SkillID.AllWindDamage);
    }

    private Unit CreateUnit(int id, PosType pos)
    {
        autoId += 1;
        return new Unit(autoId, UnitCfg.Get(id), pos);
    }

    public void AddHeroSkill(int index, SkillID skillID)
    {
        heros[index].AddSkill(skillID);
    }

    public void CostElement(List<ElementType> cost)
    {
        if (cost == null || cost.Count <= 0)
            return;
            
        cost.ForEach(item => elements.Remove(item));
    }

    public bool CheckElementEnough(List<ElementType> cost)
    {
        if (cost == null || cost.Count <= 0)
            return true;

        if (elements.Count <= 0)
            return false;
        
        var excludes = new List<int>();
        for(var i = 0; i < cost.Count; ++i)
        {
            var exist = false;
            for(var j = 0; j < elements.Count; ++j)
            {
                if (!excludes.Contains(j) && elements[j] == cost[i])
                {
                    excludes.Add(j);
                    exist = true;
                    break;
                }
            }

            if (!exist) return false;
        }

        return true;
    }
}