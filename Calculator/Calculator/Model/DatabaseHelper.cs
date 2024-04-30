using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Calculator.Model
{
    public static class DatabaseHelper
    {
        private static SQLiteConnection database;

        public static void Initialize()
        {
            if (database != null)
                return;

            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "calculator.db");
            database = new SQLiteConnection(dbPath);
            database.CreateTable<Calculators>();
        }

        public static void SaveDisplayText(string displayText)
        {
            Initialize();

            var savedData = database.Table<Calculators>().FirstOrDefault();
            if (savedData != null)
            {
                savedData.DisplayText = displayText;
                database.Update(savedData);
            }
            else
            {
                database.Insert(new Calculators { DisplayText = displayText });
            }
        }

        public static string GetSavedDisplayText()
        {
            Initialize();

            var savedData = database.Table<Calculators>().FirstOrDefault();
            return savedData?.DisplayText;
        }

        public static void DeleteSavedDisplayText()
        {
            Initialize();
            database.DeleteAll<Calculators>();
        }
    }
}