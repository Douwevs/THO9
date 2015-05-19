

namespace Brandweer.Domain
{
	public class Firetruck : WTSPart
	{
		/// <summary>
		/// Gets or sets the pressure increase.
		/// </summary>
		/// <value>The pressure increase.</value>
		public double PressureIncrease { get; set; }

		public Firetruck () : base(Part.FIRETRUCK)
		{
		}
	}
}

