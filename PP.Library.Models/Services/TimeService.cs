using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PP_Library.Models;

namespace PP_Library.Services
{
    public class TimeService
    {
        private static TimeService? instance;
        private static object daLock = new object();
        public static TimeService Current
        {
            get
            {
                //For thread safety
                lock (daLock)
                {
                    //Critical Part - OS issue if used w/o lock
                    if (instance == null)
                    {
                        instance = new TimeService();
                    }
                }

                return instance;
            }
        }

        private List<Time> workTime;
        private TimeService()
        {
            workTime = new List<Time>
            {
                new Time {EmployeeId = 300, ProjectId = 3, Date = DateTime.Today, Hours = 8, Narrative = "I am #1"},
                new Time {EmployeeId = 400, ProjectId = 9, Date = DateTime.Today, Hours = 12, Narrative = "I am #2"},
                new Time {EmployeeId = 500, ProjectId = 21, Date = DateTime.Today, Hours = 2, Narrative = "I am #3"}
            };
        }
        public List<Time> Times { get { return workTime; } }
        public Time? Get(int emplid)
        {
            return workTime.FirstOrDefault(e => e.EmployeeId == emplid);
        }

        public List<Time> Search(string query)
        {
            return Times.Where(s => s.Narrative.ToUpper().Contains(query.ToUpper())).ToList();
        }

        public void Add(Time? Time)
        {
            if (Time != null) { workTime.Add(Time); }
        }

        public void Read()
        {
            foreach (var money in workTime)
            { Console.WriteLine(money); }
            //workTime.ForEach(Console.WriteLine);
        }
        public void Edit(int toUpdate)
        {
            var UpdateTime = Current.Get(toUpdate);
            if (UpdateTime != null)
            {
                Console.WriteLine("What is the Client's updated name?");
                UpdateTime.Hours = int.Parse(Console.ReadLine() ?? "0");

                Console.WriteLine("What are the Client's updated notes?");
                UpdateTime.Narrative = Console.ReadLine() ?? string.Empty;
            }
        }
        public void Delete(int id)
        {
            var hrsNegated = Get(id);
            if (hrsNegated != null) { workTime.Remove(hrsNegated); }
        }
        public void Delete(Time c)
        {
            Delete(c.EmployeeId);
        }
    }
}
