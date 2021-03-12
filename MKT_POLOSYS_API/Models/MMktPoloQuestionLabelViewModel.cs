using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKT_POLOSYS_API.Models
{
    public class MMktPoloQuestionLabelViewModel
    {
        public long IdQuestion { get; set; }
        public string LabelId { get; set; }
        public string QuestionLabel { get; set; }
        public long AnswerType { get; set; }
        public string Lov { get; set; }
        public string IsActive { get; set; }
        public string IsVisible { get; set; }
        public string IsMandatory { get; set; }
        public string IsReadonly { get; set; }
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }
        public string UsrCrt { get; set; }
        public DateTime? DtmCrt { get; set; }
        public string UsrUpd { get; set; }
        public DateTime? DtmUpd { get; set; }
    }
}
