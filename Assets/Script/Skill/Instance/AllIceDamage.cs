using System.Collections.Generic;

public class AllIceDamage : Skill
{
    public override void Execute(Env env)
    {
        for(var i = 0; i < env.targets.Count; ++i)
        {
            Effect.Damage(env.caster, env.targets[i], DamageType.Ice, 100);    
        }
    }

    public override string GetName()
    {
        return "冻气入体";
    }

    public override string GetDesc()
    {
        return "对所有敌方造成100%冰属性伤害";
    }

    public override SkillID GetID()
    {
        return SkillID.AllIceDamage;
    }

    public override SelectType GetSelectType()
    {
        return SelectType.AllEnemy;
    }

    public override List<ElementType> GetElements() 
    { 
        return new List<ElementType>() { ElementType.Blue, ElementType.Red }; 
    }
}