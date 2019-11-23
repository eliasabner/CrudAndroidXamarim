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
//Pacote
using CRUDANDROIDMYSQL_.Modelo;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CRUDANDROIDMYSQL_
{
    [Activity(Label = "Select")]
    public class Select : Activity
    {
        //Criar uma lista de dados
        List<SelectJson> dado = new List<SelectJson>();

        //Base Adapter Personalizado
        listViewBaseAdapter adapter;

        //Para Referencia ListView
        ListView ltv;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //Chama a tela Select
            SetContentView(Resource.Layout.Select);

            //Referencia List View
            ltv = FindViewById<ListView>(Resource.Id.lvsSelect);

            //Atribui os dados retornan do ao classe e forma de list<>
            dado = await Registro();

            //Adaptar o dado retorna no listView personalizado
            adapter = new listViewBaseAdapter(this, dado);

            //Aplicar Interface com Evento
            //adapter.SetEvento(this)

            //Faz Carregar o dados da list <> no listView
            ltv.Adapter = adapter;


            
        }
    }
}