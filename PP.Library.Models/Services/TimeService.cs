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
            workTime = new List<Time>();
        }
        public List<Time> Times { get { return workTime; } }
        public Time? Get(int emplid, int projid, int timeid)
        {
            return workTime.FirstOrDefault(e => (e.Id == timeid)&&(e.EmployeeId == emplid) && (e.ProjectId == projid));
        }

        public List<Time> Search(string query)
        {
            //Doesn't like a null query
            return Times.Where(s => s.ToString().ToUpper().Contains(query.ToUpper())).ToList();
        }

        public void Add(Time? Time)
        {
            if (Time != null)
            {
                Time.Id = LastId + 1;
                Times.Add(Time);
            }
        }
        private int LastId
        {
            get
            {
                return Times.Any() ? Times.Select(c => c.Id).Max() : 0;
            }
        }

        public void Read()
        {
            foreach (var money in Times)
            { Console.WriteLine(money); }
            //workTime.ForEach(Console.WriteLine);
        }
        public void Edit(Time toUpdate)
        {
            var UpdateTime = Current.Get(toUpdate.EmployeeId, toUpdate.ProjectId, toUpdate.Id);
            if (UpdateTime != null)
            {
                UpdateTime.Hours = toUpdate.Hours;
                UpdateTime.Narrative = toUpdate.Narrative;
            }
        }
        public void Delete(int eid, int pid, int tid)
        {
            var hrsNegated = Get(eid,pid,tid);
            if (hrsNegated != null) { workTime.Remove(hrsNegated); }
        }
        public void Delete(Time c)
        {
            Delete(c.EmployeeId, c.ProjectId, c.Id);
        }
    }
}
