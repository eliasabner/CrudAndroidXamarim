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

    class listViewBaseAdapter : BaseAdapter<SelectJson>
    {
        //OBS APARECE ERRO POR MOTIVO QUE TEMOS
        //DE SOBRECARREGAR OS METODOS DO BASE
        private readonly Activity context;
        private readonly List<SelectJson> usuario;


    }
}