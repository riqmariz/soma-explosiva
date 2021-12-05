﻿
using System.Collections.Generic;
using SharedData.Values;
using UnityEngine;
using Event = SharedData.Events.Event;
using Random = UnityEngine.Random;

public class BossQuestionManager : MonoBehaviour
{
    [SerializeField] 
    private List<QuestionSO> bossQuestions;
    [SerializeField] 
    private IntValue targetValue;
    [SerializeField] 
    private StringListValue questionValue;
    [SerializeField] 
    private Event onValidHit;
    [SerializeField] 
    private Event onKillBoss;

    private List<QuestionSO> runtimeBossQuestions;
    private QuestionSO currentQuestion;
    private void Awake()
    {
        onValidHit.AddCallback(NextQuestion);
        onKillBoss.AddCallback(RemoveCallbacks);
    }
    private void Start()
    {
        NextQuestion();
    }
    private void InitRuntimeQuestions()
    {
        runtimeBossQuestions = new List<QuestionSO>(bossQuestions);
    }
    private void NextQuestion()
    {
        if (runtimeBossQuestions == null || runtimeBossQuestions.Count < 0)
        {
            InitRuntimeQuestions();
        }
        
        var random = Random.Range(0, runtimeBossQuestions.Count);
        currentQuestion = bossQuestions[random];
        targetValue.Value = currentQuestion.AnswerTargetValue;
        //this event below just triggers when change the reference of the list, not when manipulating
        questionValue.Value = currentQuestion.QuestionTexts;
        //look ahead
        runtimeBossQuestions.Remove(currentQuestion);
    }
    private void RemoveCallbacks()
    {
        onValidHit.RemoveCallback(NextQuestion);
        onKillBoss.RemoveCallback(RemoveCallbacks);
    }
    private void OnDestroy()
    {
        RemoveCallbacks();
    }
}
