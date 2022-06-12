///<author>ThomasKrahl</author>

using UnityEngine;

public class TriggerEvents : MonoBehaviour
{
   public void DestroyProp()
    {
        var obj = transform.GetComponent<Trigger>().objInTrigger;
        Destroy(obj);
    }
}
