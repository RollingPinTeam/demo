using System.Collections.Generic;

public class Dodge : Skill
{
    public override void Execute(Env env)
    {
        env.caster.Dodge();
    }

    public override string GetName()
    {
        return "闪避";
    }

    public override SkillID GetID()
    {
        return SkillID.Dodge;
    }

    public override string GetDesc()
    {
        return "提高当前回合的回避力";
    }

    public override SelectType GetSelectType()
    {
        return SelectType.Self;
    }

    public override List<ElementType> GetElements()
    { 
        return new List<ElementType>() { ElementType.Blue };
    }
}