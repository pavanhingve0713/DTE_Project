using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTE_Project.Models
{
    public partial class MstBlock
    {
        public int BlockId { get; set; }
        [DisplayName("State")]
        [Required(ErrorMessage = "Select State")]
        public short StateId { get; set; }
        [DisplayName("Division")]
        [Required(ErrorMessage = "Select Division")]
        public int DivisionId { get; set; }
        [DisplayName("District")]
        [Required(ErrorMessage = "Select District")]
        public int DistrictId { get; set; }
        [DisplayName("Block Name(In English)")]       
        [RegularExpression(@"^[a-zA-Z\s-]+$", ErrorMessage = "Use letters only")]
        [Required(ErrorMessage = "Enter Block Name.")]
        [StringLength(55)]       
        [Unicode(false)]
        public string? BlockNameEng { get; set; }
        [DisplayName("ब्लॉक का नाम(हिंदी में)")]
        [Required(ErrorMessage = "कृपया ब्लॉक का नाम दर्ज करें")]
        [RegularExpression(@"^[\u0900-\u097F\s]+$", ErrorMessage = "ब्लॉक का नाम हिंदी में दर्ज करें।")]      
        [StringLength(60)]
        public string? BlockNameHin { get; set; }
        [DisplayName("Block Code No.")]
        [Required(ErrorMessage = "Enter Code No.")]
        [RegularExpression(@"^[0-9]{1,3}$", ErrorMessage = "Must be 3 digits numbers")]
        [StringLength(10)]
        [Column(TypeName = "varchar")]
        [MaxLength(3)]
        public string? BlockCode { get; set; }
        [DisplayName("Is Active")]
        public bool IsActive { get; set; }
    }
}
