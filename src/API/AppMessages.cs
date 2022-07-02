namespace API
{
	public static class AppMessages
	{
		#region response.error = false

		public const string msgSuccess = "success";

		#endregion

		#region response.error = true

		public const string serverError = "serverError"; //Exception occured.
		public const string invalidParameters = "invalidParameters"; //API hit with invalid parameters.
	
		#endregion

	}
}
