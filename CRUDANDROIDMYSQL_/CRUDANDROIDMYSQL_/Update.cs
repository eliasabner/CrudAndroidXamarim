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
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CRUDANDROIDMYSQL_
{
    [Activity(Label = "Update")]
    public class Update : Activity
    {
        //Campos
        TextView id;
        EditText nome, email, senha;
        String id_us;
        Button btAtualiza,btVoltar;


        //
        List<CreateInserir> campo = new List<CreateInserir>();
        Atualiza atualizaRegistro = new Atualiza();



        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Update);

            //referencias
            id = FindViewById<TextView>(Resource.Id.campoID);
            nome = FindViewById<EditText>(Resource.Id.campoNome);
            email = FindViewById<EditText>(Resource.Id.campoEmail);
            senha = FindViewById<EditText>(Resource.Id.campoSenha);
            btAtualiza = FindViewById<Button>(Resource.Id.btAtualiza);
            btVoltar = FindViewById<Button>(Resource.Id.btVoltar);

            //pegar o id do usuario que vem da tela listview
            id_us = Intent.GetStringExtra("id");

            //colocar no editext
            id.Text = id_us;


            campo = await Registro(id_us);


            //Preencher os campos
            nome.Text = campo[0].nome_us;
            email.Text = campo[0].email_us;
            senha.Text = campo[0].senha_us;

            btAtualiza.Click += BtAtualiza_Click;
            btVoltar.Click += delegate
            {
                StartActivity(typeof(MENU));
            };

        }

        private async void BtAtualiza_Click(object sender, EventArgs e)
        {
            atualizaRegistro.id_us = id.Text;
            atualizaRegistro.nome_us = nome.Text;
            atualizaRegistro.email_us = email.Text;
            atualizaRegistro.senha_us = senha.Text;

            string uri = "http://10.131.45.20:8081/CRUDANDROID/UpdateRegistro.php";

            HttpClient solicita = new HttpClient();

            var json = JsonConvert.SerializeObject(atualizaRegistro);

            var conteudo = new StringContent(json, Encoding.UTF8, "application/json");
            conteudo.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //Enviar
            HttpResponseMessage resultado = await solicita.PostAsync(uri, conteudo);


            // saber se o estou conectado com o servidor
            Console.WriteLine(" - - " + resultado.IsSuccessStatusCode);

            var serResp = await resultado.Content.ReadAsStringAsync();

            Dictionary<string, string> i = JsonConvert.DeserializeObject<Dictionary<string, string>>(serResp);
        
            if(i["resp"] == "yes")
            {
                Toast.MakeText(this, "Alterodo com sucesso", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(this, "Alterodo com sucesso", ToastLength.Short).Show();

            }
            //Console.WriteLine(serResp);

           // Console.WriteLine(json);
           
        }

        // registro
        private async Task<List<CreateInserir>>Registro(string value)
        {   
            //Caminho
            string uri = "http://10.131.45.20:8081/CRUDANDROID/SelectUpdate.php";

            HttpClient solicita = new HttpClient();
            //array
            Dictionary<string, string> idDado = new Dictionary<string, string>();
            idDado.Add("id", value);

            //Tranformar em json
            var json = JsonConvert.SerializeObject(idDado);
            var conteudo = new StringContent(json, Encoding.UTF8, "application/json");

            conteudo.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //Enviar
           HttpResponseMessage resultado = await solicita.PostAsync(uri,conteudo);

            // saber se o estou conectado com o servidor
            Console.WriteLine(" - - " + resultado.IsSuccessStatusCode);

            //ler os dados
            var DadosServidorJson = await resultado.Content.ReadAsStringAsync();

            // fazer o teste a resposta dos dados do servidor
            Console.WriteLine(DadosServidorJson);

            List<CreateInserir> dadoConv = JsonConvert.DeserializeObject<List<CreateInserir>>(DadosServidorJson);

            //fazer os dados mostrar no console
            foreach (var c in dadoConv)
            {
                Console.WriteLine(c.id_us);
                Console.WriteLine(c.nome_us);
                Console.WriteLine(c.email_us);
                Console.WriteLine(c.senha_us);

            }

            return dadoConv;
        }

        
    }
}