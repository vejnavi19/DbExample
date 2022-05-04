using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Database
{
    public class Db
    {
        // Connection
        private SQLiteAsyncConnection Conn;

        // Connect to database
        public Db(string Path)
        {
            // Create new connection
            Conn = new SQLiteAsyncConnection(Path);

            // Constructor of Database (Table created by avangers class)
            Conn.CreateTableAsync<Avenger>().Wait();

        }

        // This method saves instances of Avenger Class (Insert if does not exists OR only update)
        public Task<int> SaveItemAsync(Avenger item)
        {
            if (item.ID != 0)
            {
                return Conn.UpdateAsync(item);
            } else
            {
                return Conn.InsertAsync(item);
            }
        }

        // Delete item from table
        public Task<List<Avenger>> DeleteItemAsync(int ID) 
        {
            return Conn.QueryAsync<Avenger>($"DELETE FROM [AVENGER] WHERE [ID] = {ID}");
        }

        // This will return all Items in DB
        public Task<List<Avenger>> GetItemsAsync()
        {
            return Conn.Table<Avenger>().ToListAsync();
        }


        // Query will return all items by name
        public Task<List<Avenger>> GetItemsByName(string name)
        {
            return Conn.QueryAsync<Avenger>($"SELECT * FROM [AVENGERS] WHERE [NAME] = {name}");
        }
        public Task<List<Avenger>> EditItemAsync(Avenger item, int ID)
        {
            return Conn.QueryAsync<Avenger>($"UPDATE [AVENGERS] SET [NAME] = {item.Name}, [REALNAME] = {item.RealName}, [GENDER] = {item.Gender} WHERE [ID] = {ID}");
        }

        /*
         CRUD (What does it mean?)
            C --> Create
            R --> Read
            U --> Update
            D --> Delete
         */
    }
}
