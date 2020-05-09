using UnityEngine;
using UnityEngine.InputSystem;

public class UnitControl : Interaction
{
    private Unit2D unit;
    
    public UnitControl(Unit2D unit)
    {
        this.unit = unit;
    }

    public override void OnMove(Vector2 vector)
    {        
        unit.Move(vector);
    }
}

public class MainScene : MonoBehaviour 
{
    public Unit2D unit;

    private void Awake() 
    {
        InputMgr.inst.Init();
        InputMgr.inst.Push(new UnitControl(unit));
    }

    private void FixedUpdate() 
    {
        InputMgr.inst.FixedUpdate();
    }
}