namespace allTdL.Core.Fluent
{
	public interface ICanSetCCSubjectBody
	{
		ICanSetBCCSubjectBody CC(string emailAddresses);
		ICanSetSubjectBody BCC(string emailAddresses);
		ICanSetBody Subject(string subject);
		ICanSendEmail Body(string body);
		void Send();
		void SendAsync();
	}
}
