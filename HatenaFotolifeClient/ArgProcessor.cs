using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;

namespace HatenaFotolifeClient
{
	public class ArgProcessor
	{
		private string[] args;

		public ArgProcessor( string[] args )
		{
			this.args = args;
			this.process();
		}

		/*
		 *	default: app.exe inohiro password hoge.jpg
		 *	
		 *	options:
		 *		-d dir : upload dir/*
		 *		-s int : resize to int(px)
		 *		-v : output version information
		 */

		public String Title { get; private set; }
		public Boolean IsTitle { get; private set; }
		public String ImageFilePath { get; private set; }
		public String UserName { get; private set; }
		public String Password { get; private set; }
		public Boolean IsDirectoryUpload { get; private set; }
		public String FolderName { get; private set; }
		public Boolean IsFolder { get; private set; }
		public String DirectoryName { get; private set; }
		public Boolean IsResize { get; private set; }
		public float ResizeSize { get; private set; }
		public Boolean IsOutputVersion { get; private set; }

		private void process()
		{
			for( int i = 0; i < args.Length; i++ )
			{
				switch( args[i] )
				{
					case "-t":
					{
						this.IsTitle = true;
						this.Title = args[i + 1];
						i++;
					}break;

					case "-d":
					{
						this.IsDirectoryUpload = true;
						this.DirectoryName = args[i + 1];
						i++;
					} break;

					case "-s":
					{
						this.IsResize = true;
						this.ResizeSize = Int32.Parse( args[i + 1] );
						i++;
					} break;

					case "-v":
					{
						this.IsOutputVersion = true;
						var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
						Console.WriteLine( version.ToString() );
					} break;

					case "-f":
					{
						this.IsFolder = true;
						this.FolderName = args[i + 1];
						i++;
					} break;

					default:
					{
						if( String.IsNullOrEmpty( this.UserName ) )
						{
							this.UserName = args[i];
						}
						else if( String.IsNullOrEmpty( this.Password ) )
						{
							this.Password = args[i];
						}
						else if( String.IsNullOrEmpty( this.ImageFilePath ) )
						{
							this.ImageFilePath = args[i];
						}
					} break;
				}
			}
		}
	}
}
