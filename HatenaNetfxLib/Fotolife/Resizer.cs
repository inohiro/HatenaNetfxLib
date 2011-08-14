using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;

namespace HatenaNetfxLib.Fotolife
{
	public static class Resizer
	{
		public static bool ResizeImage( string imageFilePath, float resizeSize )
		{
			Image sourceImage = Image.FromFile( imageFilePath );

			float l = sourceImage.Width;
			float s = sourceImage.Height;

			// 長辺・短辺を調べる
			//			float l, s;
			//			l = sourceWidth; s = sourceHeight;
			//			if( sourceHeight < sourceWidth ) { l = sourceWidth; s = sourceHeight; }
			//			else                             { l = sourceHeight; s = sourceWidth; }

			// 比を求める
			float g = ratio( s, l );

			// もう一方のサイズを求める
			float otherResizeSize = resizeSize * g;

			int destLong = ( int )resizeSize;
			int destShort = ( int )otherResizeSize;

			Bitmap newImage = new Bitmap( destLong, destShort );
			Graphics graphics = Graphics.FromImage( ( Image )newImage );
			graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

			try
			{
				graphics.DrawImage( sourceImage, 0, 0, resizeSize, otherResizeSize );
				graphics.Dispose();

				newImage.Save( "tmp_img.jpg" );
			}
			catch
			{
				return false;
			}
			return true;
		}

		private static float ratio( float l, float s )
		{
			// get GCD
			float g = gcd( l, s );

			// devided by GCD
			float lg = l / g;
			float sg = s / g;

			// large devide small
			return l / s;
		}
		private static float gcd( float l, float s )
		{
			while( s != 0 )
			{
				float r = l % s;
				l = s;
				s = r;
			}
			return l;
		}
	}
}
