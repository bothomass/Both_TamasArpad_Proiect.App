using System;
using Both_TamasArpad_Proiect.Data;
using System.IO;

namespace Both_TamasArpad_Proiect;

public partial class App : Application
{
    static FigurineDatabaseContext database; 
    public static FigurineDatabaseContext Database 
    { 
        get 
        { if (database == null) 
            { 
                database = new FigurineDatabaseContext(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Figurine.db3")); 
            } 
            return database; 
        } 
    }
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}
