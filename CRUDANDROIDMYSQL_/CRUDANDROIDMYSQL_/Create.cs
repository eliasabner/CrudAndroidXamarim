using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CRUDANDROIDMYSQL_.Modelo;
using Newtonsoft.Json; //Pacote do Json
using System.Net.Http;
using System.Net.Http.Headers;

namespace CRUDANDROIDMYSQL_
{
    [Activity(Label = "Create")]
    public class Create : Activity
    {

        Button btVoltar,btCad;

        // instancia / objeto / variavel

        CreateInserir inserir = new CreateInserir();

        EditText nome, email, senha;
              



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Create);
            // referncia
            btVoltar = FindViewById<Button>(Resource.Id.btVoltar);
            nome = FindViewById<EditText>(Resource.Id.campoNome);
            email = FindViewById<EditText>(Resource.Id.campoEmail);
            senha = FindViewById<EditText>(Resource.Id.campoSenha);
            btCad = FindViewById<Button>(Resource.Id.btCad);

            //eventos
            btCad.Click += BtCad_Click;
            btVoltar.Click += BtVoltar_Click;
        }

        private async void BtCad_Click(object sender, EventArgs e)
        {
            //Pegar todos os campos colocar na classe com seus atributos
            inserir.nome_us = nome.Text;
            inserir.email_us = email.Text;
            inserir.senha_us = senha.Text;

            //caminho
            string url = "http://10.131.45.20:8081/CrudAndroid/inserir.php";

            HttpClient solicita = new HttpClient();
            //Converter em Json
            var json = JsonConvert.SerializeObject(inserir);

            //preparar para enviar os caraceteres
            var conteudoString = new StringContent(json,Encoding.UTF8,"application/json");

            //Representar o type media
            conteudoString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //enviar para o servidor
             HttpResponseMessage resp =  await solicita.PostAsync(url,conteudoString);

            //Pegar a resposta e poder ler do servidor
            var serResp = await resp.Content.ReadAsStringAsync();

            //Desconpactar o json
            var i = JsonConvert.DeserializeObject<string>(serResp);
            if(i == "cert")
            {
                Toast.MakeText(this, "Sucesso", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(this, "Erro", ToastLength.Short).Show();
            }

            //Console.WriteLine(" - " + serResp);
        }

        private void BtVoltar_Click(object sender, EventArgs e)
        {
            // voltar tela menu
            StartActivity(typeof(MENU));
        }
    }
}