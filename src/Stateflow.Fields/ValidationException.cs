using System;
using System.Runtime.Serialization;

namespace Stateflow.Fields
{

	[Serializable]
	public class ValidationException : Exception
	{
	
		public ValidationException(string message) : base(message)
		{

		}

		protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public ValidationException(string message, Exception innerException) : base(message, innerException)
		{
		}

	}

}
