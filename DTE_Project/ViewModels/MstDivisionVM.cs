using Microsoft.AspNetCore.Mvc.Rendering;

namespace DTE_Project.ViewModels
{
    public class MstDivisionVM
    {
        public int DivisionId { get; set; }
        public short StateId { get; set; }
        public string? StateNameEng { get; set; } // For displaying state name
        public string DivisionNameEng { get; set; } = null!;
        public string DivisionNameHin { get; set; } = null!;
        public string DivisionCode { get; set; } = null!;
        public bool IsActive { get; set; }

        // For binding dropdown list of states
       
    }
}
