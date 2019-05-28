using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using System.Threading;
using System;
using SocketIO;

public class SocketIOManager : MonoBehaviour
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

		socketIO.On("casa", (SocketIOEvent ev) =>
		{
			Debug.Log("CASA");
			CasaType data = Mapper.MapCreate<CasaType>(ev.data.ToString(), new CasaType());

			if (DataController.Instance.registers.Values.Count > 0)
			{

				if (data.tipo.Equals("popup"))
				{
					GameManager.Instance.ShowPopupID(data.id);
					Pagination.Instance.OpenPage(4);
				}
				else
				{
					PesquisaController.Instance.indexCurrent = data.id;
					PesquisaController.Instance.SetCurrentQuestion();
					Pagination.Instance.OpenPage(3);
				}

				GameManager.Instance.txtCasa.text = data.nome;
			}

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


		//socketIO.On("finalizar", (SocketIOEvent ev) =>
		//{
		//	Debug.Log("Finish");

		//	string data = JsonConvert.DeserializeObject<string>(ev.data.ToString());
		//	GameManager.Instance.SelectFinish(data);
		//	Pagination.Instance.OpenPage(5);
		//});
	}
}
