namespace DTE_Project.ViewModels
{
    public class MstBlockVM
    {
        public int BlockId { get; set; }
        public short StateId { get; set; }
        public int DivisionId { get; set; }
        public int DistrictId { get; set; }
        public string BlockNameEng { get; set; }
        public string BlockNameHin { get; set; }
        public string BlockCode { get; set; }
        public bool IsActive { get; set; }

        public string StateNameEng { get; set; }
        public string DivisionNameEng { get; set; }
        public string DistrictNameEng { get; set; }
    }
}
