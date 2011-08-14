using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

using HatenaNetfxLib;

namespace HatenaNetfxLib.Fotolife
{
	public class Photo
	{
		public Photo() { }

		public string Title { get; set; }
		public string ImageFilePath { get; set; }
		public string FolderName { get; set; }
		public string Generator { get; set; }

		public string Post( User userInfo )
		{
			var result = this.post( userInfo );
			if( result )
			{
				return String.Format( "Success!!: {0}\n", this.Title );
			}
			else
			{
				return String.Format( "Faile...: {0}\n", this.Title );
			}
		}

		private bool post( User userInfo )
		{
			Console.WriteLine( "Uploading...: {0}", this.Title );

			// Base64 Encoding
			String encodedImg;
			if( this.ImageFilePath != null )
			{
				byte[] bytes;

				try
				{
					using( FileStream filestream = new FileStream( this.ImageFilePath, FileMode.Open, FileAccess.Read ) )
					{
						bytes = new byte[filestream.Length];
						int readBytes = filestream.Read( bytes, 0, ( Int32 )filestream.Length );
					}
					encodedImg = System.Convert.ToBase64String( bytes );
				}
				catch( FileNotFoundException )
				{
					Console.WriteLine( "ファイルが見つかりませんでした\n" );
					return false;
				}
				catch( FileLoadException )
				{
					Console.WriteLine( "データのロードに失敗しました\n" );
					return false;
				}

				StringBuilder xml = new StringBuilder();
				xml.Append( "<?xml version=\"1.0\" encoding=\"utf-8\"?>" );
				xml.Append( "<entry xmlns=\"http://purl.org/atom/ns#\">" );
				xml.Append( String.Format( "<title>{0}</title>", this.Title ) );
				xml.Append( String.Format( "<content type=\"image/png\" mode=\"base64\">{0}</content>", encodedImg ) );
				xml.Append( String.Format( "<dc:subject>{0}</dc:subject>", this.FolderName ) );
				xml.Append( String.Format( "<generator>{0}</generator></entry>", this.FolderName ) );

				HttpWebRequest request = ( HttpWebRequest )WebRequest.Create( "http://f.hatena.ne.jp/atom/post" );
				request.Method = "POST";
				request.Headers.Add( "X-WSSE", userInfo.WsseHeader );
				request.ContentType = "application/x.atom+xml";

				Stream requestream = request.GetRequestStream();
				byte[] data = Encoding.UTF8.GetBytes( xml.ToString() );
				requestream.Write( data, 0, data.Length );
				requestream.Close();

				HttpWebResponse response = ( HttpWebResponse )request.GetResponse();
				if( response.StatusCode == HttpStatusCode.Created )
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else // ImageFilePath -> null
			{
				return false;
			}
		}
	}
}
