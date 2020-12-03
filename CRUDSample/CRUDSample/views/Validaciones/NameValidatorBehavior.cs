using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace CRUDSample.Validaciones
{
    class NameValidatorBehavior : Behavior<Entry>
    {
        //Se declaran las variables 
       static readonly BindablePropertyKey NameKey =
       BindableProperty.CreateReadOnly("Valido", typeof(bool),
       typeof(NameValidatorBehavior), false);

        public static readonly BindableProperty Name =
        NameKey.BindableProperty;

        static readonly BindablePropertyKey MsgKey =
      BindableProperty.CreateReadOnly("msg", typeof(string),
      typeof(NameValidatorBehavior), "");

        public static readonly BindableProperty Msg =
              MsgKey.BindableProperty;


      
        public bool Valido
        {
            get { return (bool)base.GetValue(Name); }
            private set { base.SetValue(NameKey, value); }
        }


        public string msg
        {
            get { return (string)base.GetValue(Msg); }
            private set { base.SetValue(MsgKey, value); }

        }

        public NameValidatorBehavior()
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
            string nombre = ((Entry)sender).Text;
            //inicia con  el texto invalidado
            Valido = false;
            msg = "*Campo Requerido";//Mensaje que se muestra en el label abajo del entry
            //expresion regular para comparar el texto ingresado
            Regex alfabeto = new Regex(@"^[\sa-zA-ZäÄëËïÏöÖüÜáéíóúáéíóúÁÉÍÓÚÂÊÎÔÛâêîôûàèìòùÀÈÌÒÙÑñ]+$");
            //se comprueba el tamaño del texto, tambien si es cero
            if (nombre.Length > 20)
            {
                Valido = false;
                ((Entry)sender).TextColor = Valido ? Color.Default : Color.Red;//se cambia el color del texto
                msg = "20 Caracteres Máximo";//Mensaje que se muestra en el label abajo del entry
            }
            else if (nombre.Length == 0)
            {
                Valido = false;
                ((Entry)sender).TextColor = Valido ? Color.Default : Color.Red;//se cambia el color del texto
                msg = "*Campo Requerido";//Mensaje que se muestra en el label abajo del entry

            }                   
            else
            {
                //Si el texto ingresado y la expresion regular coinciden entonces se aprueba
                if (alfabeto.IsMatch(nombre))
                {
                    Valido = true;
                    ((Entry)sender).TextColor = Valido ? Color.Default : Color.Default;//el color del texto queda por defecto
                    msg = "";
                }
                else
                {
                    Valido = false;
                    ((Entry)sender).TextColor = Valido ? Color.Default : Color.Red;//se cambia el color del texto
                    msg = "*No se permiten números";//Mensaje que se muestra en el label abajo del entry
                }

            }     
        }


    }
}