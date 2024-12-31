using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DTE_Project.Models
{
    public partial class MstDistrict
    {
        public int DistrictId { get; set; }
		[Required(ErrorMessage = "Select State Name")]
		[DisplayName("State")]
		public short StateId { get; set; }
		[Required(ErrorMessage = "Select Division Name")]
		[DisplayName("Division")]
		public int DivisionId { get; set; }
		[StringLength(50)]
		[Required(ErrorMessage = "Enter District Name")]
		[RegularExpression(@"^[a-zA-Z\s-]+$", ErrorMessage = "Use letters only")]
		[DisplayName("District Name(In English)")]		
		public string DistrictNameEng { get; set; } = null!;
		[StringLength(60)]
		[Required(ErrorMessage = "कृपया जिले का नाम दर्ज करें")]
		[DisplayName("जिले का नाम(हिंदी में)")]
		[RegularExpression(@"^[\u0900-\u097F\s]+$", ErrorMessage = "जिले का नाम हिंदी में दर्ज करें।")]

		[Column(TypeName = "nvarchar")]
		public string DistrictNameHin { get; set; } = null!;
		[StringLength(10)]
		[Required(ErrorMessage = "Enter Code No.")]
		[DisplayName("District Code No.")]		
		[MaxLength(2)]
		[RegularExpression(@"^[0-9]{1,2}$", ErrorMessage = "Must be 2 digits numbers")]
		public string? DistrictCode { get; set; }
        public bool IsActive { get; set; }
    }
}
