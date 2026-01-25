using UnityEngine;

public class CharacterMotor
{
    private readonly Transform transform;
    private readonly Rigidbody rigidbody;

    public CharacterMotor(Transform transform, Rigidbody rigidbody)
    {
        this.transform = transform;
        this.rigidbody = rigidbody;

    }

    public void Move(Vector2 direction)
    {

    }

    public void Jump(float impulse)
    {

    }

    private bool CheckGrounded()
    {
        return Physics.Raycast(
            transform.position,
            Vector3.down,
            1.1f
        );
    }
}