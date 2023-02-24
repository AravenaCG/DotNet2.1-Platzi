using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreEscuela.Entidades;

namespace CoreEscuela.App
{
    public class Reporteador
    {

        Dictionary<LlavesDiccionario, IEnumerable<ObjetoEscuelaBase>> _diccionario;

        public Reporteador(Dictionary<LlavesDiccionario, IEnumerable<ObjetoEscuelaBase>> dicObsEsc)
        {
            if (dicObsEsc == null)
            {
                throw new ArgumentException(nameof(dicObsEsc));
            }
            else
            {
                _diccionario = dicObsEsc;
            }
        }


        public IEnumerable<Evaluación> GetListaEvaluaciones()
        {

            IEnumerable<Evaluación> rta;
            if (_diccionario.TryGetValue(LlavesDiccionario.Evaluación,
            out IEnumerable<ObjetoEscuelaBase> listaEv))
            {
                return rta = listaEv.Cast<Evaluación>();
            }
            else
            {
                return new List<Evaluación>();
            }

        }

        public IEnumerable<Asignatura> GetListaAsignaturas()
        {
            return GetListaAsignaturas(out var dummy);
        }

        public IEnumerable<Asignatura> GetListaAsignaturas(out IEnumerable<Evaluación> listaEvaluaciones)
        {
            listaEvaluaciones = GetListaEvaluaciones();

            return (from Evaluación ev in listaEvaluaciones
                    select ev.Asignatura).Distinct();
        }

        public Dictionary<string, IEnumerable<Evaluación>> GetDiccionarioEvaluacionesXAsignatura()
        {
            Dictionary<string, IEnumerable<Evaluación>> diccionarioRta = new Dictionary<string, IEnumerable<Evaluación>>();

            var listaAsig = GetListaAsignaturas(out var listaEval);

            foreach (var asignatura in listaAsig)
            {
                var evalsAsig = from evaluacion in listaEval
                    where evaluacion.Asignatura.Nombre == asignatura.Nombre 
                    select evaluacion;
                diccionarioRta.Add(asignatura.Nombre, evalsAsig);
            }

            return diccionarioRta;


        }

        public Dictionary<string, IEnumerable<Object>> GetPromedioAlumnoXAsignatura(){
            var rta = new Dictionary<string, IEnumerable<Object>>();

            var dicEvalXAsig = GetDiccionarioEvaluacionesXAsignatura();
           
            foreach (var asigConEval in dicEvalXAsig)
            {
                var promediosAlumnos = from eval in asigConEval.Value
                            group eval by new {
                                eval.Alumno.UniqueId, eval.Alumno.Nombre} into grupoEvaluacionesAlumno
                            select new AlumnoPromedio {
                                
                               alumnoId = grupoEvaluacionesAlumno.Key.UniqueId,
                               alumnoNombre= grupoEvaluacionesAlumno.Key.Nombre,
                               promedio = grupoEvaluacionesAlumno.Average( evaluacion => evaluacion.Nota),
                                
                                };
                
                
                rta.Add(asigConEval.Key, promediosAlumnos);
            }


            return rta;

        }
    

    }
}