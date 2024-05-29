namespace Zongsoft.Presentation;

public class Icon : IEquatable<Icon>
{
	#region 成员字段
	private readonly IconLibrary _library;
	private IconCatalog? _catalog;
	#endregion

	#region 构造函数
	internal Icon(IconLibrary library, int code, string name, string? catalog = null)
	{
		ArgumentException.ThrowIfNullOrEmpty(nameof(name));

		_library = library ?? throw new ArgumentNullException(nameof(library));
		this.Code = code;
		this.Name = name;

		this.Move(catalog);
	}
	#endregion

	#region 公共属性
	/// <summary>获取图标编号。</summary>
	public int Code { get; }
	/// <summary>获取图标名称。</summary>
	public string Name { get; }
	/// <summary>获取图标命名空间，即所在图标库的名称。</summary>
	public string Namespace => _library.Name;
	/// <summary>获取图标所属目录(类别)。</summary>
	public IconCatalog? Catalog => _catalog;
	#endregion

	#region 公共方法
	public bool Move(string? catalog)
	{
		if(catalog != null && _library.Catalogs.TryGetValue(catalog, out var value))
		{
			_catalog = value;
			return true;
		}

		_catalog = null;
		return false;
	}
	#endregion

	#region 重写方法
	public bool Equals(Icon? other) => other is not null && _library == other._library && this.Code == other.Code;
	public override bool Equals(object? obj) => obj is Icon other && this.Equals(other);
	public override int GetHashCode() => this.Code;
	public override string ToString() => string.IsNullOrEmpty(this.Namespace) ? $"{this.Name}#{this.Code:X}" : $"{this.Namespace}:{this.Name}#{this.Code:X}";
	#endregion

	#region 重写符号
	public static bool operator ==(Icon left, Icon right) => left.Equals(right);
	public static bool operator !=(Icon left, Icon right) => !(left == right);
	#endregion
}
