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


// PACOTE
using CRUDANDROIDMYSQL_.Modelo;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CRUDANDROIDMYSQL_
{
    [Activity(Label = "Select")]
    public class Select : Activity, IEvento
    {

        // CRIA UMA LISTA DE DADOS
        List<SelectJson> dado = new List<SelectJson>();

        // BASEADAPTER PERSONALIZADO
        listViewBaseAdapter adapter;

        // PARA REFERENCIAR LISTVIEW 
        ListView ltv;



        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // chama tela Select
             SetContentView(Resource.Layout.Select);

            // REFERENCIA O LISTVIEW
            ltv = FindViewById<ListView>(Resource.Id.lsvSelect);

            //ATRIBUI OS DADOS RETORNAN DO AO CLASSE E FORMA DE LIST<>
            dado = await Resgistro();

            //ADAPTAR O DADOS RETORNADO NO LISTVIEW PERSONALIZADO
            adapter = new listViewBaseAdapter(this, dado);

            // APLICAR INTERFACE COM EVENTO
            adapter.SetEvento(this);

            // FAZ CARREGAR O DADOS DA LIST<> NO LISTVIEW
            ltv.Adapter = adapter;

        }

        // delete
        public void onDelete(string value)
        {
            // testar para pegar o id_us do listviewbaseadapter
            Toast.MakeText(this, value, ToastLength.Short).Show();

            //Fazer uma mensagem de alert
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            //Titulo
            alert.SetTitle("CRUD MYSQL");
            //Icone
            alert.SetIcon(Android.Resource.Drawable.IcDialogDialer);
            //Mensagem
            alert.SetMessage("Deseja Excluir");
            //Evento
            alert.SetPositiveButton("OK", async (SenderAlert, args) => {
                //Toast.MakeText(this, value, ToastLength.Long).Show();
                DeleteRegistro(int.Parse(value));

                dado = await Resgistro();
                adapter = new listViewBaseAdapter(this, dado);
                adapter.SetEvento(this);
                ltv.Adapter = adapter;
                
            });

            alert.SetNegativeButton("NO", (SenderAlert, args) =>
            {
                adapter = new listViewBaseAdapter(this, dado);
                adapter.SetEvento(this);
                ltv.Adapter = adapter;

            });

            alert.Show();
            adapter.NotifyDataSetChanged();
        }

        public void onUpdate(string value)
        {
            //pASSAR PARA A TELA led
            Intent pagUpdate = new Intent(this, typeof(Update));
            Bundle parametro = new Bundle();
            parametro.PutString("id", value);
            pagUpdate.PutExtras(parametro);
            StartActivity(pagUpdate);

        }


        //Deletar Registro do Banco
        private async void DeleteRegistro(int id)
        {
            //URL
            string uri = "http://10.131.45.20:8081/CRUDANDROID/delete.php";

            //Criar Objeto de Envio
            HttpClient solicita = new HttpClient();

            //Dicionario ou selecJons
            Dictionary<string, string> i = new Dictionary<string, string>(); i.Add("id", id.ToString());

            //Converter em Json
            var cvJason = JsonConvert.SerializeObject(i);

            //Ver como ficou json testar o json
            Console.WriteLine("js" + cvJason);

            //Exixtem 3 construtores
            //O arquivo, tipo de cod. Caracteres, aplicativo MIMW
            var contentString = new StringContent(cvJason, Encoding.UTF8, "application/json");

            //Representa um tipo de midia usado em um cabeçalho Content-Type conforme definido no RFC
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //Envia o dados
            HttpResponseMessage resultado = await solicita.PostAsync(uri, contentString);

            //Testar resposta
            Console.WriteLine(" resp: " + resultado.IsSuccessStatusCode);

            //Tira a resposta em string
            var content = await resultado.Content.ReadAsStringAsync();

            //Saber a volta
            Console.WriteLine(" delete -> " + content);
            //Descompactar a respota vindo servidor em forma json para em forma dicionary
            Dictionary<string, string> servidor = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);

            if (servidor["resp"] == "sucesso")
            {
                Toast.MakeText(this, "Deletado com sucesso", ToastLength.Long).Show();

                //Atribui os dados retornan do ao classe de list<>
                dado = await Resgistro();

                //Adaptar o Dados Retornando no ListView Personalizado
                adapter = new listViewBaseAdapter(this,dado);

                //Aplicar interface com evento
                adapter.SetEvento(this);
            }
                                
        }

        private async Task<List<SelectJson>> Resgistro()
        {
           string uri = "http://10.131.45.20:8081/CRUDANDROID/select_.php";
           

            HttpClient solicita = new HttpClient();

            HttpResponseMessage resultado = await solicita.PostAsync(uri, null);

            // IsSuccessStatusCode  - Obtém um valor que indica se a 
            // resposta HTTP foi bem-sucedida.
            Console.WriteLine(" resp: " + resultado.IsSuccessStatusCode);

            //TRAGA A RESPOSTA EM STRING;
            var content = await resultado.Content.ReadAsStringAsync();
            Console.WriteLine("dados :" + content);

            // DESCOMPACTA O RESPOSTA VENDO SERVIDOR EM FORMA JSON PARA EM FORMA DICTIONARY
            List<SelectJson> i = JsonConvert.DeserializeObject<List<SelectJson>>(content);
            // TESTAR A CONVERSÃO DO JSON DO SERVIDOR
            // TESTAR A CONVERSÃO DO JSON DO SERVIDOR
            Console.WriteLine("respost " + i);
            Console.WriteLine("respost " + i.Count);


            foreach (SelectJson T in i)
            {
                Console.WriteLine("mysql : " + T.ID_US + " - " + T.NOME_US);

            }

            return i;


        }







    }
}