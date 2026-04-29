using System.Text.RegularExpressions;

namespace quickassist.Utils
{
    public class CsnFunctions
    {
        /// <summary>
        /// Funcion encargada de eliminar espacios adicionales en el nombre de un archivo.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string SanitizeFileName(string fileName)
        {
            // Elimina espacios al inicio y al final
            fileName = fileName.Trim().ToLower();

            // Reemplaza los espacios por guiones bajos
            fileName = fileName.Replace(" ", "-");

            // Elimina cualquier espacio adicional
            fileName = Regex.Replace(fileName, @"\s+", "-");

            // Eliminamos caracteres especiales
            fileName = Regex.Replace(fileName, @"[^a-zA-Z0-9._-]", "-");

            return fileName;
        }

        /// <summary>
        /// Funcion encargada de validar el ambiente de ejecucion, si es produccion o desarrollo, para establecer los valores de la cadena de conexion dependiendo del entorno.
        /// </summary>
        /// <returns></returns>
        public static bool IsProduction()
        {
            //Revisar si el environment es enviado o existe en el servidor
            string? netCoreEnvironment = Environment.GetEnvironmentVariable("ENVIRONMENT_METHOD");

            if (!string.IsNullOrEmpty(netCoreEnvironment) && netCoreEnvironment.ToLower().Equals(CsnConstants.PRODUCTION))
                return true;

            return false;
        }

        /// <summary>
        /// Establece los valores en la cadena de conexión dependiendo del entorno.
        /// </summary>
        /// <param name="str">cadena de conexión</param>
        /// <returns>cadena de conexión con valores del servidor</returns>
        public static string ConnectionString(string str)
        {
            if (string.IsNullOrEmpty(str))
                return "";


            if (!IsProduction())
                return str.Replace("SERVER_DB_HOST", Environment.GetEnvironmentVariable("SERVER_DB_HOST"))
                    .Replace("SERVER_DB_NAME", Environment.GetEnvironmentVariable("SERVER_DB_NAME"))
                    .Replace("SERVER_DB_USER", Environment.GetEnvironmentVariable("SERVER_DB_USER"))
                    .Replace("SERVER_DB_PASS", Environment.GetEnvironmentVariable("SERVER_DB_PASS"))
                    .Replace("SERVER_DB_PORT", Environment.GetEnvironmentVariable("SERVER_DB_PORT"));


            return str.Replace("SERVER_DB_HOST", Environment.GetEnvironmentVariable("SERVER_DB_HOST"))
                .Replace("SERVER_DB_NAME", Environment.GetEnvironmentVariable("SERVER_DB_NAME"))
                .Replace("SERVER_DB_USER", Environment.GetEnvironmentVariable("SERVER_DB_USER"))
                .Replace("SERVER_DB_PASS", Environment.GetEnvironmentVariable("SERVER_DB_PASS"))
                .Replace("SERVER_DB_PORT", Environment.GetEnvironmentVariable("SERVER_DB_PORT"));
        }
    }
}
