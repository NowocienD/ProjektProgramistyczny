using System.Collections.Generic;

namespace GradebookBackend.DTO
{
    public class ClassListDTO
    {
        public List<ClassDTO> ClassList { get; set; }

        public ClassListDTO()
        {
            ClassList = new List<ClassDTO>();
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ClassListDTO)obj);
        }

        public bool Equals(ClassListDTO other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            for (int i = 0; i < ClassList.Count; i++)
            {
                if (!ClassList[i].Equals(other.ClassList[i]))
                    return false;
            }
            return true;
        }
    }
}
