﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace CRUDANDROIDMYSQL_.Modelo
{
    class Atualiza
    {
        // cada campo de formulario vai ter um 
       
        public string id_us { get; set; }

        public string nome_us { get; set; }

        public string email_us { get; set; }

        public string senha_us { get; set; }
    }
}