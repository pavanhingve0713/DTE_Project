using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTE_Project.Models
{
    public partial class MstState
    {
        public short StateId { get; set; }
        [StringLength(55)]
        [Unicode(false)]
        [Required(ErrorMessage = "Field is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Use characters only.")]
        public string StateNameEng { get; set; } = null!;
        [StringLength(65)]
        [Required(ErrorMessage = "कृपया राज्य का नाम दर्ज करे।")]
        [RegularExpression(@"^[\u0900-\u097F\s]+$", ErrorMessage = "राज्य का नाम हिंदी में दर्ज करें।")]
        public string StateNameHin { get; set; } = null!;
        [StringLength(2)]
        [Unicode(false)]
        [Required(ErrorMessage = "Field is required.")]
        [MinLength(2, ErrorMessage = "Code No. must be at least 2 digit.")]
        [RegularExpression(@"^[0-9]{2}$", ErrorMessage = "Use numeric value only.")]
        public string StateCode { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
