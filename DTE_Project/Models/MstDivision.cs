using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DTE_Project.Models
{
    public partial class MstDivision
    {
        public int DivisionId { get; set; }
        [Required(ErrorMessage = "Select State.")]
        public short StateId { get; set; }
        [Required(ErrorMessage = "Field is required.")]
        [Unicode(false)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Use letters only")]
        [DisplayName("Division Name (In English)")]
        [StringLength(50)]
        public string DivisionNameEng { get; set; } = null!;
        [Required(ErrorMessage = "कृपया संभाग का नाम दर्ज करें।")]
        [RegularExpression(@"^[\u0900-\u097F\s]+$", ErrorMessage = "संभाग का नाम हिंदी में दर्ज करें।")]
        [DisplayName("संभाग का नाम (हिंदी में)")]
        [StringLength(60)]
        public string DivisionNameHin { get; set; } = null!;
        [Required(ErrorMessage = "Field is required.")]
        [MaxLength(2)]
        [RegularExpression(@"^[0-9]{1,2}$", ErrorMessage = "Must be 2 digits numbers")]
        [Display(Name = "Division Code No.")]
        [StringLength(2)]
        [Unicode(false)]
        public string DivisionCode { get; set; } = null!;
        [DisplayName("IsActive")]
        public bool IsActive { get; set; }
    }
}
