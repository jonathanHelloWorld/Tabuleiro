using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionController : MonoBehaviour
{

	[HideInInspector]public Question question = null;
	public bool usedAmountQuestions = false;
	public List<Question> questions;

	private void Start()
	{
		GameManager.Instance.OnReceiverQuestion += ChangeCurrentQuestion;
		question = null;
	}

	public void ChangeCurrentQuestion(Question question)
	{
		if (!usedAmountQuestions) { this.question = question; }
		else
		{
			Debug.Log("Else");
			OnSelectQuestionsAmount();
		}
	}

	public void OnSelectQuestionsAmount()
	{
		if(question != null) { Debug.Log("!= null"); DeleteCurrentQuestion(question.id); }
		
		if(questions.Count == 0)
		{
			//GAME ACABOU!
			Debug.Log("IS over");
		}
		else
		{
			Debug.Log("QuestionsSet");
			int random = Random.Range(0, questions.Count);
			question = questions[random];
		}
	}

	private void DeleteCurrentQuestion(string id)
	{
		int index = questions.FindIndex(0, x => x.id == id);
		questions.RemoveAt(index);
	}

	public void NextQuestion()
	{
		StartCoroutine(WaitForNextQuestion());
	}

	public void NextQuestionStart()
	{
		Debug.Log("StartQuestion");
		if (usedAmountQuestions)
		{
			ChangeCurrentQuestion(null);
			GameManager.Instance.GameStart();
		}
	}

	IEnumerator WaitForNextQuestion()
	{
		yield return new WaitForSeconds(1f);
		Debug.Log("NextQuestion");
		if (usedAmountQuestions)
		{
			ChangeCurrentQuestion(null);
			GameManager.Instance.GameStart();
		}
	}

	public string GetNameQuestion()
	{
		return question.titulo;
	}

	public int GetQtdAlternatives()
	{
		return question.alternativas.Count;
	}
}
