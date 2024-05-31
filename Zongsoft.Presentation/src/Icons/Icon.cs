namespace Zongsoft.Presentation.Icons;

public class Icon : IEquatable<Icon>
{
    #region 成员字段
    private IconCatalog? _catalog;
    #endregion

    #region 构造函数
    internal protected Icon(IconLibrary library, int code, string name, object data, IconCatalog? catalog = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(name));

        Library = library ?? throw new ArgumentNullException(nameof(library));
        Code = code;
        Name = name;
        Data = data;

        _catalog = catalog;
    }
    #endregion

    #region 公共属性
    /// <summary>获取图标编号。</summary>
    public int Code { get; }
    /// <summary>获取图标名称。</summary>
    public string Name { get; }
    /// <summary>获取图标数据。</summary>
    public object Data { get; }
    /// <summary>获取图标命名空间，即所在图标库的名称。</summary>
    public string Namespace => Library.Name;
    /// <summary>获取图标所属目录(类别)。</summary>
    public IconCatalog? Catalog => _catalog;
    #endregion

    #region 保护属性
    internal protected IconLibrary Library { get; }
    #endregion

    #region 公共方法
    public bool Move(string? catalog)
    {
        if (catalog != null && Library.Catalogs.TryGetValue(catalog, out var value))
        {
            _catalog = value;
            return true;
        }

        _catalog = null;
        return false;
    }
    #endregion

    #region 重写方法
    public bool Equals(Icon? other) => other is not null && Library == other.Library && Code == other.Code;
    public override bool Equals(object? obj) => obj is Icon other && Equals(other);
    public override int GetHashCode() => Code;
    public override string ToString() => string.IsNullOrEmpty(Namespace) ? $"{Name}#{Code:X}" : $"{Namespace}:{Name}#{Code:X}";
    #endregion

    #region 重写符号
    public static bool operator ==(Icon left, Icon right) => left.Equals(right);
    public static bool operator !=(Icon left, Icon right) => !(left == right);
    #endregion
}
