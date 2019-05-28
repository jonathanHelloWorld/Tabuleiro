using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using System.Threading;
using System;
using SocketIO;

public class ScreenSocketManager : MonoBehaviour
{
	private SocketIOComponent socketIO;

	private void Start()
	{
		//  SystemContent.Instance.ChangeVersion();
		socketIO = GetComponent<SocketIOComponent>();
		GameManager.Instance.OnSocketEmit += SocketEmitR;
		OnSocket();
	}

	public void SendState()
	{
		//socketIO.Emit("enviaPerguntas", "teste");
	}

	public void SockeEmitEstado(Dictionary<string, string> dir)
	{
		string jsonData = JsonConvert.SerializeObject(dir);
		JSONObject json = new JSONObject(jsonData);
		socketIO.Emit("estado", json);
	}

	public void SocketEmitR()
	{
		//JSONObject json = JSONObject.CreateStringObject(TeamsController.Instance.myTeam.teamName);
		//socketIO.Emit("respondeu", json);
	}

	public void OnSocket()
	{
		#region DEFAULT ON
		socketIO.On("open", (SocketIOEvent ev) =>
		{
			Debug.Log("conectou " + ev.data);

		});

		socketIO.On("close", (SocketIOEvent ev) =>
		{
			Debug.Log("Disconnected.");
		});

		socketIO.On("error", (SocketIOEvent ev) =>
		{
			Debug.Log("error" + ev.data);
		});

		#endregion

		socketIO.On("pergunta", (SocketIOEvent ev) =>
		{
			//Debug.Log("PERGUNTA");

			//QuestionMap map = Mapper.MapCreate<QuestionMap>(ev.data.ToString(), new QuestionMap());
			//Dictionary<string, string> dir = new Dictionary<string, string>()
			//{
			//	{"estado","pergunta" }
			//};
			//SockeEmitEstado(dir);

			//#region MAP
			//List<Alternative> alt = new List<Alternative>();
			//for (int i = 0; i < map.alternativas.Count; i++)
			//{
			//	Alternative newAlt = new Alternative();
			//	newAlt.texto = map.alternativas[i].texto;
			//	alt.Add(newAlt);
			//}

			//Question question = new Question()
			//{
			//	titulo = map.titulo,
			//	id = map.id,
			//	alternativas = alt
			//};
			//#endregion

			//GameManager.Instance.ReceiverAnswer(question);
			//GameManager.Instance.GameStart();
			//Pagination.Instance.OpenPage(3);
		});

		socketIO.On("home", (SocketIOEvent ev) =>
		{
			Debug.Log("HOME");

			Dictionary<string, string> dir = new Dictionary<string, string>()
			{
				{"estado","home" }
			};
			SockeEmitEstado(dir);

			Pagination.Instance.OpenPage(0);
		});

		socketIO.On("tela-instrucoes", (SocketIOEvent ev) =>
		{
			Debug.Log("INSTRUCOES");

			Dictionary<string, string> dir = new Dictionary<string, string>()
			{
				{"estado","tela-instrucoes" },
			};
			SockeEmitEstado(dir);

			Pagination.Instance.OpenPage(2);
		});

		socketIO.On("tempo", (SocketIOEvent ev) =>
		{
			Debug.Log("TEMPO");

			string temp = JsonConvert.DeserializeObject<string>(ev.data.ToString());
			GameManager.Instance.UpdateTime(temp);
		});

		socketIO.On("popup", (SocketIOEvent ev) =>
		{
			Debug.Log("POPUP");

			int id = JsonConvert.DeserializeObject<int>(ev.data.ToString());
			GameManager.Instance.ShowPopupID(id);
			Pagination.Instance.OpenPage(4);
		});

		socketIO.On("finalizar", (SocketIOEvent ev) =>
		{
			Debug.Log("Finish");

			string data = JsonConvert.DeserializeObject<string>(ev.data.ToString());
			GameManager.Instance.SelectFinish(data);
			Pagination.Instance.OpenPage(5);
		});
	}
}
