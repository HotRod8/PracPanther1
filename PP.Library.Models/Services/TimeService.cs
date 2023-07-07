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
                new Time {EmployeeId = 300, ProjectId = 1, ClientId = 3, Date = DateTime.Today, Hours = 8, Narrative = "I am #1"},
                new Time {EmployeeId = 400, ProjectId = 2, ClientId = 3, Date = DateTime.Today, Hours = 12, Narrative = "I am #2"},
                new Time {EmployeeId = 500, ProjectId = 3, ClientId = 3, Date = DateTime.Today, Hours = 2, Narrative = "I am #3"}
            };
        }
        public List<Time> Times { get { return workTime; } }
        public Time? Get(int emplid, int projid, int clientid)
        {
            var test = ProjService.Current.GetProj(clientid, projid);
            if (test == null) { return null; }
            return workTime.FirstOrDefault(e => (e.EmployeeId == emplid) && (e.ProjectId == projid) && (e.ClientId == clientid));
        }

        public List<Time> Search(string query)
        {
            //Doesn't like a null query
            return Times.Where(s => s.Narrative.ToUpper().Contains(query.ToUpper())).ToList();
        }

        public void Add(Time? Time)
        {
            if (Time != null)
            {
                workTime.Add(Time);
            }
        }

        public void Read()
        {
            foreach (var money in workTime)
            { Console.WriteLine(money); }
            //workTime.ForEach(Console.WriteLine);
        }
        public void Edit(Time toUpdate)
        {
            var UpdateTime = Current.Get(toUpdate.EmployeeId, toUpdate.ProjectId, toUpdate.ClientId);
            if (UpdateTime != null)
            {
                UpdateTime.Hours = toUpdate.Hours;
                UpdateTime.Narrative = toUpdate.Narrative;
            }
        }
        public void Delete(int eid, int pid, int cid)
        {
            var hrsNegated = Get(eid,pid,cid);
            if (hrsNegated != null) { workTime.Remove(hrsNegated); }
        }
        public void Delete(Time c)
        {
            Delete(c.EmployeeId, c.ProjectId, c.ClientId);
        }
    }
}
