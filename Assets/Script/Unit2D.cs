using UnityEngine;

public class Unit2D : MonoBehaviour 
{
    public float speed = 1;
    private Rigidbody2D rb;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();    
        // Debug.Log(rb.isKinematic);
    }

    public void Move(Vector2 delta)
    {
        rb.MovePosition(rb.position + delta * speed);
    }
}