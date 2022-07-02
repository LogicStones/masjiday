namespace API
{
	public class ResponseEntity
	{
		public bool Error { get; set; }
		public string Message { get; set; }
		public dynamic Data { get; set; }
	}
}
