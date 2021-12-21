using System.Collections;
using System.Collections.Generic;
using Ball;
using TMPro;
using UnityEngine;


public class FireBall : MonoBehaviour
{
    [SerializeField] private MoveToDirection BallPrefab;
    [SerializeField] private float initialBallDistanceFromCenter = 1;
    [SerializeField] private FollowPointer directionManager;
    [SerializeField] private float ballSpeed = 4;
    [SerializeField] private ModifierManager modManager;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private List<int> m_possibleValues;
    [SerializeField] private TextMeshPro m_currentValueDisplay;
    [SerializeField] private MeshRenderer m_currentBall;

    private int m_currentValue = 0;
    public delegate int ModifierDelegate(int value);
    private ModifierDelegate currentModifier;

    private float lastTimeFired = 0f;

    public float TimeBetwennFireBalls => 1f / fireRate;

    private void Start()
    {
        currentModifier = modManager.CurrentModifier.ModifierMethod;
        modManager.ModifierListUpdated.AddListener((list) => { currentModifier = modManager.CurrentModifier.ModifierMethod; });
        m_currentValue = SortValue();
        
    }

    public void Fire()
    {
        if (Time.time - lastTimeFired < TimeBetwennFireBalls)
            return;

        lastTimeFired = Time.time;
        var ball = Instantiate(BallPrefab.gameObject, null).GetComponent<MoveToDirection>();
        ball.Direction = directionManager.GetDirection();
        ball.transform.position = InitialBallPosition;
        ball.Speed = ballSpeed;

        AudioManager.GetInstance().PlayAudio("fire_ball");

        var valueHolder = ball.GetComponent<HoldValue>();
        valueHolder.Value = m_currentValue;
        ApplyModifier(currentModifier, valueHolder);
        m_currentValue = SortValue();
    }

    public void SetCurrentModifier(ModifierDelegate modifier) 
    {
        currentModifier = modifier;
    }

    private void ApplyModifier(ModifierDelegate modifier, HoldValue value) 
    {
        if(modifier != null)
            value.Value = modifier(value.Value);
    }

    private Vector3 InitialBallPosition
    {
        get 
        {
            return (Vector3)InitialBallDistanceFromCenter + transform.position;
        }
    }

    private Vector2 InitialBallDistanceFromCenter 
    {
        get 
        {
            return directionManager.GetDirection() * initialBallDistanceFromCenter;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(InitialBallPosition,1);
    }

    private int SortValue()
    {
        var random = Random.Range(0, m_possibleValues.Count);
        m_currentValueDisplay.text = m_possibleValues[random].ToString();
        m_currentBall.GetComponent<BallColorManager>().SetColor(m_currentBall, m_possibleValues[random]);
        return m_possibleValues[random];
    }
}
