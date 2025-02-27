namespace HomeDoctorSolution.Models.ModelDTO
{
	public class DeleteFileDTO
	{
		public List<ListPath> ListPaths { get; set; } = null!;
	}
	public class ListPath
	{
		public int Id { get; set; }
		public string? Path { get; set; }
		public string? ThumbnailPath { get; set; }

	}
}
