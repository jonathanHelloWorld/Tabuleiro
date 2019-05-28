using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PesquisaController : Singleton<PesquisaController>
{
	public event Events.SimpleEvent OnResetAlts;

	public bool isTablet = false;
	public List<QuestionP> questions;
	public QuestionP refQuestion;
	[HideInInspector] public int indexCurrent;

	[Space]
	[Header(" Results")]
	public List<SurveyResult> results;
	public QuestionP questionResult;
	public Image imgResult;

    void Start()
    {
		DataController.Instance.OnReset += Reset;
    }

	public void Reset()
	{
		for (int i = 0; i < questions.Count; i++)
		{
			questions[i].answer = "";
		}

		//if(isTablet)
			questionResult.answer = "";
	}

	public bool AnswersIsCompleted()
	{

		if (!isTablet)
		{
			for (int i = 0; i < questions.Count; i++)
			{
				if (string.IsNullOrEmpty(questions[i].answer))
				{
					return false;
				}
			}
		}
		else
		{
			if (string.IsNullOrEmpty(refQuestion.answer))
			{
				return false;
			}
		}
		return true;
	}

	public List<Dictionary<string,string>> GetResultPesquisa()
	{
		List <Dictionary<string, string>> datas = new List<Dictionary<string, string>>();

		for (int i = 0; i < questions.Count; i++)
		{
			Dictionary<string, string> data = new Dictionary<string, string>();

			data.Add("idUsuario", DataController.Instance.account.id);
			data.Add("alternativa", questions[i].answer);
			data.Add("pergunta", questions[i].id);

			data.Add("valor", questions[i].answer == "sim" ? questions[i].valor.ToString() : "0");
			
			datas.Add(data);
		}

		return datas;
	}

	public void SetQuestions(List<SearchMap> datas)
	{
			for (int i = 0; i < questions.Count; i++)
			{
				questions[i].txt.text = datas[i].titulo;
				questions[i].id = datas[i].id;
				questions[i].valor = datas[i].valor;
			}
	}

	public void AddValuesInQuestion()
	{
		questions[indexCurrent].answer = refQuestion.answer;
		if(indexCurrent == questions.Count - 1)
		{
			Debug.Log("ACABOU");
			GameRequest.Instance.SubmitSearchs();
		}
	}

	public void SetCurrentQuestion()
	{
		refQuestion.txt.text = questions[indexCurrent].txt.text;
		refQuestion.answer = questions[indexCurrent].answer;
	}

	public void ResetAlternativaColor()
	{
		if (OnResetAlts != null) OnResetAlts();
	}

	//RESULT REFERENTE AO FINALIZAR DEPOIS DE COMPUTAR A PESQUISA
	public bool ResultIsCompleted()
	{
		if (string.IsNullOrEmpty(questionResult.answer))
		{
				return false;
		}
		return true;
	}

	public void SetQuestionResult(int index)
	{
		imgResult.sprite = results[index-1].spriteResult;
		questionResult.txt.text = results[index-1].txtResult;
		questionResult.id = results[index-1].idResult;
	}

	public Dictionary<string,string> GetResultFinal()
	{
		Dictionary<string, string> data = new Dictionary<string, string>();

		Debug.Log(DataController.Instance.account.id);

		data.Add("idUsuario", DataController.Instance.account.id);
		data.Add("resultId", questionResult.id);
		data.Add("alternativa", questionResult.answer);

		return data; 
	}

}
