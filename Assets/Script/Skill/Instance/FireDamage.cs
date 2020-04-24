using System.Collections.Generic;

public class FireDamage : Skill
{
    public override void Execute(Env env)
    {
        Effect.Damage(env.caster, env.target, DamageType.Fire, 150);
    }

    public override string GetName()
    {
        return "火息";
    }

    public override string GetDesc()
    {
        return "对一个敌方造成150%火属性伤害";
    }

    public override SkillID GetID()
    {
        return SkillID.FireDamage;
    }

    public override SelectType GetSelectType()
    {
        return SelectType.Enemy;
    }

    public override List<ElementType> GetElements() 
    { 
        return new List<ElementType>() { ElementType.Red }; 
    }
}