namespace JobPortal.Models.ViewModels
{
    public class JobsList
    {
        public IEnumerable<Job> Jobs { get; set; }
        public PagingInfo PagingInfo { get; set; }

        public string? CurrentCategory { get; set; }
    }
}
