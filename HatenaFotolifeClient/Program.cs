using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HatenaNetfxLib;
using HatenaNetfxLib.Fotolife;
using System.Drawing;
using System.IO;

namespace HatenaFotolifeClient
{
	class Program
	{
		const string GENERATOR_NAME = "InoHiro_Console_Uploader(HatenaNetfxLib.)";

		static void Main( string[] args )
		{
			//if( args.Length < 2 ) 
			//{ 
			//    Console.WriteLine( "Invalid Arguments..." );
			//    Console.ReadKey();
			//    Environment.Exit( 0 ); 
			//}

			ArgProcessor ap = new ArgProcessor( args );

			User user = new User();
			user.Username = ap.UserName;
			user.Password = ap.Password;


			if( ap.IsDirectoryUpload )
			{
				string[] files = Directory.GetFiles( ap.DirectoryName );
				foreach( string file in files )
				{
					Photo photo = new Photo();
					if( ap.IsTitle ) { photo.Title = ap.Title; }else { photo.Title = file; }
					if( ap.IsFolder ) { photo.FolderName = ap.FolderName; }
					photo.Generator = GENERATOR_NAME;

					if( Resizer.ResizeImage( file, ap.ResizeSize ) )
					{
						photo.ImageFilePath = "tmp_img.jpg";
						Console.WriteLine( photo.Post( user ) );
					}
					else
					{
						Console.WriteLine( "アップロードに失敗しました" );
					}
				}
			}
			else
			{
				Photo photo = new Photo();
				if( ap.IsTitle ) { photo.Title = ap.Title; }else { photo.Title = ap.ImageFilePath; }
				if( ap.IsFolder ) { photo.FolderName = ap.FolderName; }
				photo.Generator = GENERATOR_NAME;

				if( Resizer.ResizeImage( ap.ImageFilePath, ap.ResizeSize ) )
				{
					photo.ImageFilePath = "tmp_img.jpg";
					Console.WriteLine( photo.Post( user ) );
				}
				else
				{
					Console.WriteLine( "アップロードに失敗しました" );
				}
			}

			Console.ReadKey();
		}
	}
}
