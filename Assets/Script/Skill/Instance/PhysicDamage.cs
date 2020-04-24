public class PhysicDamage : Skill
{
    public override void Execute(Env env)
    {
        Effect.Damage(env.caster, env.target, DamageType.Physic, 100);
    }

    public override string GetName()
    {
        return "攻击";
    }

    public override string GetDesc()
    {
        return "对一个敌方造成100%物理伤害";
    }

    public override SkillID GetID()
    {
        return SkillID.PhysicDamage;
    }

    public override SelectType GetSelectType()
    {
        return SelectType.Enemy;
    }
}