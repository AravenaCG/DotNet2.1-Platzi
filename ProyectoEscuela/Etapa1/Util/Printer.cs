using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Console;

namespace CoreEscuela.Util
{
    public static class Printer
    {
        public static void DibujarLinea(int tamaño = 10){            
            WriteLine("".PadLeft(tamaño,'='));
        }

        public static void EscribirTitulo(string titulo){
            var tamaño = titulo.Length + 4;
            DibujarLinea(tamaño);
            WriteLine($"| {titulo} |");
            DibujarLinea(tamaño);
        }

         public static void PresioneENTER(){            
            WriteLine("PRESIONE ENTER PARA CONTINUAR");
        }
    }
}