﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Model
{
    public class Grade
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int Importance { get; set; }
        public int Value { get; set; }
        public string Topic { get; set; }

        public Subject Subject { get; set; }
        public Teacher Teacher { get; set; }
    }
}