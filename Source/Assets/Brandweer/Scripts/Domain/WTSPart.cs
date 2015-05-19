using UnityEngine;
using System.Collections.Generic;

namespace Brandweer.Domain
{
	public class WTSPart : MonoBehaviour 
	{
		/// <summary>
		/// Gets or sets the pressure.
		/// </summary>
		/// <value>The pressure in bar.</value>
		public double Pressure { get; set; }
		/// <summary>
		/// Gets or sets the water output.
		/// </summary>
		/// <value>The water output in water/second (Q).</value>
		public double WaterOutput { get; set; }
		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>The type that counts as indentifier.</value>
		public Part Type { get; set; }
		/// <summary>
		/// Gets or sets the hoses.
		/// </summary>
		/// <value>The list of hoses that are connected as output.</value>
		public List<Hose> Hoses { get; set; }
		/// <summary>
		/// Gets or sets the maximum of outputs.
		/// </summary>
		/// <value>The amount of possible output hoses.</value>
		public int MaxOutputHoses { get; set; }

		public WTSPart (Part type)
		{
			Type = type;
		}
	}
}
