namespace allTdL.Core.Fluent
{
	public interface ICanSetBCCSubjectBody
	{
		ICanSetSubjectBody BCC(string emailAddresses);
		ICanSetBody Subject(string subject);
		ICanSendEmail Body(string body);
		void Send();
		void SendAsync();
	}
}
