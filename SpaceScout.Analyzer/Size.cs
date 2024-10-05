// ***********************************************************************
// File              : Size.cs
// Assembly          : SpaceScout.Analyzer
// Author            : aikoradlingmayr
// Created           : 04-10-2024
// 
// Last Modified By   : aikoradlingmayr
// Last Modified On   : 04-10-2024
// ***********************************************************************
// ReSharper disable PossibleLossOfFraction
namespace SpaceScout.Analyzer;

#region class Size --------------------------------------------------------------------------------
public class Size {
  #region Fields ---------------------------------------------------------------
  private readonly long _sizeInBytes;
  private Sizes _sizeType;
  private float? _size;
  
  #endregion
  
  #region Constructors ---------------------------------------------------------
  public Size(long sizeInBytes) {
    _sizeInBytes = sizeInBytes;
    Init();
  }
  #endregion
  
  #region Properties -----------------------------------------------------------
  public long AsBytes => _sizeInBytes;

  public float Get {
    get
    {
      if (_size == null) {
        Init();
      }
      return (float)_size!;
    }
  }

  public Sizes SizeType {
    get
    {
      if (_sizeType == Sizes.Unknown) {
        Init();
      }
      return _sizeType;
    }
  }
  #endregion
  
  #region Methods --------------------------------------------------------------
  private void Init() {
    float yotta = _sizeInBytes / (1024L * 1024 * 1024 * 1024) / (1024L * 1024 * 1024 * 1024 * 1024);
    if (yotta > 1) {
      _size = yotta;
      _sizeType = Sizes.YottaBytes;
      return;
    }
    float zetta = _sizeInBytes / (1024L * 1024 * 1024 * 1024) / (1024L * 1024 * 1024 * 1024);
    if (zetta > 1) {
      _size = zetta;
      _sizeType = Sizes.ZettaBytes;
      return;
    }
    float exa = _sizeInBytes / (1024L * 1024 * 1024 * 1024 * 1024 * 1024);
    if (exa > 1) {
      _size = exa;
      _sizeType = Sizes.ExaBytes;
      return;
    }
    float peta = _sizeInBytes / (1024L * 1024 * 1024 * 1024 * 1024);
    if (peta > 1) {
      _size = peta;
      _sizeType = Sizes.PetaBytes;
      return;
    }
    float tera = _sizeInBytes / (1024L * 1024 * 1024 * 1024);
    if (tera > 1) {
      _size = tera;
      _sizeType = Sizes.TeraBytes;
      return;
    }
    float giga = _sizeInBytes / (1024 * 1024 * 1024);
    if (giga > 1) {
      _size = giga;
      _sizeType = Sizes.GigaBytes;
      return;
    }
    float mega = _sizeInBytes / (1024 * 1024);
    if (mega > 1) {
      _size = mega;
      _sizeType = Sizes.MegaBytes;
      return;
    }
    float kilo = _sizeInBytes / 1024;
    if (kilo > 1) {
      _size = kilo;
      _sizeType = Sizes.KiloBytes;
      return;
    }
    _size = _sizeInBytes;
    _sizeType = Sizes.Bytes;

  }
  
  public override string ToString() {
    return $"{Get} {SizeType.ToString()}";
  }
  
  public string ToShortString() {
    return $"{Get} {SizeType.ToShortString()}";
  }
  #endregion
}
#endregion

#region enum Sizes ---------------------------------------------------------------------------------
public enum Sizes {
  Unknown,
  Bytes,
  KiloBytes,
  MegaBytes,
  GigaBytes,
  TeraBytes,
  PetaBytes,
  ExaBytes,
  ZettaBytes,
  YottaBytes
}
#endregion

#region class SizesExtensions ----------------------------------------------------------------------
public static class SizesExtensions
{
  public static string ToShortString(this Sizes size)
  {
    return size switch
    {
      Sizes.KiloBytes => "KB",
      Sizes.MegaBytes => "MB",
      Sizes.GigaBytes => "GB",
      Sizes.TeraBytes => "TB",
      Sizes.PetaBytes => "PB",
      Sizes.ExaBytes => "EB",
      Sizes.ZettaBytes => "ZB",
      Sizes.YottaBytes => "YB",
      _ => size.ToString()
    };
  }
}
#endregion