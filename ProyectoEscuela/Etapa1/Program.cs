using System;
using CoreEscuela.App;
using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;
namespace CoreEscuela
{

    class Program
    {

        static void Main(string[] args)
        {

        //    AppDomain.CurrentDomain.ProcessExit += AccionDelEvento;

            var engine = new EscuelaEngine();
            engine.Inicializar();

            Printer.EscribirTitulo("BIENVENIDOS A LA ESCUELA");

            var dictmp = engine.GetDiccionarioObjetos();

            //engine.ImprimirDiccionario(dictmp,false);
            var reporteador= new Reporteador(engine.GetDiccionarioObjetos());
            var evalList = reporteador.GetListaEvaluaciones();
            var asigList = reporteador.GetListaAsignaturas();
            var listaEvaluaXASig = reporteador.GetDiccionarioEvaluacionesXAsignatura();


            
        }

        private static void AccionDelEvento(object? sender, EventArgs e)
        {
            Printer.EscribirTitulo("SALIO");
        }

        private static void ImpimirCursosEscuela(Escuela escuela)
        {

            Printer.EscribirTitulo("Cursos de la Escuela");


            if (escuela?.Cursos != null)
            {
                foreach (var curso in escuela.Cursos)
                {
                    WriteLine($"Nombre {curso.Nombre}, Id  {curso.UniqueId}");
                }
            }
        }



    }


}