namespace Zongsoft.Presentation.Icons;

[System.Reflection.DefaultMember(nameof(Icons))]
public partial class IconLibrary
{
    #region 静态字段
    public static readonly IconLibraryCollection Libraries = new();
    #endregion

    #region 构造函数
    public IconLibrary(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));

        Name = name;
        Icons = new IconCollection(this);
        Catalogs = new CatalogCollection(this);
    }
    #endregion

    #region 公共属性
    public string Name { get; }
    public IconCollection Icons { get; }
    public CatalogCollection Catalogs { get; }
    #endregion

    #region 虚拟方法
    protected virtual Icon CreateIcon(int code, string name, object data, IconCatalog? catalog = null) => new(this, code, name, data, catalog);
    #endregion

    #region 静态方法
    /// <summary>获取指定表达式对应的图标。</summary>
    /// <param name="text">指定的图标表达式，其格式为：
    ///		<list type="bullet">
    ///			<item>{name}@{library}</item>
    ///			<item>#{code}@{library}</item>
    ///			<item>{library}:{name}</item>
    ///			<item>{library}:#{code}</item>
    ///		</list>
    /// </param>
    /// <returns>如果成功则返回指定表达式所对应的图标，否则返回空(<c>null</c>)。</returns>
    public static Icon? GetIcon(string text) => text == null ? null : GetIcon(text.AsSpan());

    /// <summary>获取指定表达式对应的图标。</summary>
    /// <param name="text">指定的图标表达式，其格式为：
    ///		<list type="bullet">
    ///			<item>{name}@{library}</item>
    ///			<item>#{code}@{library}</item>
    ///			<item>{library}:{name}</item>
    ///			<item>{library}:#{code}</item>
    ///		</list>
    /// </param>
    /// <returns>如果成功则返回指定表达式所对应的图标，否则返回空(<c>null</c>)。</returns>
    public static Icon? GetIcon(ReadOnlySpan<char> text)
    {
        if (text.IsEmpty)
            return null;

        var separator = text.IndexOf('@');

        if (separator > 0)
            return Libraries[text[(separator + 1)..].ToString()]?.Icons[text[..separator].ToString()];

        if ((separator = text.IndexOf(':')) > 0)
            return Libraries[text[..separator].ToString()]?.Icons[text[(separator + 1)..].ToString()];

        return null;
    }
    #endregion

    #region 嵌套子类
    public class CatalogCollection(IconLibrary library) : KeyedCollection<string, IconCatalog>(StringComparer.OrdinalIgnoreCase)
    {
        private readonly IconLibrary _library = library ?? throw new ArgumentNullException(nameof(library));

        public IconCatalog Add(string name)
        {
            var catalog = new IconCatalog(_library, name);
            Add(catalog);
            return catalog;
        }

        protected override string GetKeyForItem(IconCatalog catalog) => catalog.Name;
    }

    public class IconCollection(IconLibrary library) : IEnumerable<Icon>
    {
        private readonly Dictionary<int, Icon> _codes = new();
        private readonly Dictionary<string, Icon> _names = new(StringComparer.OrdinalIgnoreCase);
        private readonly IconLibrary _library = library ?? throw new ArgumentNullException(nameof(library));

        public int Count => _codes.Count;
        public Icon? this[int code] => _codes.TryGetValue(code, out var icon) ? icon : null;
        public Icon? this[string key]
        {
            get
            {
                if (string.IsNullOrEmpty(key))
                    return null;

                if (key.Length > 1 && key[0] == '#')
                {
                    if (int.TryParse(key.AsSpan()[1..], System.Globalization.NumberStyles.HexNumber, null, out var code))
                        return this[code];
                }

                return _names.TryGetValue(key, out var icon) ? icon : null;
            }
        }

        public Icon? Add(string name, int code, object data, IconCatalog? catalog = null) => Add(code, name, data, catalog);
        public Icon? Add(int code, string name, object data, IconCatalog? catalog = null)
        {
            if (_codes.ContainsKey(code))
                return null;

            var icon = _library.CreateIcon(code, name, data, catalog);
            return _codes.TryAdd(code, icon) ? _names[name] = icon : null;
        }

        public void Clear()
        {
            _codes.Clear();
            _names.Clear();
        }

        public bool Remove(int code) => _codes.Remove(code, out var icon) && _names.Remove(icon.Name);
        public bool Remove(string name) => name != null && _names.Remove(name, out var icon) && _codes.Remove(icon.Code);
        public bool Remove(Icon icon) => icon is not null && _codes.Remove(icon.Code) && _names.Remove(icon.Name);
        public bool Contains(int code) => _codes.ContainsKey(code);
        public bool Contains(string name) => name != null && _names.ContainsKey(name);
        public bool Contains(Icon icon) => icon is not null && _codes.ContainsKey(icon.Code);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<Icon> GetEnumerator() => _codes.Values.GetEnumerator();
    }
    #endregion
}
