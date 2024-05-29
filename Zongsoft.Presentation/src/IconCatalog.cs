namespace Zongsoft.Presentation;

public class IconCatalog : IEquatable<IconCatalog>
{
	#region 构造函数
	internal IconCatalog(IconLibrary library, string name)
	{
		this.Name = name ?? string.Empty;
		this.Icons = new IconCollection(library, this);
	}
	#endregion

	#region 公共属性
	public string Name { get; }
	public IEnumerable<Icon> Icons { get; }
	#endregion

	#region 重写方法
	public bool Equals(IconCatalog? other) => other is not null && string.Equals(this.Name, other.Name, StringComparison.OrdinalIgnoreCase);
	public override bool Equals(object? obj) => obj is IconCatalog other && this.Equals(other);
	public override int GetHashCode() => this.Name.GetHashCode();
	public override string ToString() => this.Name;
	#endregion

	#region 嵌套子类
	private class IconCollection(IconLibrary library, IconCatalog catalog) : IEnumerable<Icon>
	{
		private readonly IconLibrary _library = library ?? throw new ArgumentNullException(nameof(library));
		private readonly IconCatalog _catalog = catalog ?? throw new ArgumentNullException(nameof(catalog));

		public IEnumerator<Icon> GetEnumerator() => _library.Icons.Where(icon => icon.Catalog == _catalog).GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
	#endregion
}
