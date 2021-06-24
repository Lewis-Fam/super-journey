namespace allTdL.Core.Fluent
{
	public interface ICanSetSubjectBody
	{
		ICanSetBody Subject(string subject);
		ICanSendEmail Body(string body);
		void Send();
		void SendAsync();
	}
}
