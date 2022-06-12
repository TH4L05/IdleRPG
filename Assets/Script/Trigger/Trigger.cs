///<author>ThomasKrahl</author>

using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    #region Events

    [Space(5)]
    [Header("Events")]
    public UnityEvent OnObjectTriggerEnter;
    public UnityEvent OnObjectTriggerStay;
    public UnityEvent OnObjectTriggerExit;

    #endregion

    #region Fields

    [Space(5)]
    [Header("Parameters")]
    [SerializeField] private bool destroyOnEnter = false;
    [SerializeField] private bool destroyOnExit = false;
    [SerializeField] private bool playerOnly = false;
    [SerializeField] private string triggerTag;
    [SerializeField] private bool triggerTagOnly = false;

    [Space(5)]
    [Header("Dev")]
    [SerializeField] private Color gizmoColor = Color.cyan;

    public GameObject objInTrigger;

    #endregion

    #region Unity Functions

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        if (triggerTagOnly)
        {
            if (collision.CompareTag(triggerTag)) return;       
        }
        objInTrigger = collision.gameObject;
        OnObjectTriggerEnter?.Invoke();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (triggerTagOnly)
        {
            if (collision.CompareTag(triggerTag)) return;
        }
        objInTrigger = collision.gameObject;
        OnObjectTriggerStay?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (triggerTagOnly)
        {
            if (collision.CompareTag(triggerTag)) return;
        }
        objInTrigger = null;
        OnObjectTriggerExit?.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }

    #endregion
}
