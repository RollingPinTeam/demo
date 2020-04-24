using System.Collections.Generic;

public class WindDamage : Skill
{
    public override void Execute(Env env)
    {
        Effect.Damage(env.caster, env.target, DamageType.Wind, 150);
    }

    public override string GetName()
    {
        return "疾风";
    }

    public override string GetDesc()
    {
        return "对一个敌方造成150%风属性伤害";
    }

    public override SkillID GetID()
    {
        return SkillID.WindDamage;
    }

    public override SelectType GetSelectType()
    {
        return SelectType.Enemy;
    }

    public override List<ElementType> GetElements() 
    { 
        return new List<ElementType>() { ElementType.Yellow }; 
    }
}