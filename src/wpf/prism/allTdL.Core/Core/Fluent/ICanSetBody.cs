namespace allTdL.Core.Fluent
{
	public interface ICanSetBody
	{
		ICanSendEmail Body(string body);
		void Send();
		void SendAsync();
	}
}
