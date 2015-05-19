

namespace Brandweer.Domain
{
	public class LiftingRamp : WTSPart
	{
		/// <summary>
		/// Gets or sets the height.
		/// </summary>
		/// <value>The height in meters.</value>
		public double Height { get; set; }

		public LiftingRamp () : base(Part.LIFTINGRAMP)
		{
		}
	}
}

