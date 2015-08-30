using System.Collections.Generic;
using System.Linq;

namespace Discussions.Models
{
    public class IndexViewModel
    {

        private discussionsEntities db = new discussionsEntities();

        public IndexViewModel()
        {
            Discussions = new List<Discussion>();
            Initialize();
        }

        public List<Discussion> Discussions { get; set; }

        private void Initialize()
        {
            foreach (var d in db.Discussions)
            {
                var dis = new Discussion()
                {
                    Date = d.Date,
                    Id = d.Id,
                    Location = d.Location,
                    Outcome = d.Outcome,
                    Subject = d.Subject
                };
                
                AddParticipants(dis);
                this.Discussions.Add(dis);
            }
        }

        private void AddParticipants(Discussion dis)
        {
            var eds = db.EmployeeDiscussions.Where(x => x.DiscussionId == dis.Id).ToList();

            foreach (var e in eds)
            {
                if (e.IsObserver)
                {
                    dis.Observer = new Employee()
                    {
                        FirstName = e.Employee.FirstName,
                        LastName = e.Employee.LastName,
                        IsObserver = e.IsObserver
                    };
                }
                else
                {
                    dis.Participants.Add(new Employee()
                    {
                        FirstName = e.Employee.FirstName,
                        LastName = e.Employee.LastName,
                        IsObserver = e.IsObserver
                    });
                }
            }
        }
    }

   
}