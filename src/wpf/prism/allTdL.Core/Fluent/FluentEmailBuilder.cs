namespace allTdL.Core.Fluent
{
	public class FluentEmailBuilder : ICanSetFromAddress, ICanSetToAddress, ICanSetCCSubjectBody, ICanSetBCCSubjectBody, ICanSetSubjectBody, ICanSetBody, ICanSendEmail
	{
		// Instantiating functions

		public static ICanSetFromAddress CreateEmail()
		{
			return new FluentEmailBuilder();
		}

		// Chaining functions

		public ICanSetToAddress From(string emailAddresses)
		{
			return this;
		}

		public ICanSetCCSubjectBody To(string emailAddresses)
		{
			return this;
		}

		public ICanSetBCCSubjectBody CC(string emailAddresses)
		{
			return this;
		}

		public ICanSetSubjectBody BCC(string emailAddresses)
		{
			return this;
		}

		public ICanSetBody Subject(string subject)
		{
			return this;
		}

		public ICanSendEmail Body(string body)
		{
			return this;
		}

		// Executing functions

		public void Send()
		{
		}

		public void SendAsync()
		{
		}
	}
}
