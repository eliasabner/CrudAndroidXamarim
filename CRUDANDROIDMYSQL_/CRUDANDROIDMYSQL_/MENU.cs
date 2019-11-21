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

namespace CRUDANDROIDMYSQL_
{
    [Activity(Label = "MENU")]
    public class MENU : Activity
    {
        TextView Create;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.MENU);
            // referencia
            Create = FindViewById<TextView>(Resource.Id.create);

            //evento
            Create.Click += Create_Click;

        }
        // chamar a tela  create
        private void Create_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(Create));
        }
    }
}