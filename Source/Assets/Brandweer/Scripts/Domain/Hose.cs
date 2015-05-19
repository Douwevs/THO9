using UnityEngine;

namespace Brandweer.Domain
{
	public class Hose : MonoBehaviour
	{
		/// <summary>
		/// Gets or sets the length.
		/// </summary>
		/// <value>The length in meters.</value>
		public double Length { get; set; }
		/// <summary>
		/// Gets or sets the diameter.
		/// </summary>
		/// <value>The diameter in centimeter.</value>
		public double Diameter { get; set; }
		/// <summary>
		/// Gets or sets the output part.
		/// </summary>
		/// <value>The output part to which the hose leads.</value>
		public WTSPart Part { get; set; }

		public Hose ()
		{
		}

		/// <summary>
		/// Calculates the pressure lost.
		/// </summary>
		/// <returns>The lost pressure.</returns>
		public double PressureLost(){
			//TODO: Replace with meaningful code.
			return 0;
		}
	}
}

