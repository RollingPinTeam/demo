using UnityEngine;

public enum DamageType
{
    Physic = 1,
    Fire = 2,
    Ice = 3,
    Wind = 4,
}

public static class Effect
{
    public static void Damage(Unit caster, Unit target, DamageType type, int ratio)
    {
        if (target.isDodge && Random.Range(0, 100) < 75)
        {
            BattleUI.inst.Record("{0}闪避了{1}的攻击", target.name, caster.name);
            return;
        }

        if (target.coveredTarget != null && type == DamageType.Physic)
        {
            Damage(caster, target.coveredTarget, type, ratio);
            return;
        }
        
        var damage = -caster.atk * ratio * 0.01f;
        if (target.isDefense) { damage *= 0.5f; }
        if (caster.isEnforce) { damage *= 2f; }
        var intDamage = (int)damage;
        target.ModifyHp(intDamage);
        target.AddDamageType(type);
        BattleUI.inst.Record("{0}对{1}造成{2}点伤害", caster.name, target.name, intDamage);

        if (type == DamageType.Physic && target.isCounter) 
        { 
            Damage(target, caster, type, 100); 
        }
    }
}
