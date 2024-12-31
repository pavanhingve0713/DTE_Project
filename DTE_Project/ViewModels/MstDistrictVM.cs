namespace DTE_Project.ViewModels
{
    public class MstDistrictVM
    {
        public int DistrictId { get; set; }
        public short StateId { get; set; }
        public string StateNameEng { get; set; } = null!;
        public int DivisionId { get; set; }
        public string DivisionNameEng { get; set; } = null!;
        public string DistrictNameEng { get; set; } = null!;
        public string DistrictNameHin { get; set; } = null!;
        public string? DistrictCode { get; set; }
        public bool IsActive { get; set; }
    }
}
