using System;
using System.Text;
using System.Security.Cryptography;

namespace HatenaNetfxLib
{
	public class User
	{
		public String Username { get; set; }
		public String Password { get; set; }
		public String WsseHeader
		{
			get { return this.CreateWsseHeader(); }
		}

		public User() { }
		public User( String UserName, String Password )
		{
			this.Username = UserName;
			this.Password = Password;
		}

		private String CreateWsseHeader()
		{
			// HTTPリクエスト毎に生成するセキュリティ・トークン（ランダム文字列）
			byte[] b_nonce = new byte[8];
			Random rand = new Random();
			rand.NextBytes( b_nonce );

			// nonce 生成時の日
			string created = DateTime.Now.ToUniversalTime().ToString( "o" );
			byte[] b_created = Encoding.UTF8.GetBytes( created );

			byte[] b_password = Encoding.UTF8.GetBytes( Password );
			SHA1Managed sh1 = new SHA1Managed();
			sh1.Initialize();

			// Join
			byte[] origin = new byte[b_nonce.Length + b_created.Length + b_password.Length];
			Array.Copy( b_nonce, 0, origin, 0, b_nonce.Length );
			Array.Copy( b_created, 0, origin, b_nonce.Length, b_created.Length );
			Array.Copy( b_password, 0, origin, b_nonce.Length + b_created.Length, b_password.Length );

			// Create Hash Value
			byte[] passwordDigest = sh1.ComputeHash( origin );

			string header = String.Format(
				"UsernameToken Username=\"{0}\", PasswordDigest=\"{1}\", Nonce=\"{2}\", Created=\"{3}\"",
				Username, Convert.ToBase64String( passwordDigest ), Convert.ToBase64String( b_nonce ), created );

			return header;
		}
	}
}