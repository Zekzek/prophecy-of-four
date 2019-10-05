using TMPro;
using UnityEngine;

public class ThingBehaviour : MonoBehaviour
{
    [SerializeField]
    private new Rigidbody rigidbody;

    [SerializeField]
    private TextMeshProUGUI displayNameText;

    public string DisplayName { get { return displayNameText.text; } set { displayNameText.text = value; } }

    public void Move(float horizontal, float vertical)
    {
        rigidbody.velocity = new Vector3(horizontal, 0, vertical);
    }
}