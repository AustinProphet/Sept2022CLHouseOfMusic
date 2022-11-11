using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.Entity;
using System.Diagnostics;

namespace PaulsHouseOfMusic
{
    public class InventoryBuilder
    {
        static SQLiteConnection CreateConnection()
        {

            SQLiteConnection sqlite_conn;
            sqlite_conn = new SQLiteConnection("Data Source=musicalinstruments.db; Version = 3; New = True; Compress = True; ");

         try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return sqlite_conn;
        }

        public List<MusicEquipment> ReadData()
        {
            SQLiteConnection conn = CreateConnection(); 
            List<MusicEquipment> list = new List<MusicEquipment>(); 
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM MusicEquipment";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string name = sqlite_datareader.GetString(0);
                double price = sqlite_datareader.GetDouble(1);
                Console.WriteLine($"name: {name} price: {price.ToString()}");
                MusicEquipment me = new MusicEquipment();
                me.Name = name;
                me.Price = price;   
                list.Add(me);
            }
            conn.Close();
            return list;
        }


    }
}
