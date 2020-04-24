using System.Collections.Generic;

public class IceDamage : Skill
{
    public override void Execute(Env env)
    {
        Effect.Damage(env.caster, env.target, DamageType.Ice, 150);
    }

    public override string GetName()
    {
        return "冰刺";
    }

    public override string GetDesc()
    {
        return "对一个敌方造成150%冰属性伤害";
    }

    public override SkillID GetID()
    {
        return SkillID.IceDamage;
    }

    public override SelectType GetSelectType()
    {
        return SelectType.Enemy;
    }

    public override List<ElementType> GetElements() 
    { 
        return new List<ElementType>() { ElementType.Blue }; 
    }
}