/* 
 * Requires the LeapMotion SDK
 * http://developer.leapmotion.com
 * Unity Project Setup: https://developer.leapmotion.com/documentation/Languages/CSharpandUnity/Guides/Setup_Unity.html
 */

using UnityEngine;
using System.Collections;
using Leap;

namespace Leap
{
	//Extension to the unity vector class. Provides automatic scaling into unity scene space.
	//Leap coordinates are in cm, so the .02f scaling factor means 1cm of hand motion = .02m scene motion
	public static class LeapExtensions
	{
		//public static Vector3 InputScale = new Vector3(0.04f, 0.04f, 0.04f);
		public static Vector3 InputOffset = new Vector3(0,0,0);

		public static Vector3 LeapMin = new Vector3(-15, 60, -15);
		public static Vector3 LeapMax = new Vector3(15, 80, 15);
		public static Vector3 WorldMin = new Vector3(-2, -0.25f, -2);
		public static Vector3 WorldMax = new Vector3(2, 2.75f, 2);
		
		//For Directions
		public static Vector3 ToUnity(this Vector lv)
		{
			return FlippedZ(lv);
		}
		//For Acceleration/Velocity
		public static Vector3 ToUnityScaled(this Vector lv)
		{
			return Scaled(FlippedZ( lv ));
		}
		//For Positions
		public static Vector3 ToUnityTranslated(this Vector lv)
		{
			return Offset(Scaled(FlippedZ( lv )));
		}
		
		private static Vector3 FlippedZ( Vector v ) { return new Vector3( v.x, v.y, -v.z ); }
		private static Vector3 Scaled( Vector3 v ) {
			v.x = (v.x - LeapMin.x)/(LeapMax.x - LeapMin.x);
			v.y = (v.y - LeapMin.y)/(LeapMax.y - LeapMin.y);
			v.z = (v.z - LeapMin.z)/(LeapMax.z - LeapMin.z);

			v.x = WorldMin.x + (v.x * (WorldMax.x - WorldMin.x));
			v.y = WorldMin.y + (v.y * (WorldMax.y - WorldMin.y));
			v.z = WorldMin.z + (v.z * (WorldMax.z - WorldMin.z));

			return v;
		}
		private static Vector3 Offset( Vector3 v ) { return v + InputOffset; }
	}
}