using ENT;

namespace DAL
{
    public class clsListadoCandidatoDAL
    {
        private static List<clsCandidato> candidatos = new List<clsCandidato>()
        {
            new clsCandidato(1, "Paulie Gualtieri"),
            new clsCandidato(2, "Christopher Moltisanti"),
            new clsCandidato(3, "Silvio Dante"),
            new clsCandidato(4, "Vito Spatafore"),
            new clsCandidato(5, "Ralph Cifaretto"),
            new clsCandidato(6, "Furio Giunta"),
            new clsCandidato(7, "Carlo Gervasi"),
            new clsCandidato(8, "Jackie Aprile, Jr"),
            new clsCandidato(9, "Richie Aprile"),
            new clsCandidato(10, "Bobby Baccalieri"),
            new clsCandidato(11, "Phil Leotardo"),
            new clsCandidato(12, "Big Pussy Bonpensiero"),
            new clsCandidato(13, "Benny Fazio"),
            new clsCandidato(14, "Tony Blundetto"),
            new clsCandidato(15, "Little Pauli Germani"),
        };

        /// <summary>
        /// Función que devuelve un listado con todos los candidatos
        /// <br></br>
        /// Pre: Ninguna
        /// <br></br>
        /// Post: Ninguna
        /// </summary>
        /// <returns>Listado completo de candidatos</returns>
        private static List<clsCandidato> obtenerTodosLosCandidatosDAL()
        {
            return candidatos;
        }

        /// <summary>
        /// Función que devuelve un listado con 4 candidatos aleatorios
        /// <br></br>
        /// Pre: ninguna
        /// <br></br>
        /// Post: ninguna
        /// </summary>
        /// <returns>Listado con 4 candidatos aleatorios</returns>
        public static List<clsCandidato> obtenerCandidatosRondaDAL()
        {
            List<clsCandidato> candidatos = obtenerTodosLosCandidatosDAL();
            List<clsCandidato> candidatosAleatorios = [];
            Random aletorio = new Random();

            // No sé si es la mejor forma de hacerlo, pero sabiendo que siempre vamos a añadir 4 candidatos he
            // decidido usar un for
            for (int i = 0; i < 4; i++)
            {
                clsCandidato candidato = null;

                // Hago un do while para evitar candidatos duplicados
                do
                {
                    candidato = candidatos[aletorio.Next(0, candidatos.Count)];
                } while (candidatosAleatorios.Contains(candidato));

                candidatosAleatorios.Add(candidato);
            }

            return candidatosAleatorios;
        }
    }
}
