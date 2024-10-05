// ***********************************************************************
// File              : ADirectory.cs
// Assembly          : SpaceScout.Analyzer
// Author            : aikoradlingmayr
// Created           : 04-10-2024
// 
// Last Modified By   : aikoradlingmayr
// Last Modified On   : 04-10-2024
// ***********************************************************************
using System.Collections.ObjectModel;
using System.Text;
using Utils;
using Utils.FileAnalyzer;

namespace SpaceScout.Analyzer;

#region class ADirectory ----------------------------------------------------------------------------
public class ADirectory : Base {
  #region Fields ---------------------------------------------------------------
  private readonly ObservableCollection<AFile> _files = [];
  private readonly ObservableCollection<ADirectory> _directories = [];
  #endregion
  
  #region Constructor ----------------------------------------------------------
  public ADirectory(DirectoryInfo directoryInfo) : base (directoryInfo) {
    _files.CollectionChanged += (sender, args) => FileCollectionChanged();
    _directories.CollectionChanged += (sender, args) => DirectoryCollectionChanged();
    Init();
    CalculateSize();
  }
  #endregion

  #region Properties -----------------------------------------------------------
  public int FileCount {
    get {
     var fileCount = Files.Length;
      foreach (var directory in Directories) {
        fileCount += directory.FileCount;
      }
      return fileCount;
    }
  }
  public new long SizeAsBytes => _size.AsBytes;
  public ADirectory[] Directories => _directories.Where(d => d != null).ToArray();
  public AFile[] Files => _files.Where(f => f != null).ToArray();
  #endregion
  
  #region Implementation -------------------------------------------------------
  private void FileCollectionChanged() {
    // TODO: add here something for progress tracking
    //CalculateSize();
  }
  private void DirectoryCollectionChanged() {
    //CalculateSize();
  }
  private void CalculateSize() {
    var subDirsSize = Directories.Sum(d => d.SizeAsBytes);
    var filesSize = Files.Sum(f => f.SizeAsBytes);
    _size = new Size(filesSize + subDirsSize);
  }
  private void Init() {
    var directoryInfo = _backend as DirectoryInfo;
    if (directoryInfo == null) {
      Logger.Error("DirectoryInfo is null");
      return;
    }
    if (SpeedFilter(directoryInfo.FullName)) return;
    if (!directoryInfo.HasReadAccess()) return;
    if (directoryInfo.IsReparse()) return;
    
    Logger.Debug($"Processing directory: {directoryInfo.FullName}");
    
    var infos = directoryInfo.EnumerateFileSystemInfos();

    Parallel.ForEach(infos, info => {
      if (!info.HasReadAccess()  || info.IsReparse()) 
        return;
      switch (info) {
        case FileInfo file:
          _files.Add(new AFile(file));
          break;
        case DirectoryInfo dir:
          _directories.Add(new ADirectory(dir));
          break;
      }
    });
  }
  
  // Returns true if it should skip
  private bool SpeedFilter(string path) {
    if (Misc.IsAdmin) {
      
    }
    else {
      if (path.ContainsFormatIndependend("Library/Containers")) return true;
      if (path.ContainsFormatIndependend("Library/Caches")) return true;
      if (path.ContainsFormatIndependend("Library/Group Containers")) return true;
    }
    // cloud checks
    if (!Hub.ScanCloud) {
      if (path.ContainsFormatIndependend("iCloud Drive")) return true;
      if (path.ContainsFormatIndependend("iCloud Documents")) return true;
      if (path.ContainsFormatIndependend("iCloud Photos")) return true;
      if (path.ContainsFormatIndependend("iCloud Photo Library")) return true;
      if (path.ContainsFormatIndependend("OneDrive")) return true;
      if (path.ContainsFormatIndependend("Dropbox")) return true;
      if (path.ContainsFormatIndependend("Google Drive")) return true;
      if (path.ContainsFormatIndependend("Google Photos")) return true;
      if (path.ContainsFormatIndependend("CloudStorage")) return true;
    }
    return false;
  }
  #endregion
  
  #region Methods --------------------------------------------------------------
  public string ToString(int level) {
    var sb = new StringBuilder();
    sb.Append($"{new string(' ', level * 2)}{ShortName} ({SizeShortString})\n");
    foreach (var file in Files) {
      if (file == null) {
        Logger.Error($"File is null, directory: {_backend.FullName}");
        continue;
      }
      sb.Append($"{new string(' ', level * 2 + 2)}{file.ShortName} ({file.SizeShortString})\n");
    }
    foreach (var directory in Directories) {
      sb.Append(directory.ToString(level + 1));
    }
    return sb.ToString();
  }
  #endregion
}
#endregion