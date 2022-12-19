using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseWork.Model
{
    public class NewTask
    {
        public string Title { get; set; }
        public string Assignment { get; set; }
        public long DeadLine { get; set; }
        public int Urgency { get; set; }
        public string User { get; set; }
    }
}
