using System;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.Serialization;

namespace DirectoryFileCount.InterfaceAndModels.Models
{
    [DataContract(IsReference = true)]
    public class CountingRequest
    {
        #region Fields

        [DataMember] private Guid _guid;
        [DataMember] private string _path;
        [DataMember] private DateTime _date;
        [DataMember] private int _filesCount;
        [DataMember] private int _directoriesCount;
        [DataMember] private long _totalSize;
        [DataMember] private Guid _userGuid;
        [DataMember] private User _user;

        #endregion

        #region Properties

        public Guid Guid
        {
            get { return _guid; }
            private set { _guid = value; }
        }

        public string Path
        {
            get { return _path; }
            private set { _path = value; }
        }

        public int FilesCount
        {
            get { return _filesCount; }
            private set { _filesCount = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            private set { _date = value; }
        }

        public int DirectoriesCount
        {
            get { return _directoriesCount; }
            private set { _directoriesCount = value; }
        }

        public long TotalSize
        {
            get { return _totalSize; }
            private set { _totalSize = value; }
        }

        public Guid UserGuid
        {
            get { return _userGuid; }
            private set { _userGuid = value; }
        }

        public User User
        {
            get { return _user; }
            private set { _user = value; }
        }

        #endregion

        #region Constructor

        public CountingRequest(string path, User user, int directoriesCount, int filesCount, long size) : this()
        {
            _guid = Guid.NewGuid();
            _path = path;
            _date = DateTime.Now;
            _directoriesCount = directoriesCount;
            _filesCount = filesCount;
            _totalSize = size;
            _user = user;
            _userGuid = user.Guid;
            user.CountingRequests.Add(this);
        }

        private CountingRequest()
        {
        }

        #endregion

        public void DeleteDatabaseValues()
        {
            _user = null;
        }

        public override string ToString()
        {
            return $"{Path} on {Date}";
        }

        #region EntityFrameworkConfiguration

        public class CountingRequestEntityConfiguration : EntityTypeConfiguration<CountingRequest>
        {
            public CountingRequestEntityConfiguration()
            {
                ToTable("CountingRequests");
                HasKey(s => s.Guid);

                Property(p => p.Path)
                    .HasColumnName("Path")
                    .IsRequired();
                Property(s => s.Date)
                    .HasColumnName("Date")
                    .IsRequired();
                Property(s => s.FilesCount)
                    .HasColumnName("FilesCount")
                    .IsRequired();
                Property(s => s.DirectoriesCount)
                    .HasColumnName("DirectoriesCount")
                    .IsRequired();
                Property(s => s.TotalSize)
                    .HasColumnName("TotalSize")
                    .IsRequired();
                Property(s => s.UserGuid)
                    .HasColumnName("UserGuid")
                    .IsRequired();
            }
        }

        #endregion
    }
}