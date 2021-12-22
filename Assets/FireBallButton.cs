using UnityEngine;
using UnityEngine.UI;

public class FireBallButton : MonoBehaviour
{
    private Button _button;
    private FireBall _fireBall;
    private void Awake()
    {
        _button = GetComponent<Button>();
        _fireBall = FindObjectOfType<FireBall>();
        _button.onClick.AddListener(_fireBall.Fire);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(_fireBall.Fire);
    }
}
