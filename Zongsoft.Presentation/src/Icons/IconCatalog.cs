namespace Zongsoft.Presentation.Icons;

public class IconCatalog : IEquatable<IconCatalog>
{
    #region 构造函数
    internal protected IconCatalog(IconLibrary library, string name)
    {
        Library = library ?? throw new ArgumentNullException(nameof(library));
        Name = name ?? string.Empty;
        Icons = new IconCollection(this);
    }
    #endregion

    #region 公共属性
    public string Name { get; }
    public IEnumerable<Icon> Icons { get; }
    #endregion

    #region 保护属性
    internal protected IconLibrary Library { get; }
    #endregion

    #region 重写方法
    public bool Equals(IconCatalog? other) => other is not null && string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase);
    public override bool Equals(object? obj) => obj is IconCatalog other && Equals(other);
    public override int GetHashCode() => Name.GetHashCode();
    public override string ToString() => Name;
    #endregion

    #region 嵌套子类
    private class IconCollection(IconCatalog catalog) : IEnumerable<Icon>
    {
        private readonly IconCatalog _catalog = catalog ?? throw new ArgumentNullException(nameof(catalog));

        public IEnumerator<Icon> GetEnumerator() => _catalog.Library.Icons.Where(icon => icon.Catalog == _catalog).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    #endregion
}

public static class IconCatalogUtility
{
    public static Icon? Icon(this IconCatalog catalog, int code, string name, object data)
    {
        ArgumentNullException.ThrowIfNull(catalog);
        ArgumentException.ThrowIfNullOrEmpty(name);

        return catalog.Library.Icons.Add(code, name, data, catalog);
    }

    public static Icon? Icon(this IconCatalog catalog, string name, int code, object data)
    {
        ArgumentNullException.ThrowIfNull(catalog);
        ArgumentException.ThrowIfNullOrEmpty(name);

        return catalog.Library.Icons.Add(code, name, data, catalog);
    }
}