using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    public Vector3 movement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movement * speed * Time.deltaTime);
    }
    public void OnMovement(InputValue val)
    {
        
        Setmovement(val.Get<Vector2>());
    }

    public void OnMovementEvent(InputAction.CallbackContext ctx)
    {
        Setmovement(ctx.ReadValue<Vector2>());
    }

    private void Setmovement(Vector2 moveVal)
    {
        movement = moveVal;
        movement.z = movement.y;
        movement.y = 0;
    }

    public void OnJump()
    {

    }
}
