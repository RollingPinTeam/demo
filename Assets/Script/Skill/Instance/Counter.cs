using System.Collections.Generic;

public class Counter : Skill
{
    public override void Execute(Env env)
    {
        env.caster.Counter();
    }

    public override string GetName()
    {
        return "反击";
    }

    public override string GetDesc()
    {
        return "反击物理攻击";
    }

    public override SkillID GetID()
    {
        return SkillID.Counter;
    }

    public override SelectType GetSelectType()
    {
        return SelectType.Self;
    }

    public override List<ElementType> GetElements()
    { 
        return new List<ElementType>() { ElementType.Yellow };
    }
}