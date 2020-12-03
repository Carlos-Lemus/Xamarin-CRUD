using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace CRUDSample.Validaciones
{
    class DUIValidatorBehavior : Behavior<Entry>
    {
        //Se declaran las variables 
        static readonly BindablePropertyKey DUIKey =
       BindableProperty.CreateReadOnly("DUIValido", typeof(bool),
       typeof(DUIValidatorBehavior), false);

        public static readonly BindableProperty DUI =
        DUIKey.BindableProperty;

        static readonly BindablePropertyKey MsgKey =
      BindableProperty.CreateReadOnly("msgdui", typeof(string),
      typeof(DUIValidatorBehavior), "");

        public static readonly BindableProperty Msg =
              MsgKey.BindableProperty;



        public bool DUIValido
        {
            get { return (bool)base.GetValue(DUI); }
            private set { base.SetValue(DUIKey, value); }
        }


        public string msgdui
        {
            get { return (string)base.GetValue(Msg); }
            private set { base.SetValue(MsgKey, value); }

        }

        public DUIValidatorBehavior()
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
            string dui = ((Entry)sender).Text;
            //inicia con  el texto invalidado
            DUIValido = false;
            msgdui = "*Campo Requerido";//Mensaje que se muestra en el label abajo del entry
            //expresion regular para comparar el texto ingresado
            Regex numeros_guion = new Regex(@"^\d{8}([\s-])?\d{1}$");
            //se comprueba el tamaño del texto, tambien si es cero
            if (dui.Length > 20)
            {
                DUIValido = false;
                ((Entry)sender).TextColor = DUIValido ? Color.Default : Color.Red;//se cambia el color del texto
                msgdui = "Ha excedido el número máximo de caracteres";//Mensaje que se muestra en el label abajo del entry
            }
            else if (dui.Length == 0)
            {
                DUIValido = false;
                ((Entry)sender).TextColor = DUIValido ? Color.Default : Color.Red;//se cambia el color del texto
                msgdui = "*Campo Requerido";//Mensaje que se muestra en el label abajo del entry

            }                   
            else
            {
                //Si el texto ingresado y la expresion regular coinciden entonces se aprueba
                if (numeros_guion.IsMatch(dui))
                {
                    DUIValido = true;
                    ((Entry)sender).TextColor = DUIValido ? Color.Default : Color.Default;//el color del texto queda por defecto
                    msgdui = "";
                }
                else
                {
                    DUIValido = false;
                    ((Entry)sender).TextColor = DUIValido ? Color.Default : Color.Red;//se cambia el color del texto
                    msgdui = "Debe seguir éste formato: 00000000-0";//Mensaje que se muestra en el label abajo del entry
                }

            }     
        }


    }
}