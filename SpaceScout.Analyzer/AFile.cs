// ***********************************************************************
// File              : AFile.cs
// Assembly          : SpaceScout.Analyzer
// Author            : aikoradlingmayr
// Created           : 04-10-2024
// 
// Last Modified By   : aikoradlingmayr
// Last Modified On   : 04-10-2024
// ***********************************************************************
namespace SpaceScout.Analyzer;

#region class AFile --------------------------------------------------------------------------------
public class AFile : Base {
  #region Fields ---------------------------------------------------------------
  private readonly FileInfo _fileInfo;
  #endregion
  
  #region Constructor ----------------------------------------------------------
  public AFile(FileInfo fileInfo): base(fileInfo) {
    _fileInfo = fileInfo;
    _size = new Size(_fileInfo.Length);
  }
  #endregion

  #region Properties -----------------------------------------------------------
  public string Extension => _fileInfo.Extension;
  #endregion

}
#endregion
