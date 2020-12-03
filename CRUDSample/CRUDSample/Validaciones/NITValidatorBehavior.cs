using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace CRUDSample.Validaciones
{
    class NITValidatorBehavior : Behavior<Entry>
    {
        //Se declaran las variables 
        static readonly BindablePropertyKey NITKey =
        BindableProperty.CreateReadOnly("NITValido", typeof(bool),
        typeof(NITValidatorBehavior), false);

        public static readonly BindableProperty NIT =
        NITKey.BindableProperty;

        static readonly BindablePropertyKey MsgKey =
      BindableProperty.CreateReadOnly("msgnit", typeof(string),
      typeof(NITValidatorBehavior), "");

        public static readonly BindableProperty Msg =
              MsgKey.BindableProperty;



        public bool NITValido
        {
            get { return (bool)base.GetValue(NIT); }
            private set { base.SetValue(NITKey, value); }
        }


        public string msgnit
        {
            get { return (string)base.GetValue(Msg); }
            private set { base.SetValue(MsgKey, value); }

        }

        public NITValidatorBehavior()
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
            string nit = ((Entry)sender).Text;
            //inicia con  el texto invalidado
            NITValido = false;
            msgnit = "*Campo Requerido";//Mensaje que se muestra en el label abajo del entry
            Regex formato_nit = new Regex(@"^\d{4}([\s-])?\d{6}\1\d{3}\1\d{1}$");
            //se comprueba el tamaño del texto, tambien si es cero
            if (nit.Length > 17)
            {
                NITValido = false;
                ((Entry)sender).TextColor = NITValido ? Color.Default : Color.Red;//se cambia el color del texto
                msgnit = "Ha excedido el número máximo de caracteres";//Mensaje que se muestra en el label abajo del entry
            }
            else if (nit.Length == 0)
            {
                NITValido = false;
                ((Entry)sender).TextColor = NITValido ? Color.Default : Color.Red;//se cambia el color del texto
                msgnit = "*Campo Requerido";//Mensaje que se muestra en el label abajo del entry

            }
            else
            {
                //Si el texto ingresado y la expresion regular coinciden entonces se aprueba
                if (formato_nit.IsMatch(nit))
                {
                    NITValido = true;
                    ((Entry)sender).TextColor = NITValido ? Color.Default : Color.Default;//el color del texto queda por defecto
                    msgnit = "";
                }
                else
                {
                    NITValido = false;
                    ((Entry)sender).TextColor = NITValido ? Color.Default : Color.Red;
                    msgnit = "Debe seguir éste formato: 0000-000000-000-0";//Mensaje que se muestra en el label abajo del entry
                }

            }
        }


    }
}