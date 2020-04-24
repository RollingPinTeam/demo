using System.Collections.Generic;

public class AllFireDamage : Skill
{
    public override void Execute(Env env)
    {
        for(var i = 0; i < env.targets.Count; ++i)
        {
            Effect.Damage(env.caster, env.targets[i], DamageType.Fire, 100);    
        }
    }

    public override string GetName()
    {
        return "烛龙焚天";
    }

    public override string GetDesc()
    {
        return "对所有敌方造成100%火属性伤害";
    }

    public override SkillID GetID()
    {
        return SkillID.AllFireDamage;
    }

    public override SelectType GetSelectType()
    {
        return SelectType.AllEnemy;
    }

    public override List<ElementType> GetElements() 
    { 
        return new List<ElementType>() { ElementType.Yellow, ElementType.Red }; 
    }
}