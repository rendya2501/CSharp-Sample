using System.Windows;
using System;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LocalDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Execute();

            void Execute()
            {
                try
                {
                    // 接続文字列の構築
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
                    {
                        DataSource = @"(LocalDB)\MSSQLLocalDB",
                        AttachDBFilename = System.IO.Path.GetFullPath(@"..\..\SampleDatabase.mdf"),
                        IntegratedSecurity = true,
                    };
                    // 接続オブジェクト生成
                    using SqlConnection connection = new SqlConnection(builder.ConnectionString);
                    // サーバー接続
                    connection.Open();
                    // SQLコマンド生成
                    StringBuilder query = new StringBuilder()
                        .AppendLine("DROP TABLE IF EXISTS [Products];")
                        .AppendLine("CREATE TABLE [Products] ( ")
                        .AppendLine("    [Id]    INT        NOT NULL,")
                        .AppendLine("    [name]  NCHAR (32) NULL, ")
                        .AppendLine("    [price] INT        NULL ")
                        .AppendLine("    PRIMARY KEY CLUSTERED ([Id] ASC)")
                        .AppendLine("); ")
                        .AppendLine("INSERT INTO [Products]")
                        .AppendLine("VALUES ")
                        .AppendLine("    (1,N'ペンギンクッキー',150), ")
                        .AppendLine("    (2,N'シロクマアイス',200), ")
                        .AppendLine("    (3,N'らくだケーキ',320), ")
                        .AppendLine("    (4,N'くじらキャンディー',60), ")
                        .AppendLine("    (5,N'ふくろうサブレ',120); ")
                        .AppendLine("SELECT * FROM [Products];");
                    using SqlCommand com = new SqlCommand(query.ToString(), connection);
                    // SQL実行
                    using SqlDataReader reader = com.ExecuteReader();
                    // 結果表示
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader.GetString(1)},{reader.GetInt32(2)}");
                    }
                }
                catch (SqlException Ex)
                {
                    Console.WriteLine(Ex.ToString());
                }
            }
        }



        private void Execute()
        {
            try
            {
                Console.WriteLine("LocalDB に接続し、Create、Read、Update、Delete 操作のデモを行います。");
                Console.WriteLine("---");

                // 接続文字列の構築
                string constr = @"";
                // 接続オブジェクト生成
                using SqlConnection connection = new SqlConnection(constr);

                // サーバー接続
                Console.WriteLine("SQL Server に接続しています... ");
                connection.Open();
                Console.WriteLine("接続成功");
                Console.WriteLine("---");

                // CreateDataBase();
                CreateTable();
                CreateDefaultData();
                Insert();
                Update();
                Delete();
                //DropTbale();

                Task.WaitAll(
                    Task.Run(Read),
                    Task.Run(() =>
                    {
                        string sqlstr = "select * from products";
                        using SqlCommand com = new SqlCommand(sqlstr, connection);
                        // SQL実行
                        using SqlDataReader reader = com.ExecuteReader();
                        // 結果表示
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader.GetString(1)},{reader.GetInt32(2)}");
                        }
                    }
                    )
                );


                // データベースの作成
                void CreateDataBase()
                {
                    Console.WriteLine("既に作成されている SampleDB データベースを削除し、再作成します... ");
                    string sql = "DROP DATABASE IF EXISTS [SampleDB]; CREATE DATABASE [SampleDB]";
                    // コマンドがタイムアウトする場合は秒数を変更(ms) デフォルトは 30秒
                    using SqlCommand command = new SqlCommand(sql, connection) { CommandTimeout = 60000 };
                    command.ExecuteNonQuery();
                    Console.WriteLine("SampleDB データベースを作成しました。");
                    Console.WriteLine("---");
                }

                // テーブルの作成
                void CreateTable()
                {
                    Console.WriteLine("テーブルを作成");
                    StringBuilder query = new StringBuilder()
                        //.AppendLine("USE SampleDB; ")
                        .AppendLine("DROP TABLE IF EXISTS [Employees];")
                        .AppendLine("CREATE TABLE [Employees] ( ")
                        .AppendLine("    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ")
                        .AppendLine("    Name NVARCHAR(50), ")
                        .AppendLine("    Location NVARCHAR(50) ")
                        .AppendLine("); ");
                    using (SqlCommand command = new SqlCommand(query.ToString(), connection) { CommandTimeout = 60000 })
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("テーブル作成完了");
                        Console.WriteLine();
                    }
                    Read();
                }

                // サンプルデータの登録
                void CreateDefaultData()
                {
                    Console.WriteLine("デフォルトデータを作成します。");
                    StringBuilder query = new StringBuilder()
                        .AppendLine("INSERT INTO Employees (Name, Location)")
                        .AppendLine("VALUES ")
                        .AppendLine("    (N'Jared', N'Australia'), ")
                        .AppendLine("    (N'Nikita', N'India'), ")
                        .AppendLine("    (N'Tom', N'Germany'); ");
                    using (SqlCommand command = new SqlCommand(query.ToString(), connection) { CommandTimeout = 60000 })
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("作成完了");
                        Console.WriteLine();
                    }
                    Read();
                }

                // INSERT デモ
                void Insert()
                {
                    Console.WriteLine("テーブルに新しい行を挿入するには、任意のキーを押して続行します...");
                    StringBuilder query = new StringBuilder()
                        .AppendLine("INSERT Employees (Name, Location) ")
                        .AppendLine("VALUES (@name, @location);");
                    using (SqlCommand command = new SqlCommand(query.ToString(), connection) { CommandTimeout = 60000 })
                    {
                        command.Parameters.AddWithValue("@name", "Jake");
                        command.Parameters.AddWithValue("@location", "United States");
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected + " 行 挿入されました。");
                        Console.WriteLine();
                    }
                    Read();
                }

                // UPDATE デモ
                void Update()
                {
                    string userToUpdate = "Nikita";
                    Console.WriteLine("ユーザー名 '" + userToUpdate + "' の 'Location' を更新中です。任意のキーを押して処理を続行します...");
                    StringBuilder query = new StringBuilder()
                        .AppendLine("UPDATE Employees")
                        .AppendLine("SET Location = N'United States'")
                        .AppendLine("WHERE Name = @name");
                    using (SqlCommand command = new SqlCommand(query.ToString(), connection) { CommandTimeout = 60000 })
                    {
                        command.Parameters.AddWithValue("@name", userToUpdate);
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected + " 行 更新されました。");
                        Console.WriteLine();
                    }
                    Read();
                }

                // DELETE デモ
                void Delete()
                {
                    string userToDelete = "Jared";
                    Console.WriteLine("ユーザー名 '" + userToDelete + "' を削除中です。任意のキーを押して処理を続行します...");
                    StringBuilder query = new StringBuilder()
                        .AppendLine("DELETE FROM Employees WHERE Name = @name;");
                    using (SqlCommand command = new SqlCommand(query.ToString(), connection) { CommandTimeout = 60000 })
                    {
                        command.Parameters.AddWithValue("@name", userToDelete);
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected + " 行 削除されました。");
                        Console.WriteLine();
                    }
                    Read();
                }

                // Select
                void Read()
                {
                    StringBuilder query = new StringBuilder()
                        .AppendLine("SELECT Id, Name, Location FROM Employees;");
                    using SqlCommand command = new SqlCommand(query.ToString(), connection);
                    using SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader.GetInt32(0)} {reader.GetString(1)} {reader.GetString(2)}");
                        //_TextBox1.Text += $"{reader.GetInt32(0)} {reader.GetString(1)} {reader.GetString(2)}";
                    }
                    Console.WriteLine("---");
                }

                // テーブル削除
                void DropTbale()
                {
                    Console.WriteLine("SampleDB データベースを削除します。");
                    string sql = "DROP TABLE [Employees];";
                    //string sql = "DROP DATABASE [SampleDB];";
                    using SqlCommand command = new SqlCommand(sql, connection) { CommandTimeout = 60000 };
                    command.ExecuteNonQuery();
                    Console.WriteLine("削除完了");
                    Console.WriteLine("---");
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("全て完了しました");
        }

    }
}

