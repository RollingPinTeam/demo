using System.Collections.Generic;

public enum SelectType
{
    Self,
    Firend,
    FirendWithMe,
    Enemy,
    AllFirend,
    AllEnemy,
    All,
}

public enum SkillID
{
    PhysicDamage,
    Counter,
    Cover,
    Defense,
    Dodge,
    Enforce,
    Change,
    FireDamage,
    IceDamage,
    WindDamage,
    AllFireDamage,
    AllIceDamage,
    AllWindDamage,
}

public abstract class Skill
{
    public string shortKey { get; private set; }

    public static Skill Create(SkillID id)
    {
        switch(id)
        {
            case SkillID.AllFireDamage: return new AllFireDamage();
            case SkillID.AllIceDamage: return new AllIceDamage();
            case SkillID.AllWindDamage: return new AllWindDamage();
            case SkillID.Counter: return new Counter();
            case SkillID.Cover: return new Cover();
            case SkillID.Defense: return new Defense();
            case SkillID.Dodge: return new Dodge();
            case SkillID.FireDamage: return new FireDamage();
            case SkillID.IceDamage: return new IceDamage();
            case SkillID.PhysicDamage: return new PhysicDamage();
            case SkillID.WindDamage: return new WindDamage();
            case SkillID.Enforce: return new Enforce();
            case SkillID.Change: return new Change();
        }

        return null;
    }

    public void Cast(Unit owner, Unit target)
    {
        var env = new Env() { caster = owner, owner = owner };

        switch(GetSelectType())
        {
            case SelectType.Self: 
                env.target = owner;
                break;
            case SelectType.Firend:
                env.target = target;
                break;
            case SelectType.FirendWithMe:
                env.target = target;
                break;
            case SelectType.Enemy:  
                env.target = target;
                break;
            case SelectType.AllFirend:
                env.targets = ModelMgr.inst.heros;
                break;
            case SelectType.AllEnemy:
                env.targets = ModelMgr.inst.enemys;
                break;
            case SelectType.All:
                var targets = new List<Unit>();
                targets.AddRange(ModelMgr.inst.heros);
                targets.AddRange(ModelMgr.inst.enemys);
                env.targets = targets;
                break;
        }

        Execute(env);
        ModelMgr.inst.CostElement(GetElements());
        owner.DoAction();
    }

    public void SetShortKey(string key) { shortKey = key; }
    public virtual List<ElementType> GetElements() { return null; }
    public abstract SkillID GetID();
    public abstract SelectType GetSelectType();
    public abstract string GetName();
    public abstract string GetDesc();
    public abstract void Execute(Env env);
}