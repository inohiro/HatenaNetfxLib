using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HatenaNetfxLib;
using System.Net;
using System.IO;
using System.Xml;

namespace HatenaNetfxLib.Diary
{
	public class BlogCollection
	{
		public BlogCollection()
		{ }

		public List<Entry> Entries
		{
			get;
			set;
		}

		public List<Entry> Get( User user, int page )
		{
			StringBuilder query = new StringBuilder();
			if( page > 0 )
			{
				query.AppendFormat( "http://d.hatena.ne.jp/{0}/atom/blog/?page={1}", user.Username, page );
			}
			else
			{
				Exception ex = new Exception( "取得するページは1以上である必要があります。" );
				throw ex;
			}
			this.get( query.ToString(), user.WsseHeader );
			return new List<Entry>();
		}

		public List<Entry> Get( User user )
		{
			StringBuilder query = new StringBuilder();
			query.AppendFormat( "http://d.hatena.ne.jp/{0}/atom/blog", user.Username );

			this.get( query.ToString(), user.WsseHeader );
			return new List<Entry>();
		}

		private void get( string query, string header )
		{
			HttpWebRequest request = ( HttpWebRequest )WebRequest.Create( query );
			request.Method = "GET";
			request.Headers.Add( "X-WSSE", header );
			request.ContentType = "application/x.atom+xml";

			HttpWebResponse response;

			try
			{
				response = ( HttpWebResponse )request.GetResponse();
			}
			catch( WebException webExp )
			{
				throw webExp;
			}

			if( response != null )
			{
				Stream responstream = response.GetResponseStream();
				StreamReader stmReader = new StreamReader( responstream );
				string result = stmReader.ReadToEnd();
				this.analyseResponse( responstream );
			}
		}

		private List<Entry> analyseResponse( Stream responstream )
		{
			XmlReaderSettings setting = new XmlReaderSettings();
			setting.IgnoreComments = true;
			setting.IgnoreWhitespace = true;
			setting.IgnoreProcessingInstructions = true;

			XmlReader reader = XmlReader.Create( responstream, setting );
			while( reader.Read() )
			{
				switch( reader.NodeType )
				{
					// 欲しい情報を取り出す（拡張Entryクラスを作ってもよいのでは）
					case XmlNodeType.Element:
					{
					} break;

					default: { } break;
				}
			}

			return new List<Entry>();
		}
	}
}
