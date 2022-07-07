using System;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DeadLock
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Execute();
        }

        static void Execute()
        {
            // 接続文字列
            string constr = @"";

            // 接続オブジェクト生成
            using (SqlConnection connection = new SqlConnection(constr))
            {
                // データベース接続
                connection.Open();

                // テーブルの作成
                new Action(() =>
                {
                    Console.WriteLine("テーブルを作成");
                    StringBuilder query = new StringBuilder()
                        .AppendLine("DROP TABLE IF EXISTS [TEST_TBL1];")
                        .AppendLine("DROP TABLE IF EXISTS [TEST_TBL2];")
                        .AppendLine("CREATE TABLE [TEST_TBL1] ( ")
                        .AppendLine("    ID INT NOT NULL PRIMARY KEY, ")
                        .AppendLine("    Name NVARCHAR(50) ")
                        .AppendLine("); ")
                        .AppendLine("CREATE TABLE [TEST_TBL2] ( ")
                        .AppendLine("    ID INT NOT NULL PRIMARY KEY, ")
                        .AppendLine("    Name NVARCHAR(50) ")
                        .AppendLine("); ");
                    using (SqlCommand command = new SqlCommand(query.ToString(), connection) { CommandTimeout = 60000 })
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("テーブル作成完了");
                        Console.WriteLine();
                    }
                }).Invoke();

                // 初期データ挿入
                new Action(() =>
                {
                    StringBuilder query = new StringBuilder()
                      .AppendLine("INSERT [TEST_TBL1] (ID, Name) ")
                      .AppendLine("VALUES")
                      .AppendLine("    (1,'test001'),")
                      .AppendLine("    (2,'test002');")
                      .AppendLine("INSERT [TEST_TBL2] (ID, Name) ")
                      .AppendLine("VALUES")
                      .AppendLine("    (1,'test001'),")
                      .AppendLine("    (2,'test002');");
                    using (SqlCommand command = new SqlCommand(query.ToString(), connection) { CommandTimeout = 60000 })
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine(rowsAffected + " 行 挿入されました。");
                        Console.WriteLine();
                    }
                }).Invoke();
            }

            Task.WaitAll(
                Task.Run(Task1),
                Task.Run(Task2)
            );

            void Task1()
            {
                Console.WriteLine("UpdateTask1");
                int counter = 0;
                // 接続オブジェクト生成
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    // データベース接続
                    connection.Open();

                    using (SqlCommand command = new SqlCommand() { CommandTimeout = 60000 })
                    {
                        while (true)
                        {
                            try
                            {
                                using (SqlTransaction transaction = connection.BeginTransaction("query1"))
                                {
                                    command.Connection = connection;
                                    command.Transaction = transaction;

                                    command.CommandText = new StringBuilder()
                                        .AppendLine("UPDATE [TEST_TBL1]")
                                        .AppendLine("SET [Name] = 'aaaaa'")
                                        .AppendLine("WHERE ID = 2;")
                                        .ToString();
                                    command.ExecuteNonQuery();

                                    command.CommandText = new StringBuilder()
                                        .AppendLine("UPDATE [TEST_TBL1]")
                                        .AppendLine("SET [Name] = 'bbbbb'")
                                        .AppendLine("WHERE ID = 1;")
                                        .ToString();
                                    command.ExecuteNonQuery();

                                    transaction.Commit();
                                    transaction.Dispose();

                                    counter++;
                                    Console.WriteLine(counter);
                                    if (counter >= 200)
                                    {
                                        break;
                                    }
                                };
                            }
                            catch (SqlException ex)
                            {
                                Console.WriteLine("エラーNo:" + ex.Number.ToString() + Environment.NewLine + " エラーメッセージ：" + ex.Message.ToString());
                                return;
                            }
                        }
                    }
                }
            }

            void Task2()
            {
                Console.WriteLine("UpdateTask2");
                int counter = 0;
                // 接続オブジェクト生成
                using (SqlConnection connection = new SqlConnection(constr))
                {
                    // データベース接続
                    connection.Open();

                    using (SqlCommand command = new SqlCommand() { CommandTimeout = 60000 })
                    {
                        while (true)
                        {
                            try
                            {
                                using (SqlTransaction transaction = connection.BeginTransaction("query2"))
                                {
                                    command.Connection = connection;
                                    command.Transaction = transaction;

                                    command.CommandText = new StringBuilder()
                                        .AppendLine("UPDATE [TEST_TBL1]")
                                        .AppendLine("SET [Name] = 'ccccc'")
                                        .AppendLine("WHERE ID = 1;")
                                        .ToString();
                                    command.ExecuteNonQuery();

                                    command.CommandText = new StringBuilder()
                                        .AppendLine("UPDATE [TEST_TBL1]")
                                        .AppendLine("SET [Name] = 'ddddd'")
                                        .AppendLine("WHERE ID = 2;")
                                        .ToString();
                                    command.ExecuteNonQuery();

                                    transaction.Commit();
                                    transaction.Dispose();

                                    counter++;
                                    Console.WriteLine(counter);
                                    if (counter >= 200)
                                    {
                                        break;
                                    }
                                }
                            }
                            catch (SqlException ex)
                            {
                                Console.WriteLine("エラーNo:" + ex.Number.ToString() + Environment.NewLine + " エラーメッセージ：" + ex.Message.ToString());
                                return;
                            }
                        }
                    }
                }
            }

            Console.WriteLine("処理終了");
            Console.ReadLine();
        }
    }
}
