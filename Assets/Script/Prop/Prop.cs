///<author>ThomasKrahl</author>

using System;
using UnityEngine;

using IdleGame.Unit;

public class Prop : MonoBehaviour
{
    public static Action<GameObject> PropDeleted;
    public float speed = 1f;
    public float yPos = 0f;

    private UnitState state;

    private void Awake()
    {
        transform.position = new Vector3 (transform.position.x, yPos, transform.position.z);
        state = UnitState.Resting;
    }

    private void Update()
    {
        CheckState();
    }

    private void CheckState()
    {
        switch (state)
        {
            case UnitState.Invalid:
            default:
                break;


            case UnitState.Resting:            
                break;


            case UnitState.Running:
                transform.position += Vector3.left * speed * Time.deltaTime;
                break;
        }
    }

    private void OnDestroy()
    {
        PropDeleted?.Invoke(gameObject);
    }

    internal void SetState(UnitState playerState)
    {
        state = playerState;
    }
}
