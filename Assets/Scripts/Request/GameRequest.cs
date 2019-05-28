using UnityEngine;
using System.IO;
using LitJson;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq;
using UnityEngine.Networking;
using System.Text;

public class GameRequest : Singleton<GameRequest> {

	public string baseUrl = "http://10.11.1.84:9000/";
	
	private string urlGetQuestions = "perguntas";
	private string urlPostPontos = "pontuacoes/";
	private string urlPostSearchs = "respostas/all";
	private string urlPostResultFinal = "respostas";
	private string urlPostRegister = "usuarios";

	public int gambiarra = 5;

	public void SubmitAnswer (Dictionary<string,string> data){
		StartCoroutine(Upload1(data));
	}
	
	IEnumerator Upload1(Dictionary<string, string> data) {
		//WWWForm form = new WWWForm();
		
			UnityWebRequest www = UnityWebRequest.Post(baseUrl + urlPostPontos, data);

			yield return www.SendWebRequest();

			if (www.isNetworkError || www.isHttpError) {
				Debug.Log(www.error);
			}
			else {
	
			Debug.Log("Form upload complete!");
			}
	}

	public void PostRegister(Dictionary<string, string> data)
	{
		StartCoroutine(Upload(data));
	}

	IEnumerator Upload(Dictionary<string, string> data)
	{
		WWWForm form = new WWWForm();

		UnityWebRequest www = UnityWebRequest.Post(baseUrl + urlPostRegister, data);

		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError)
		{
			Debug.Log(www.error);
		}
		else
		{
			Account account = JsonConvert.DeserializeObject<Account>(www.downloadHandler.text);
			DataController.Instance.account = account;

			//Debug.Log(DataController.Instance.account.id);
			Pagination.Instance.OpenPage(0);
			Debug.Log("Form upload complete!");
		}
	}

	public void SubmitSearchs()
	{
		Debug.Log("Submit");
		StartCoroutine(UploadSearchs(PesquisaController.Instance.GetResultPesquisa()));
	}

	IEnumerator UploadSearchs(List<Dictionary<string,string>> data) //Survey Search = Pesquisa de procurar, Survey = Pesquisa estatistica
	{
		Debug.Log("data" + data);

		string dataJson = JsonConvert.SerializeObject(data, Formatting.Indented);
		string dataModify  = dataJson.Replace(System.Environment.NewLine, "");

		WWWForm form  =  new WWWForm();

		form.AddField("respostas", dataModify);

		UnityWebRequest www = UnityWebRequest.Post(baseUrl + urlPostSearchs, form);
	
		yield return www.SendWebRequest();

		if(www.isNetworkError || www.isHttpError)
		{
			Debug.Log(www.error);
		}
		else
		{
			int indexResult = JsonConvert.DeserializeObject<int>(www.downloadHandler.text);
			PesquisaController.Instance.SetQuestionResult(indexResult);
			Pagination.Instance.OpenPage(gambiarra);

			Debug.Log("Form upload complete!");
		}
	}

	public void SubmitResultFinal()
	{
		Debug.Log("SubmitFinal");
		StartCoroutine(UploadResult(PesquisaController.Instance.GetResultFinal()));
	}

	IEnumerator UploadResult(Dictionary<string, string> data) 
	{
		UnityWebRequest www = UnityWebRequest.Post(baseUrl + urlPostResultFinal, data);

		yield return www.SendWebRequest();

		if (www.isNetworkError || www.isHttpError)
		{
			Debug.Log(www.error);
		}
		else
		{
			Debug.Log("Form upload complete!");
			DataController.Instance.ResetGame();
		}
	}

	public void LoadQuestions()
	{
		WWW request = new WWW(baseUrl + urlGetQuestions);
		StartCoroutine(OnResponse(request));
	}

	private IEnumerator OnResponse(WWW req)
	{
		yield return req;

		SetAllIData(req);
	}

	private void SetAllIData(WWW req)
	{

		if(req != null && !string.IsNullOrEmpty(req.text))
		{
			
			List<SearchMap> questions = Mapper.MapCreateList<SearchMap>(req.text, new List<SearchMap>());
			PesquisaController.Instance.SetQuestions(questions);
		}
	}

}
