namespace Delegates
{
	public delegate void Delegate();
	public delegate void Delegate<T>(in T _value);
	public delegate void Delegate<T1, T2>(in T1 _value1, in T2 _value2);
	public delegate void Delegate<T1, T2, T3>(in T1 _value1, in T2 _value2, in T3 _value3);

	public delegate bool BoolDelegate<T>(in T _value);
}
