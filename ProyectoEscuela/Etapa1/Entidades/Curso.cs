using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreEscuela.Util;

namespace CoreEscuela.Entidades
{
    public class Curso : ObjetoEscuelaBase, ILugar
    {

        public TiposJornada Jornada { get; set; }
        public List<Asignatura> Asignaturas { get; set; }
        public List<Alumno> Alumnos { get; set; }

        public Curso(string nombre, TiposJornada tiposJornada) => (Nombre, Jornada) = (nombre, tiposJornada);


        public override string ToString()
        {
            return $"Nombre: {Nombre}, Id: {UniqueId}. \n Jornada: {Jornada}";
        }


        public string Dirección { get; set; }

        public void LimpiarLugar()
        {
            Printer.DibujarLinea();
            Console.WriteLine("Limpiando Establecimiento...");
            Console.WriteLine($"Curso {Nombre} Limpio");
        }







    }
}