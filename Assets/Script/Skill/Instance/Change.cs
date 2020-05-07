using System.Collections.Generic;

public class Change : Skill
{
    public override void Execute(Env env)
    {
        env.target.ModifyAP(1);
    }

    public override string GetName()
    {
        return "换手";
    }

    public override string GetDesc()
    {
        return "选择一个队友,该队友可再行动一次";
    }

    public override SkillID GetID()
    {
        return SkillID.Change;
    }

    public override SelectType GetSelectType()
    {
        return SelectType.Firend;
    }

    public override List<ElementType> GetElements()
    { 
        return new List<ElementType>() { ElementType.Blue };
    }
}