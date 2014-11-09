using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Atsugiri_beef_jerky
{
    class SQLite
    {
        private string DBfile { get; set; }
        private SQLiteConnection Connection { get; set; }

        public SQLite(string dbFile)
        {
            DBfile = dbFile;
            TryOpenFile();
        }

        private void TryOpenFile()
        {
            if (!File.Exists(DBfile))
            {
                SQLiteConnection.CreateFile(DBfile);
                InitializeDB();
            }

            OpenFile();
        }

        private void OpenFile()
        {
            Connection = new SQLiteConnection("Data Source=" + DBfile);
            Connection.Open();
        }

        private void CloseFile()
        {
            if (Connection != null)
            {
                Connection.Close();
                Connection = null;
            }
        }

        private void InitializeDB()
        {
            using (var sqlt = Connection.BeginTransaction())
            {
                using (var sqlc = Connection.CreateCommand())
                {
                    sqlc.CommandText = "create table ignore (twitterId, screenName);";
                    sqlc.ExecuteNonQuery();
                }
                sqlt.Commit();
            }
        }



        public bool isExistValue(string colomn, string value)
        {
            using (var sqlt = Connection.BeginTransaction())
            {
                using (var sqlc = Connection.CreateCommand())
                {
                    sqlc.CommandText = "select * from ignore where " + colomn + " = '" + value + "';";
                    sqlc.ExecuteNonQuery();
                }
                sqlt.Commit();
            }
        }
    }
}
