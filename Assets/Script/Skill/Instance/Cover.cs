using System.Collections.Generic;

public class Cover : Skill
{
    public override void Execute(Env env)
    {
        env.caster.Cover(env.target);
    }

    public override string GetName()
    {
        return "掩护";
    }

    public override string GetDesc()
    {
        return "承受一个队友收到的伤害,自身收到伤害减少";
    }

    public override SkillID GetID()
    {
        return SkillID.Cover;
    }

    public override SelectType GetSelectType()
    {
        return SelectType.Firend;
    }

    public override List<ElementType> GetElements()
    { 
        return new List<ElementType>() { ElementType.Yellow };
    }
}