using System;
using System.Collections.Generic;

namespace BurkinafasoSite.Models
{
    public partial class Module
    {
        public Module()
        {
            FactsNavigation = new HashSet<Facts>();
            Image = new HashSet<Image>();
            PracticalExercise = new HashSet<PracticalExercise>();
            Test = new HashSet<Test>();
            TheoreticalExercise = new HashSet<TheoreticalExercise>();
        }

        public int Id { get; set; }
        public string Topic { get; set; }
        public string Status { get; set; }
        public string Facts { get; set; }
        public int? FkCourseId { get; set; }
        public string Language { get; set; }

        public Course FkCourse { get; set; }
        public ICollection<Facts> FactsNavigation { get; set; }
        public ICollection<Image> Image { get; set; }
        public ICollection<PracticalExercise> PracticalExercise { get; set; }
        public ICollection<Test> Test { get; set; }
        public ICollection<TheoreticalExercise> TheoreticalExercise { get; set; }
    }
}
