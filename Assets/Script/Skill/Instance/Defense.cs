using System.Collections.Generic;

public class Defense : Skill
{
    public override void Execute(Env env)
    {
        env.caster.Defense();
    }

    public override string GetName()
    {
        return "防御";
    }

    public override string GetDesc()
    {
        return "减少当前回合受到的伤害";
    }

    public override SkillID GetID()
    {
        return SkillID.Defense;
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