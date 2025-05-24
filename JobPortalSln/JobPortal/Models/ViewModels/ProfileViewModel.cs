namespace JobPortal.Models.ViewModels
{
    public class ProfileViewModel
    {
        public Users User { get; set; }

        public List<Category> AllCategories { get; set; }

        public List<int> SelectedCategoryIDs { get; set; } = new();
    }
}
