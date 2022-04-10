using System;
using System.Collections.Generic;

namespace webproject2.Model
{
    public partial class Dbn
    {
        public Dbn()
        {
            Prediction = new HashSet<Prediction>();
        }

        public int Id { get; set; }
        public string Dbnname { get; set; }
        public int? Inputneuronscount { get; set; }
        public int? Hiddenlayerscount { get; set; }
        public int? Hiddenneuronscount { get; set; }
        public int? Outputneuronscount { get; set; }
        public bool? Trainingset { get; set; }
        public decimal? Learningrate { get; set; }
        public string Datasetfilename { get; set; }
        public int? Datasetsize { get; set; }
        public int? NumoftrainingEpoch { get; set; }

        public virtual ICollection<Prediction> Prediction { get; set; }
    }
}
