using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Globalization;
using System.IO;

namespace HatenaNetfxLib.Diary
{
	public class Entry : IPostable
	{
		String title, text;
		DateTime date;
		ErrorInfo errorInfo;

		List<Entry> entries;

		public String Title { set { title = value; } }
		public String Text { set { text = value; } }
		public DateTime Date { set { date = value; } }
		public ErrorInfo ErrorInformation {	get { return this.errorInfo; } }

		public Entry() { }
		public Entry( String Title, String Text )
		{
			this.title = Title;
			this.text = Text;
		}
		public Entry( String Title, String Text, DateTime Date )
		{
			this.title = Title;
			this.text = Text;
			this.date = Date;
		}

		public Boolean Create( User userInfo )
		{
			return this.Post( userInfo );
		}

		//private string CreateDocument()
		//{
		//    StringBuilder xml = new StringBuilder();
		//    if( !String.IsNullOrEmpty( this.title ) && !String.IsNullOrEmpty( this.text ) )
		//    {
		//        xml.Append( "<?xml version=\"1.0\" encoding=\"utf-8\"?>" );
		//        xml.Append( "<entry xmlns=\"http://purl.org/atom/ns#\">" );
		//        xml.Append( "<title>" );
		//        xml.Append( title );
		//        xml.Append( "</title>" );
		//        xml.Append( "<content type=\"text/plain\">" );
		//        xml.Append( text );
		//        xml.Append( "</content>" );
		//        xml.Append( "<updated>" );
		//        xml.Append( DateTime.Now.ToString( "o", new CultureInfo( "ja-jp" ) ) );
		//        xml.Append( "</updated></entry>" );

		//        string requestUri = "http://d.hatena.ne.jp/" + us.Username + "/atom/blog";
		//        HttpWebRequest request = ( HttpWebRequest )WebRequest.Create( requestUri );
		//        request.Method = "POST";
		//        request.Headers.Add( "X-WSSE", userInfo.WsseHeader );
		//        request.ContentType = "application/x.atom+xml";
		//    }
		//    else
		//    {

		//    }
		//    return xml.ToString();
		//}

		private Boolean Post( User userInfo )
		{
			if( !String.IsNullOrEmpty( this.title ) && !String.IsNullOrEmpty( this.text ) )
			{
				StringBuilder xml = new StringBuilder();
				xml.Append( "<?xml version=\"1.0\" encoding=\"utf-8\"?>" );
				xml.Append( "<entry xmlns=\"http://purl.org/atom/ns#\">" );
				xml.Append( "<title>" );
				xml.Append( title );
				xml.Append( "</title>" );
				xml.Append( "<content type=\"text/plain\">" );
				xml.Append( text );
				xml.Append( "</content>" );
				xml.Append( "<updated>" );
				xml.Append( DateTime.Now.ToString( "o", new CultureInfo( "ja-jp" ) ) );
				xml.Append( "</updated></entry>" );

				string requestUri = "http://d.hatena.ne.jp/" + userInfo.Username + "/atom/blog";
				HttpWebRequest request = ( HttpWebRequest )WebRequest.Create( requestUri );
				request.Method = "POST";
				request.Headers.Add( "X-WSSE", userInfo.WsseHeader );
				request.ContentType = "application/x.atom+xml";

				HttpWebResponse response = null;
				try
				{
					Stream requestream = request.GetRequestStream();
					byte[] data = Encoding.UTF8.GetBytes( xml.ToString() );
					requestream.Write( data, 0, data.Length );
					requestream.Close();
					response = ( HttpWebResponse )request.GetResponse();
				}
				catch( WebException webExp )
				{
					errorInfo = new ErrorInfo();
					errorInfo.IsError = true;
					errorInfo.Title = webExp.Message;
					errorInfo.Status = webExp.Status;
				}
				finally
				{

				}

				if( errorInfo.IsError )
				{
					return false;
				}
				else
				{
					if( response.StatusCode == HttpStatusCode.Created ) 
						return true;
					else return false;
				}
			}
			else
			{
				return false;
			}
		}

		public List<Entry> GetEntries( User info )
		{
			List<Entry> entries = new List<Entry>();
			return entries;
		}


	}
}