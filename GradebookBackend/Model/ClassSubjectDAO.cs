using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Model
{
    public class ClassSubjectDAO
    {
        public int ClassId { get; set; }
        public virtual ClassDAO Class { get; set; }

        public int SubjectId { get; set; }
        public virtual SubjectDAO Subject{ get; set; }
    }
}
