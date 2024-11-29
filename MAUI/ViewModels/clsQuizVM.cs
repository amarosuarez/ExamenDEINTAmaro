using BL;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using ENT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.ViewModels
{
    public class clsQuizVM : INotifyPropertyChanged
    {
        #region Atributos
        private List<clsCandidato> candidatos;
        private clsCandidato candidatoRespuesta;
        private clsCandidato candidatoSeleccionado;
        private String foto;
        private int aciertos;
        private int errores;
        private int rondas = 1;
        private List<clsCandidato> candidatosJugados = new List<clsCandidato>();
        private String error;
        private bool showError;
        private bool showContent;
        #endregion

        #region Propiedades
        public List<clsCandidato> Candidatos
        {
            get { return candidatos; }
        }
        public clsCandidato CandidatoRespuesta
        {
            get { return candidatoRespuesta; }
        }

        public clsCandidato CandidatoSeleccionado {
            get {
                return candidatoSeleccionado;
            }

            set {
                if (value != null)
                {
                    candidatoSeleccionado = value;
                    compruebaCandidato();
                }
            }
        }

        public String Foto
        {
            get {
                return foto;
            }
        }

        public int Aciertos
        {
            get { return aciertos; }
        }

        public int Errores
        {
            get { return errores; }
        }

        public int Rondas
        {
            get { return rondas; }
        }

        public String Error
        {
            get { return error; }
        }

        public bool ShowError
        {
            get { return showError; }
        }

        public bool ShowContent
        {
            get { return !showError; }
        }
        #endregion

        #region Constructores
        public clsQuizVM()
        {
            try
            {
                //throw new Exception();
                juego();
            }
            catch (Exception e)
            {
                error = "Ha ocurrido un error";
                showError = true;
                NotifyPropertyChanged(nameof(Error));
                NotifyPropertyChanged(nameof(ShowError));
            }
        }
        #endregion

        #region Metodos

        /// <summary>
        /// Función que inicia el juego, seleccionando 4 candidatos para esa ronda y al candidato a adivinar
        /// <br></br>
        /// Pre: Ninguna
        /// <br></br>
        /// Post: Ninguna
        /// </summary>
        private void juego()
        {
            candidatos = clsListadoCandidatoBL.obtenerCandidatosRondaBL();
            NotifyPropertyChanged(nameof(Candidatos));
            seleccionarCandidato();
        }

        /// <summary>
        /// Función que selecciona al candidato a adivinar en esa ronda y lo añade a los candidato jugados
        /// <br></br>
        /// Pre: Ninguna
        /// <br></br>
        /// Post: Ninguna
        /// </summary>
        private void seleccionarCandidato() {
            Random aletorio = new Random();
            int numAle;

            // Hago un do while para evitar jugar con candidatos ya jugados anteriormente
            do
            {
                numAle = aletorio.Next(0, 4);
                candidatoRespuesta = candidatos[numAle];

            } while (CandidatoRespuesta != null && candidatosJugados != null && candidatosJugados.Contains(candidatoRespuesta));

            // Coloco la ruta de la foto y añado al candidato
            foto = "f" + CandidatoRespuesta.Id + "f.jfif";
            NotifyPropertyChanged(nameof(Foto));
            candidatosJugados.Add(candidatoRespuesta);
        }

        /// <summary>
        /// Función que comprueba si el candidato seleccionado es el candidato a adivinar.
        /// Si acierta se suma un acierto, en caso contrario se suma un error
        /// Se suma una ronda siempre y cuando se juegan 5 rondas le da al usuario la opción 
        /// de volver a jugar o de salir del juego.
        /// <br></br>
        /// Pre: Ninguna
        /// <br></br>
        /// Post: Ninguna
        /// </summary>
        private async void compruebaCandidato()
        {
            // Compruebo si ha acertado
            bool acerto = candidatoSeleccionado.Id == candidatoRespuesta.Id;
            String mensaje;

            // Construyo el mensaje y sumo 1 a error o a acierto
            if (acerto)
            {
                mensaje = "Has acertado";
                aciertos++;
                NotifyPropertyChanged(nameof(Aciertos));
            } else {
                mensaje = "Has fallado";
                errores++;
                NotifyPropertyChanged(nameof(Errores));
            }

            // Muestro el toast con el mensaje
            CancellationTokenSource token = new CancellationTokenSource();
            var toast = Toast.Make(mensaje, ToastDuration.Short, 14);
            await toast.Show(token.Token);

            // Compruebo si era la última ronda
            if (rondas == 5)
            {
                // Construyo el mensaje de si ha ganado o ha perdido
                String mensajeFinal = "Has perdido...";

                if (aciertos >= 3)
                {
                    mensajeFinal = "Has ganado!!!";
                }

                // Le doy al usuario la opción de volver a jugar o salir
                bool confirmacion = await Application.Current.MainPage.DisplayAlert(
                    mensajeFinal,
                    "¿Quieres volver a jugar?",
                    "Sí",
                    "No");

                if (confirmacion)
                {
                    volverAJugar();
                } else
                {
                    Application.Current.Quit();
                }
            } else
            {
                // Sumo una ronda y vuelvo a ejecutar la función juego para generar una ronda nueva
                rondas++;

                NotifyPropertyChanged(nameof(Rondas));
                juego();
            }
        }

        /// <summary>
        /// Función que resetea todos los contadores y genera una nueva ronda
        /// <br></br>
        /// Pre: Ninguna
        /// <br></br>
        /// Post: Ninguna
        /// </summary>
        private void volverAJugar()
        {
            candidatosJugados.Clear();
            aciertos = 0;
            errores = 0;
            rondas = 1;
            NotifyPropertyChanged(nameof(Aciertos));
            NotifyPropertyChanged(nameof(Errores));
            NotifyPropertyChanged(nameof(Rondas));
            juego();
        }
        #endregion

        #region Notify
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")

        {

            PropertyChanged?.Invoke(this, new
            PropertyChangedEventArgs(propertyName));

        }
        #endregion
    }
}
