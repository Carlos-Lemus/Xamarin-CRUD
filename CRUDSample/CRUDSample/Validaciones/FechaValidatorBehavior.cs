using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace CRUDSample.Validaciones
{
    class FechaValidatorBehavior : Behavior<DatePicker>
    {
        //Se declaran las variables 
       static readonly BindablePropertyKey NameKey =
       BindableProperty.CreateReadOnly("FechaValido", typeof(bool),
       typeof(NameValidatorBehavior), false);

        public static readonly BindableProperty Name =
        NameKey.BindableProperty;

        static readonly BindablePropertyKey MsgKey =
      BindableProperty.CreateReadOnly("msgFecha", typeof(string),
      typeof(FechaValidatorBehavior), "");

        public static readonly BindableProperty Msg =
              MsgKey.BindableProperty;


      
        public bool FechaValido
        {
            get { return (bool)base.GetValue(Name); }
            private set { base.SetValue(NameKey, value); }
        }


        public string msgFecha
        {
            get { return (string)base.GetValue(Msg); }
            private set { base.SetValue(MsgKey, value); }

        }

        public FechaValidatorBehavior()
        {
          
        }

        protected override void OnAttachedTo(DatePicker picker)
        {
            picker.DateSelected += OnDateChanged;
            base.OnAttachedTo(picker);
        }
        protected override void OnDetachingFrom(DatePicker picker)
        {
            picker.DateSelected -= OnDateChanged;
            base.OnDetachingFrom(picker);
        }

        void OnDateChanged(object sender, DateChangedEventArgs args)
        {
            //se obtiene el año actual y el seleccionado para definir si la fecha seleccionada es válida
            DateTime seleccion = args.NewDate;
            int año_actual = DateTime.Now.Year;
            int año_seleccionado = seleccion.Year;

            int diferencia_años = año_actual - año_seleccionado; 
            FechaValido = false;
            if (diferencia_años < -2)
            {
                FechaValido = false;
                msgFecha = "No puede seleccionar una fecha mayor a dos años en el futuro";
            }
            else 
            {
                FechaValido = true;
                msgFecha = "";
            }  
        }
    }
}