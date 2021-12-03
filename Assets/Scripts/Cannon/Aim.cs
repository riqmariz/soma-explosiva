using UnityEngine;

public class Aim : MonoBehaviour
{
    SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        sprite.enabled = Input.GetMouseButton(0);
    }
}
