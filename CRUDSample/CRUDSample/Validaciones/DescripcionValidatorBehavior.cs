using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace CRUDSample.Validaciones
{
    class DescripcionValidatorBehavior : Behavior<Entry>
    {
        //Se declaran las variables 
       static readonly BindablePropertyKey NameKey =
       BindableProperty.CreateReadOnly("DescripcionValido", typeof(bool),
       typeof(DescripcionValidatorBehavior), false);

        public static readonly BindableProperty Name =
        NameKey.BindableProperty;

        static readonly BindablePropertyKey MsgKey =
      BindableProperty.CreateReadOnly("msgDescripcion", typeof(string),
      typeof(DescripcionValidatorBehavior), "");

        public static readonly BindableProperty Msg =
              MsgKey.BindableProperty;


      
        public bool DescripcionValido
        {
            get { return (bool)base.GetValue(Name); }
            private set { base.SetValue(NameKey, value); }
        }


        public string msgDescripcion
        {
            get { return (string)base.GetValue(Msg); }
            private set { base.SetValue(MsgKey, value); }

        }

        public DescripcionValidatorBehavior()
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
            string descripcion = ((Entry)sender).Text;
            //inicia con  el texto invalidado
            DescripcionValido = false;
            msgDescripcion = "*Campo Requerido";//Mensaje que se muestra en el label abajo del entry
            //expresion regular para comparar el texto ingresado
          //  Regex alfabeto = new Regex(@"^[\s0-9a-zA-ZäÄëËïÏöÖüÜáéíóúáéíóúÁÉÍÓÚÂÊÎÔÛâêîôûàèìòùÀÈÌÒÙÑñ]+$");
            //se comprueba el tamaño del texto, tambien si es cero
            if (descripcion.Length > 50)
            {
                DescripcionValido = false;
                ((Entry)sender).TextColor = DescripcionValido ? Color.Default : Color.Red;//se cambia el color del texto
                msgDescripcion = "50 Caracteres Máximo";//Mensaje que se muestra en el label abajo del entry
            }
            else if (descripcion.Length == 0)
            {
                DescripcionValido = false;
                ((Entry)sender).TextColor = DescripcionValido ? Color.Default : Color.Red;//se cambia el color del texto
                msgDescripcion = "*Campo Requerido";//Mensaje que se muestra en el label abajo del entry

            }                   
            else
            {
                
                    DescripcionValido = true;
                    ((Entry)sender).TextColor = DescripcionValido ? Color.Default : Color.Default;//el color del texto queda por defecto
                    msgDescripcion = "";
              
            }     
        }


    }
}