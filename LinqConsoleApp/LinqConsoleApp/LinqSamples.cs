using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Xml.Schema;

namespace LinqConsoleApp
{
    public class LinqSamples
    {
        public static IEnumerable<Emp> Emps { get; set; }
        public static IEnumerable<Dept> Depts { get; set; }

        public LinqSamples()
        {
            LoadData();
        }

        public void LoadData()
        {
            var empsCol = new List<Emp>();
            var deptsCol = new List<Dept>();

            #region Load depts
            var d1 = new Dept
            {
                Deptno = 1,
                Dname = "Research",
                Loc = "Warsaw"
            };

            var d2 = new Dept
            {
                Deptno = 2,
                Dname = "Human Resources",
                Loc = "New York"
            };

            var d3 = new Dept
            {
                Deptno = 3,
                Dname = "IT",
                Loc = "Los Angeles"
            };

            deptsCol.Add(d1);
            deptsCol.Add(d2);
            deptsCol.Add(d3);
            Depts = deptsCol;
            #endregion

            #region Load emps
            var e1 = new Emp
            {
                Deptno = 1,
                Empno = 1,
                Ename = "Jan Kowalski",
                HireDate = DateTime.Now.AddMonths(-5),
                Job = "Backend programmer",
                Mgr = null,
                Salary = 2000
            };

            var e2 = new Emp
            {
                Deptno = 1,
                Empno = 20,
                Ename = "Anna Malewska",
                HireDate = DateTime.Now.AddMonths(-7),
                Job = "Frontend programmer",
                Mgr = e1,
                Salary = 4000
            };

            var e3 = new Emp
            {
                Deptno = 1,
                Empno = 2,
                Ename = "Marcin Korewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Frontend programmer",
                Mgr = null,
                Salary = 5000
            };

            var e4 = new Emp
            {
                Deptno = 2,
                Empno = 3,
                Ename = "Paweł Latowski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Frontend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e5 = new Emp
            {
                Deptno = 2,
                Empno = 4,
                Ename = "Michał Kowalski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Backend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e6 = new Emp
            {
                Deptno = 2,
                Empno = 5,
                Ename = "Katarzyna Malewska",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Manager",
                Mgr = null,
                Salary = 8000
            };

            var e7 = new Emp
            {
                Deptno = null,
                Empno = 6,
                Ename = "Andrzej Kwiatkowski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "System administrator",
                Mgr = null,
                Salary = 7500
            };

            var e8 = new Emp
            {
                Deptno = 2,
                Empno = 7,
                Ename = "Marcin Polewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Mobile developer",
                Mgr = null,
                Salary = 4000
            };

            var e9 = new Emp
            {
                Deptno = 2,
                Empno = 8,
                Ename = "Władysław Torzewski",
                HireDate = DateTime.Now.AddMonths(-9),
                Job = "CTO",
                Mgr = null,
                Salary = 12000
            };

            var e10 = new Emp
            {
                Deptno = 2,
                Empno = 9,
                Ename = "Andrzej Dalewski",
                HireDate = DateTime.Now.AddMonths(-4),
                Job = "Database administrator",
                Mgr = null,
                Salary = 9000
            };

            empsCol.Add(e1);
            empsCol.Add(e2);
            empsCol.Add(e3);
            empsCol.Add(e4);
            empsCol.Add(e5);
            empsCol.Add(e6);
            empsCol.Add(e7);
            empsCol.Add(e8);
            empsCol.Add(e9);
            empsCol.Add(e10);
            Emps = empsCol;

            #endregion

        }


        /*
            Celem ćwiczenia jest uzupełnienie poniższych metod.
         *  Każda metoda powinna zawierać kod C#, który z pomocą LINQ'a będzie realizować
         *  zapytania opisane za pomocą SQL'a.
        */

        /// <summary>
        /// SELECT * FROM Emps WHERE Job = "Backend programmer";
        /// </summary>
        public void Przyklad1()
        {

            //1. Query syntax (SQL)
            var res = from emp in Emps
                      where emp.Job == "Backend programmer"
                      select new
                      {
                          Empno = emp.Empno,
                          Ename = emp.Ename,
                          Salary = emp.Salary,
                          HireDate = emp.HireDate,
                          Job = emp.Job,
                          Deptno = emp.Deptno,
                          Mgr = emp.Mgr
                      };

            foreach (var Emp in res) {
                Console.WriteLine(Emp.ToString());
            }
            Console.WriteLine();

            //2. Lambda and Extension methods

            var res2 = Emps.Where(emp => emp.Job == "Backend programmer")
                           .Select(emp => (new
                           {
                               Empno = emp.Empno,
                               Ename = emp.Ename,
                               Salary = emp.Salary,
                               HireDate = emp.HireDate,
                               Job = emp.Job,
                               Deptno = emp.Deptno,
                               Mgr = emp.Mgr
                           }));
            
            foreach (var Emp in res2)
            {
                Console.WriteLine(Emp.ToString());
            }

        }

        /// <summary>
        /// SELECT * FROM Emps Where Job = "Frontend programmer" AND Salary>1000 ORDER BY Ename DESC;
        /// </summary>
        public void Przyklad2()
        {
            //1. Query syntax (SQL)
            var res = from emp in Emps
                      where emp.Job == "Frontend programmer" && emp.Salary > 1000
                      orderby emp.Ename descending
                      select new
                      {
                          Empno = emp.Empno,
                          Ename = emp.Ename,
                          Salary = emp.Salary,
                          HireDate = emp.HireDate,
                          Job = emp.Job,
                          Deptno = emp.Deptno,
                          Mgr = emp.Mgr
                      };

            foreach (var Emp in res)
            {
                Console.WriteLine(Emp.ToString());
            }
            Console.WriteLine();

            //2. Lambda and Extension methods
            var res2 = Emps.Where(emp => emp.Job == "Frontend programmer" && emp.Salary > 1000)
                            .Select(emp => (new
                            {
                                emp.Empno,
                                emp.Ename,
                                emp.Salary,
                                emp.HireDate,
                                emp.Job,
                                emp.Deptno,
                                emp.Mgr
                            })).OrderByDescending(emp => emp.Ename);

            foreach (var Emp in res2)
            {
                Console.WriteLine(Emp.ToString());
            }

        }

        /// <summary>
        /// SELECT MAX(Salary) FROM Emps;
        /// </summary>
        public void Przyklad3()
        {
            //1. Query syntax (SQL)
            var res = (from emp in Emps
                      select emp.Salary).Max();

                Console.WriteLine(res);
            Console.WriteLine();

            //2. Lambda and Extension methods
            var res2 = Emps.Select(emp => (emp.Salary)).Max();
            Console.WriteLine(res2);

        }

    /// <summary>
    /// SELECT * FROM Emps WHERE Salary=(SELECT MAX(Salary) FROM Emps);
    /// </summary>
    public void Przyklad4()
        {
            //1. Query syntax (SQL)
            var res = from emp in Emps
                      where emp.Salary == ((from empIN in Emps
                                            select empIN.Salary).Max())
                      select new
                      {
                          Empno = emp.Empno,
                          Ename = emp.Ename,
                          Salary = emp.Salary,
                          HireDate = emp.HireDate,
                          Job = emp.Job,
                          Deptno = emp.Deptno,
                          Mgr = emp.Mgr
                      };

            foreach (var Emp in res)
            {
                Console.WriteLine(Emp.ToString());
            }
            Console.WriteLine();

            //2. Lambda and Extension methods
            var res2 = Emps.Where(emp => emp.Salary == (Emps.Select(emp => (emp.Salary)).Max()))
                            .Select(emp => (new
                            {
                                emp.Empno,
                                emp.Ename,
                                emp.Salary,
                                emp.HireDate,
                                emp.Job,
                                emp.Deptno,
                                emp.Mgr
                            }));

            foreach (var Emp in res2)
            {
                Console.WriteLine(Emp.ToString());
            }

        }

        /// <summary>
        /// SELECT ename AS Nazwisko, job AS Praca FROM Emps;
        /// </summary>
        public void Przyklad5()
        {
            //1. Query syntax (SQL)
            var res = from emp in Emps
                      select new
                      {
                          Nazwisko = emp.Ename,
                          Praca = emp.Job,
                      };

            foreach (var Emp in res)
            {
                Console.WriteLine(Emp.ToString());
            }
            Console.WriteLine();

            //2. Lambda and Extension methods
            var res2 = Emps.Select(emp => (new
                            {
                                Nazwisko = emp.Ename,
                                Praca = emp.Job,
                            }));

            foreach (var Emp in res2)
            {
                Console.WriteLine(Emp.ToString());
            }

        }

        /// <summary>
        /// SELECT Emps.Ename, Emps.Job, Depts.Dname FROM Emps
        /// INNER JOIN Depts ON Emps.Deptno=Depts.Deptno
        /// Rezultat: Złączenie kolekcji Emps i Depts.
        /// </summary>
        public void Przyklad6()
        {
            //1. Query syntax (SQL)
            var res = from emp in Emps
                      join dept in Depts on emp.Deptno equals dept.Deptno
                      select new
                      {
                          Ename = emp.Ename,
                          Job = emp.Job,
                          Dname = dept.Dname,
                      };

            foreach (var Emp in res)
            {
                Console.WriteLine(Emp.ToString());
            }
            Console.WriteLine();

            //2. Lambda and Extension methods
            var res2 = Emps.Join(Depts, 
                                 emp => emp.Deptno, 
                                 dept => dept.Deptno, 
                                 (emp, dept) => new {
                                     Ename = emp.Ename,
                                     Job = emp.Job,
                                     Dname = dept.Dname,
                                 });
                        

            foreach (var Emp in res2)
            {
                Console.WriteLine(Emp.ToString());
            }
        }

        /// <summary>
        /// SELECT Job AS Praca, COUNT(1) LiczbaPracownikow FROM Emps GROUP BY Job;
        /// </summary>
        public void Przyklad7()
        {
            var res2 = Emps.GroupBy(emp => emp.Job)
                           .Select(emp => (new
                                {
                                    Praca = emp.Key,
                                    LiczbaPracownikow = emp.Count()
                           }));

            foreach (var Emp in res2)
            {
                Console.WriteLine(Emp.ToString());
            }

        }

        /// <summary>
        /// Zwróć wartość "true" jeśli choć jeden
        /// z elementów kolekcji pracuje jako "Backend programmer".
        /// </summary>
        public void Przyklad8()
        {
            var res2 = Emps.Any(emp => emp.Job == "Backend programmer");
            Console.WriteLine(res2);

        }

        /// <summary>
        /// SELECT TOP 1 * FROM Emp WHERE Job="Frontend programmer"
        /// ORDER BY HireDate DESC;
        /// </summary>
        public void Przyklad9()
        {
            var res2 = Emps.Where(emp => emp.Job == "Frontend programmer")
                .OrderByDescending(emp => emp.HireDate)
                .Select(emp => (new
                {
                    emp.Empno,
                    emp.Ename,
                    emp.Salary,
                    emp.HireDate,
                    emp.Job,
                    emp.Deptno,
                    emp.Mgr
                })).First();

            Console.WriteLine(res2);
        }

        /// <summary>
        /// SELECT Ename, Job, Hiredate FROM Emps
        /// UNION
        /// SELECT "Brak wartości", null, null;
        /// </summary>
        public void Przyklad10Button_Click()
        {
            var res3 = Emps.Select(
            emp => new Emp
            {
                Ename = "Brak wartości",
                Job = null,
                HireDate = null
            });

            var res2 = Emps.Select(emp => (new Emp
            {
                Ename = emp.Ename,
                Job = emp.Job,
                HireDate = emp.HireDate
            })).Union(res3);

            foreach (var Emp in res2)
            {
                Console.WriteLine(Emp.ToString());
            }
        }

        //Znajdź pracownika z najwyższą pensją wykorzystując metodę Aggregate()
        public void Przyklad11()
        {

            var res2 = Emps.Where(emp => 
                       emp.Salary == Emps.Aggregate(0, (maxSalary, emp) =>
                       emp.Salary > maxSalary ? maxSalary = emp.Salary : maxSalary))
                            .Select(emp => (new
            {
                emp.Empno,
                emp.Ename,
                emp.Salary,
                emp.HireDate,
                emp.Job,
                emp.Deptno,
                emp.Mgr
            }));

            foreach (var Emp in res2)
            {
                Console.WriteLine(Emp.ToString());
            }
        }

        //Z pomocą języka LINQ i metody SelectMany wykonaj złączenie
        //typu CROSS JOIN
        public void Przyklad12()
        {

            var res2 = Emps.SelectMany(emp => Depts.Select(dept => Tuple.Create(emp, dept)))
                           .Select((emp) => (new
                           {
                               Empno = emp.Item1.Empno,
                               Ename = emp.Item1.Ename,
                               Salary = emp.Item1.Salary,
                               HireDate = emp.Item1.HireDate,
                               Job = emp.Item1.Job,
                               Deptno = emp.Item1.Deptno,
                               Mgr = emp.Item1.Mgr

                           },
                           new {
                               Deptno = emp.Item2.Deptno,
                               Dname = emp.Item2.Dname,
                               Loc = emp.Item2.Loc
                           }
                           ));

            foreach (var Emp in res2)
            {
                Console.WriteLine(Emp.ToString());
            }
        }
    }
}
