///<author>ThomasKrahl</author>

using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    #region Fields

    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] [Range(0.1f, 10f)] private float speed = 1f;
    [SerializeField] [Range(0.1f, 10f)] private float lifeTime = 2f;

    #endregion

    #region UnityFunctions

    private void Awake()
    {
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    #endregion

    public void SetText(float value)
    {
        textField.text = value.ToString();
    }

    public void SetText(string text)
    {
        textField.text = text;
    }

}
