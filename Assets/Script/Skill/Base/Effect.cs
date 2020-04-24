public enum DamageType
{
    Physic = 1,
    Fire = 2,
    Ice = 3,
    Wind = 4,
}

public static class Effect
{
    public static int Damage(Unit caster, Unit target, DamageType type, int ratio)
    {
        return 0;
    }
}
