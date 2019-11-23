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

namespace CRUDANDROIDMYSQL_
{
    public interface IEvento
    {
        void onDelete(string value);
        void onUpdate(string value);
    }
    class listViewBaseAdapter : BaseAdapter<SelectJson>
    {
        //OBS APARECE ERRO POR MOTIVO QUE TEMOS
        //DE SOBRECARREGAR OS METODOS DO BASE
        private readonly Activity context;
        private readonly List<SelectJson> usuario;

        IEvento delete;
        IEvento Update;

        //Construtor
        public listViewBaseAdapter(Activity context,List<SelectJson> usuario)
        {
            this.context = context;
            this.usuario = usuario;
        }
        /*  GetItem - retorna um indentificador de linha
            Sobrecarga
            this[int] indexador - para retornar os dados associados a um numero de linha
         */


        public override long GetItemId(int position)
        {
            return int.Parse(usuario[position].ID_US);
        }

        public override SelectJson this[int position]
        {
            get
            {
                return usuario[position];
            }
        }

        /*
            Sobrecarga
            GetView retotna uma 
         */
        public override int Count
        {
            get
            {
                return usuario.Count;
            }
        }

        
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
          
            var view = convertView ?? context.LayoutInflater.Inflate(Resource.Layout.ModeloListView, parent, false);

            var txtId = view.FindViewById<TextView>(Resource.Id.txtId);
            var txtNome = view.FindViewById<TextView>(Resource.Id.txtNome);
            var btEd = view.FindViewById<ImageButton>(Resource.Id.imgEdit);
            var btDel = view.FindViewById<ImageButton>(Resource.Id.imgDel);

            //preencher os campos
            txtNome.Text = usuario[position].NOME_US;
            txtId.Text = usuario[position].ID_US.ToString();

            btDel.Click += (sender, arg) =>
             {
                //Aplica Interface
                delete.onDelete(txtId.Text);
             };

            //Update
            btEd.Click += (sender, arg) =>
             {
                 //
                 Update.onUpdate(txtId.Text);
             };
              
            return view;   
            
        }

        public void SetEvento(IEvento listener)
        {
            this.delete = listener;
            this.Update = listener;
        }
    }
}