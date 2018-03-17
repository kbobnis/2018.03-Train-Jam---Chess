using System;

public class Percent {
	public readonly static Percent One = new Percent(1);
	
	public readonly float value;

	public Percent(float f) {
		if (f < 0 || f > 1) {
			throw new ArgumentException(string.Format("Value outside bounds: {0}", f));
		}
		this.value = f;
	}
}