// ***********************************************************************
// File              : Base.cs
// Assembly          : SpaceScout.Analyzer
// Author            : aikoradlingmayr
// Created           : 04-10-2024
// 
// Last Modified By   : aikoradlingmayr
// Last Modified On   : 04-10-2024
// ***********************************************************************
namespace SpaceScout.Analyzer;

#region class Base ---------------------------------------------------------------------------------
public abstract class Base {
    #region Fields -------------------------------------------------------------
    protected readonly FileSystemInfo _backend;
    protected Size _size = new Size(0);
    #endregion
    
    #region Constructor --------------------------------------------------------
    protected Base(FileSystemInfo fileSystemInfo) {
        _backend = fileSystemInfo;
    }
    #endregion
    
    #region Properties ---------------------------------------------------------
    public string ShortName => _backend.Name;
    public string Path => _backend.FullName;
    public long SizeAsBytes => _size.AsBytes;
    public float Size => _size.Get;
    public Sizes SizeType => _size.SizeType;
    public string SizeString => _size.ToString();
    public string SizeShortString => _size.ToShortString();
    #endregion
  
}
#endregion
