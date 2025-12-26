using IK.SCP.Application.PDF.Envasado.Model;

namespace IK.SCP.Application.PDF.Helpers;

public static class Calculos
{
    public static object ObtenerValorAtributo(dynamic  objeto, string nombreAtributo)
    {
        IDictionary<string, object> diccionario = objeto;

        if (diccionario.ContainsKey(nombreAtributo))
        {
            return diccionario[nombreAtributo];
        }
        else
        {
            throw new ArgumentException($"El atributo '{nombreAtributo}' no existe en el objeto dinámico.");
        }
    }
    
    public static double ObtenerValorEnPosicion(string cadena, int posicion)
    {
        // Dividir la cadena en base al delimitador "||"
        string[] partes = cadena.Split(new string[] { "||" }, StringSplitOptions.None);

        // Verificar si la posición es válida
        if (posicion >= 0 && posicion < partes.Length)
        {
            // Convertir la subcadena en un número
            return double.Parse(partes[posicion]);
        }
        else
        {
            // Manejar el caso en el que la posición no sea válida
            throw new ArgumentOutOfRangeException("La posición especificada no es válida.");
        }
    }
    
    public static decimal CalcularPesoTotal(List<dynamic> data, string articulo)
    {
        decimal total = 0;

        foreach (var d in data)
        {
            if (d is IDictionary<string, object> dictionary && dictionary.ContainsKey(articulo))
            {
                string[] datos = dictionary[articulo].ToString().Split("||");

                if (datos.Length > 0)
                {
                    if (decimal.TryParse(datos[0], out decimal valor1) && decimal.TryParse(datos[1], out decimal valor2))
                    {
                        total += valor1 * valor2;
                    }
                }
            }
        }

        return total;
    }

    public static decimal? ObtenerValorMerma(List<MermaBlending> lista, string Articulo)
    {
        var merma = lista.FirstOrDefault(p => p.Articulo == Articulo);

        if (merma != null)
        {
            return merma.Merma;
        }

        return null;
    }
}