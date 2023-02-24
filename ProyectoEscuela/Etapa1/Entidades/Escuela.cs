

using System.Text;
using CoreEscuela.Util;

namespace CoreEscuela.Entidades
{

    public class Escuela : ObjetoEscuelaBase
    {
        public int AnioCreacion { get; set; }

        public string? Pais { get; set; }

        public string? Ciudad { get; set; }

        public TiposEscuela TipoEscuela { get; set; }
        public List<Curso> Cursos { get; set; }

        public Escuela(string nombre ) => this.Nombre = nombre;

        public Escuela(string nombre, int anio, string pais, string ciudad, TiposEscuela tipoDEscuela) => (Nombre, AnioCreacion, Pais, Ciudad, TipoEscuela) = (nombre, anio, pais, ciudad, tipoDEscuela);

        
        public void Timbrar()
        {
            Console.Beep(10000, 1000);
        }

        public string ImprimirCursos()
        {

            StringBuilder stringCursos = new StringBuilder();
            stringCursos.Append("========================================\n");
            stringCursos.Append("===        Cursos de la              ===\n");
            stringCursos.Append("===           Escuela                ===\n");
            stringCursos.Append("========================================\n");
            foreach (Curso curso in this.Cursos)
            {


                stringCursos.Append(curso.ToString());
                stringCursos.Append("\n ******************** \n");
            }
            return stringCursos.ToString();
        }

        public static bool EliminarCurso(Escuela escu, string nombreCurso)
        {
            int cant = escu.Cursos.RemoveAll((cur) => cur.Nombre == nombreCurso);
            if (cant >= 1)
            {
                return true;
            }
            return false;
        }



         public override string ToString()
        {
            return $"Nombre: \"{Nombre}\", Tipo: {TipoEscuela} {System.Environment.NewLine} Pais: {Pais}, Ciudad:{Ciudad}";
        }

        public void LimpiarLugar()
        {
             
            Printer.DibujarLinea();
            Console.WriteLine("Limpiando Escuela..");
            
            foreach (var curso in Cursos)
            {
                curso.LimpiarLugar();
            }
            
            Printer.EscribirTitulo($"Escuela {Nombre} Limpia");
        }
    }

}


