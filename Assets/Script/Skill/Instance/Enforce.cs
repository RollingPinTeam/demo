using System.Collections.Generic;

public class Enforce : Skill
{
    public override void Execute(Env env)
    {
        env.caster.Enforce();
    }

    public override string GetName()
    {
        return "蓄力";
    }

    public override string GetDesc()
    {
        return "下回合造成双倍伤害";
    }

    public override SkillID GetID()
    {
        return SkillID.Enforce;
    }

    public override SelectType GetSelectType()
    {
        return SelectType.Self;
    }

    public override List<ElementType> GetElements()
    { 
        return new List<ElementType>() { ElementType.Red };
    }
}