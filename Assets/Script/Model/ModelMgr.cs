using System.Collections.Generic;

public class ModelMgr
{
    public static readonly ModelMgr inst = new ModelMgr();
    public List<Unit> heros = new List<Unit>();
    public List<Unit> enemys = new List<Unit>();
    public List<ElementType> elements = new List<ElementType>();

    public void Init()
    {
        elements.Add(ElementType.Blue);
        elements.Add(ElementType.Yellow);
        elements.Add(ElementType.Red);
        heros.Add(new Unit(UnitCfg.Get(1001), PosType.Front));
        heros.Add(new Unit(UnitCfg.Get(1002), PosType.Front));
        heros.Add(new Unit(UnitCfg.Get(1003), PosType.Back));
        heros.Add(new Unit(UnitCfg.Get(1004), PosType.Back));
        enemys.Add(new Unit(UnitCfg.Get(2001), PosType.Front));
        enemys.Add(new Unit(UnitCfg.Get(2002), PosType.Back));
    }

    public void AddHeroSkill(int index, SkillID skillID)
    {
        heros[index].AddSkill(skillID);
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