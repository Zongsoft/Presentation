
namespace Zongsoft.Presentation;

public class IconLibraryCollection : ICollection<IconLibrary>
{
	private readonly Dictionary<string, IconLibrary> _libraries = new(StringComparer.OrdinalIgnoreCase);

	public int Count => _libraries.Count;
	bool ICollection<IconLibrary>.IsReadOnly => false;
	public IconLibrary? this[string name] => name != null && _libraries.TryGetValue(name, out var library) ? library : null;

	public void Add(IconLibrary library)
	{
		throw new NotImplementedException();
	}

	public void Clear()
	{
		throw new NotImplementedException();
	}

	public bool Remove(IconLibrary library)
	{
		throw new NotImplementedException();
	}

	public bool Contains(IconLibrary library)
	{
		throw new NotImplementedException();
	}

	void ICollection<IconLibrary>.CopyTo(IconLibrary[] array, int arrayIndex)
	{
		throw new NotImplementedException();
	}

	IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	public IEnumerator<IconLibrary> GetEnumerator()
	{
		throw new NotImplementedException();
	}
}
