// ***********************************************************************
// File              : Analyzer.cs
// Assembly          : SpaceScout.Analyzer
// Author            : aikoradlingmayr
// Created           : 04-10-2024
// 
// Last Modified By   : aikoradlingmayr
// Last Modified On   : 04-10-2024
// ***********************************************************************
using System.Text;

namespace SpaceScout.Analyzer;

#region class Analyzer -----------------------------------------------------------------------------
public class Analyzer(string path) {
  #region Fields ---------------------------------------------------------------
  ADirectory? _rootFolder;
  DateTime _startTime;
  DateTime _endTime;
  #endregion
  
  #region Constructors ---------------------------------------------------------
  public void Analyze() {
    _startTime = DateTime.Now;
    _rootFolder = new ADirectory(new DirectoryInfo(path));
    _endTime = DateTime.Now;
  }
  #endregion
  
  #region Properties -----------------------------------------------------------
  public int FileCount => _rootFolder!.FileCount;
  public TimeSpan Duration => _endTime - _startTime;
  #endregion
  
  #region Methods --------------------------------------------------------------
  public override string ToString() {
    var sb = new StringBuilder();
    sb.Append(_rootFolder?.ToString(0));
    return sb.ToString();
  }
  #endregion
}
#endregion