using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Discussions.Models
{
    public class Discussion
    {
        public Discussion()
        {
            Participants = new List<Employee>();
         }

        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public string DateString { get { return Date.ToString("dd MMM yyyy"); } }
        public string Location { get; set; }
        public string Subject { get; set; }
        public string Outcome { get; set; }
        public Employee Observer { get; set; }
        
        public string ObserverName { get { return Observer == null ? string.Empty : string.Format("{0} {1}", Observer.FirstName, Observer.LastName); } }

        public string ParticipantList
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                foreach (var p in Participants)
                {
                    if (!p.IsObserver)
                    {
                        sb.Append(string.Format("{0} {1}", p.FirstName, p.LastName));
                    }
                }

                return sb.ToString();
            }
        }

        public List<Employee> Participants { get; set; }
    }
}
