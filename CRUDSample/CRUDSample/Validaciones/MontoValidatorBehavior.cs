using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace CRUDSample.Validaciones
{
    class MontoValidatorBehavior : Behavior<Entry>
    {
        //Se declaran las variables 
       static readonly BindablePropertyKey NameKey =
       BindableProperty.CreateReadOnly("MontoValido", typeof(bool),
       typeof(MontoValidatorBehavior), false);

        public static readonly BindableProperty Name =
        NameKey.BindableProperty;

        static readonly BindablePropertyKey MsgKey =
      BindableProperty.CreateReadOnly("msgMonto", typeof(string),
      typeof(MontoValidatorBehavior), "");

        public static readonly BindableProperty Msg =
              MsgKey.BindableProperty;


      
        public bool MontoValido
        {
            get { return (bool)base.GetValue(Name); }
            private set { base.SetValue(NameKey, value); }
        }


        public string msgMonto
        {
            get { return (string)base.GetValue(Msg); }
            private set { base.SetValue(MsgKey, value); }

        }

        public MontoValidatorBehavior()
        {
          
        }

        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }
        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }
        void OnEntryTextChanged(object sender, TextChangedEventArgs args)
        {

           //Se obtiene el texto del entry
            string numero = ((Entry)sender).Text;
            //inicia con  el texto invalidado
            MontoValido = false;
            msgMonto = "*Campo Requerido";//Mensaje que se muestra en el label abajo del entry
            //expresion regular para comparar el texto ingresado ^(0|0?[1-9]\d*)\.\d\d$
            Regex decimales = new Regex(@"^\d+(\.\d{1,2})?$");
            //se comprueba el tamaño del texto, tambien si es cero
            if (numero.Length > 20)
            {
                MontoValido = false;
                ((Entry)sender).TextColor = MontoValido ? Color.Default : Color.Red;//se cambia el color del texto
                msgMonto = "20 Caracteres Máximo";//Mensaje que se muestra en el label abajo del entry
            }
            else if (numero.Length == 0)
            {
                MontoValido = false;
                ((Entry)sender).TextColor = MontoValido ? Color.Default : Color.Red;//se cambia el color del texto
                msgMonto = "*Campo Requerido";//Mensaje que se muestra en el label abajo del entry

            }                   
            else
            {
                //Si el texto ingresado y la expresion regular coinciden entonces se aprueba
                if (decimales.IsMatch(numero))
                {
                    MontoValido = true;
                    ((Entry)sender).TextColor = MontoValido ? Color.Default : Color.Default;//el color del texto queda por defecto
                    msgMonto = "";
                }
                else
                {
                    MontoValido = false;
                    ((Entry)sender).TextColor = MontoValido ? Color.Default : Color.Red;//se cambia el color del texto
                    msgMonto = "*Sólo se permiten números (máximo dos decimales después del punto)";//Mensaje que se muestra en el label abajo del entry
                }

            }     
        }


    }
}