using System.Collections.Generic;

public class AllWindDamage : Skill
{
    public override void Execute(Env env)
    {
        for(var i = 0; i < env.targets.Count; ++i)
        {
            Effect.Damage(env.caster, env.targets[i], DamageType.Wind, 100);    
        }
    }

    public override string GetName()
    {
        return "疾风狂卷";
    }

    public override SkillID GetID()
    {
        return SkillID.AllWindDamage;
    }

    public override string GetDesc()
    {
        return "对所有敌方造成100%风属性伤害";
    }

    public override SelectType GetSelectType()
    {
        return SelectType.AllEnemy;
    }

    public override List<ElementType> GetElements() 
    { 
        return new List<ElementType>() { ElementType.Blue, ElementType.Yellow }; 
    }
}