using System;

using HatenaNetfxLib;
using HatenaNetfxLib.Diary;
using HatenaNetfxLib.Fotolife;

namespace HatenaLibTest
{
	class Program
	{
		static void Main( string[] args )
		{
			User user = new User();
			user.Username = "InoHiro";
			user.Password = "pass";

			BlogCollection collection = new BlogCollection();
			collection.Get( user );

			//Entry entry = new Entry( "Hello, World", "This is Sample Text. It was set in Constractor" );

			//if( entry.Create( info ) )
			//{
			//    Console.WriteLine( "Success!!" );
			//}
			//else
			//{
			//    Console.WriteLine( "Fail..." );
			//    if( entry.ErrorInformation != null )
			//    {
			//        Console.WriteLine( entry.ErrorInformation.Title );
			//    }
			//}

			Console.ReadKey();
		}
	}
}
