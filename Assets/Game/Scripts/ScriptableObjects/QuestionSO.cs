using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Question", order = 0)]
public class QuestionSO : ScriptableObject
{
    [SerializeField] 
    private List<string> questionTexts;
    [SerializeField] 
    private int answerTargetValue;

    public List<string> QuestionTexts => questionTexts;
    public int AnswerTargetValue => answerTargetValue;
}
