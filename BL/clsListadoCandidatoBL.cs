using DAL;
using ENT;

namespace BL
{
    public class clsListadoCandidatoBL
    {
        /// <summary>
        /// Función que devuelve un listado con 4 candidatos aleatorios
        /// <br></br>
        /// Pre: ninguna
        /// <br></br>
        /// Post: ninguna
        /// </summary>
        /// <returns>Listado con 4 candidatos aleatorios</returns>
        public static List<clsCandidato> obtenerCandidatosRondaBL()
        {
            return clsListadoCandidatoDAL.obtenerCandidatosRondaDAL();
        }
    }
}
