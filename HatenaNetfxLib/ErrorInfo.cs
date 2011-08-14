using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace HatenaNetfxLib
{
	public class ErrorInfo : Exception
	{
		public ErrorInfo() 
		{
			this.IsError = false;
		}

		public Boolean IsError
		{
			get;
			set;
		}
		public String Title
		{
			get;
			set;
		}
		public WebExceptionStatus Status
		{
			get;
			set;
		}
		public HttpStatusCode StatusCode
		{
			get;
			set;
		}

	}
}
