using CoreEscuela.Entidades;
using CoreEscuela.Util;

namespace CoreEscuela.App
{
    public sealed class EscuelaEngine
    {

        public CoreEscuela.Entidades.Escuela Escuela { get; set; }

        public EscuelaEngine()
        {

        }

        public void Inicializar()
        {
            Escuela = new Entidades.Escuela("Scalabritney Ortiz", 1850, "Argentina", "BsAs", TiposEscuela.Primaria);
            CargarCursos();
            CargarAsignaturas();
            CargarEvaluaciones();

        }

        private void CargarEvaluaciones()
        {
            var lista = new List<Evaluación>();

            foreach (var curso in Escuela.Cursos)
            {
                foreach (var asignatura in curso.Asignaturas)
                {
                    foreach (var alumno in curso.Alumnos)
                    {

                        var rnd = new Random();
                        for (int i = 0; i < 5; i++)
                        {
                            var evalu = new Evaluación
                            {
                                Asignatura = asignatura,
                                Nombre = $"{asignatura.Nombre} Ev#{i + 1}",
                                Nota = MathF.Round((float)(5 * rnd.NextDouble()),2),
                                Alumno = alumno
                            };
                            alumno.Evaluaciones.Add(evalu);
                            lista.Add(evalu);
                        }
                    }
                    {

                    }

                }
            }
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            out int conteoEvaluaciones,
            out int conteoAsignaturas,
            out int conteoCursos,
            out int conteoAlumnos,
            bool traeEvaluaciones = true,
            bool traerAlumnos = true,
            bool traerAsignaturas = true,
            bool traerCursos = true
            )
        {
            conteoEvaluaciones = conteoAlumnos = conteoAsignaturas = conteoCursos = 0;

            var listaObj = new List<ObjetoEscuelaBase>();
            listaObj.Add(Escuela);

            if (traerCursos)
                listaObj.AddRange(Escuela.Cursos);

            conteoCursos = Escuela.Cursos.Count;

            foreach (var curso in Escuela.Cursos)
            {
                conteoAsignaturas += curso.Asignaturas.Count;
                conteoAlumnos += curso.Alumnos.Count;

                if (traerAsignaturas)
                    listaObj.AddRange(curso.Asignaturas);


                if (traerAlumnos)
                    listaObj.AddRange(curso.Alumnos);

                if (traeEvaluaciones)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        listaObj.AddRange(alumno.Evaluaciones);
                        conteoEvaluaciones += alumno.Evaluaciones.Count;
                    }
                }

            }

            return listaObj.AsReadOnly();
        }



        private void CargarAsignaturas()
        {
            List<Asignatura> listaAsignaturas = new List<Asignatura>(){
                new Asignatura{Nombre="Matematica"},
                new Asignatura{Nombre="Educación Fisica"},
                new Asignatura{Nombre="Castellano"},
                new Asignatura{Nombre="Ciencias Naturales"},
                new Asignatura{Nombre="Quimica"},
            };
            foreach (var curso in Escuela.Cursos)
            {
                curso.Asignaturas = listaAsignaturas;
            }
        }

        private List<Alumno> GenerarAlumnosAlAzar(int cantidad)
        {
            string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
            string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
            string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

            var listaAlumnos = from n1 in nombre1
                               from n2 in nombre2
                               from a1 in apellido1
                               select new Alumno { Nombre = $"{n1} {n2} {a1}" };

            return listaAlumnos.OrderBy((al) => al.UniqueId).Take(cantidad).ToList();
        }


        private void CargarCursos()
        {
            Escuela.Cursos = new List<Curso>(){
                new Curso("101",TiposJornada.Mañana),
                new Curso("201", TiposJornada.Mañana),
                new Curso("301",  TiposJornada.Mañana),
                new Curso("401",  TiposJornada.Tarde),
                new Curso("501",  TiposJornada.Tarde)
            };

            Random rnd = new Random();
            foreach (var c in Escuela.Cursos)
            {
                int cantRandom = rnd.Next(5, 20);
                c.Alumnos = GenerarAlumnosAlAzar(cantRandom);
            }

        }

        public Dictionary<LlavesDiccionario, IEnumerable<ObjetoEscuelaBase>> GetDiccionarioObjetos()
        {


            var dic = new Dictionary<LlavesDiccionario, IEnumerable<ObjetoEscuelaBase>>();

            dic.Add(LlavesDiccionario.Escuela, new[] { Escuela });
            dic.Add(LlavesDiccionario.Curso, Escuela.Cursos.Cast<ObjetoEscuelaBase>());
            var listatemp = new List<Evaluación>();
            var listaTmpAsig = new List<Asignatura>();
            var listatmpAlum = new List<Alumno>();

            foreach (var cur in Escuela.Cursos)
            {
                listaTmpAsig.AddRange(cur.Asignaturas);
                listatmpAlum.AddRange(cur.Alumnos);


                foreach (var alum in cur.Alumnos)
                {

                    listatemp.AddRange(alum.Evaluaciones);
                }

            }
            dic.Add(LlavesDiccionario.Evaluación, listatemp.Cast<ObjetoEscuelaBase>());
            dic.Add(LlavesDiccionario.Asignatura, listaTmpAsig.Cast<ObjetoEscuelaBase>());
            dic.Add(LlavesDiccionario.Alumno, listatmpAlum.Cast<ObjetoEscuelaBase>());

            return dic;
        }


        public void ImprimirDiccionario(Dictionary<LlavesDiccionario, IEnumerable<ObjetoEscuelaBase>> dic,
                      bool imprEval = false)
        {
            foreach (var objdic in dic)
            {
                Printer.EscribirTitulo(objdic.Key.ToString());

                foreach (var val in objdic.Value)
                {
                    switch (objdic.Key)
                    {
                        case LlavesDiccionario.Evaluación:
                            if (imprEval)
                                Console.WriteLine(val);
                            break;
                        case LlavesDiccionario.Escuela:
                            Console.WriteLine("Escuela: " + val);
                            break;
                        case LlavesDiccionario.Alumno:
                            Console.WriteLine("Alumno: " + val.Nombre);
                            break;
                        case LlavesDiccionario.Curso:
                            var curtmp = val as Curso;
                            if (curtmp != null)
                            {
                                int count = curtmp.Alumnos.Count;
                                Console.WriteLine("Curso: " + val.Nombre + " Cantidad Alumnos: " + count);
                            }
                            break;
                        default:
                            Console.WriteLine(val);
                            break;
                    }
                }
            }
        }


    }



}