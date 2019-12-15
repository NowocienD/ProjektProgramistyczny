using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Model
{
    public class ClassSubject
    {
        public int ClassId { get; set; }
        public virtual Class Class { get; set; }

        public int SubjectId { get; set; }
        public virtual Subject Subject{ get; set; }
    }
}
