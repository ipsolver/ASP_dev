namespace JobPortal.Models.ViewModels
{
	public class UsersList
	{
		public IEnumerable<Users> Users { get; set; }
		public PagingInfo PagingInfo { get; set; }
	}
}
