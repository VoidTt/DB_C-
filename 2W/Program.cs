using System;
using System.Data.SqlClient;
using CollegeDB.Data;
using CollegeDB.Models;

namespace CollegeDB
{
    class Program
    {
        static string connectionString =
            "Data Source=(localdb)\\MSSQLLocalDB;" +
            "Initial Catalog=TraineeDB;" +
            "Integrated Security=True;" +
            "TrustServerCertificate=True;";

        static StudentRepository student_repo = new StudentRepository(connectionString);
        static GroupRepository group_repo = new GroupRepository(connectionString);

        static void Main()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("=== Добро пожаловать в журнал ===");
                Console.WriteLine("1. Показать всех студентов");
                Console.WriteLine("2. Показать все грууппы");
                Console.WriteLine("3. Добавить");
                Console.WriteLine("4. Редактировать");
                Console.WriteLine("5. Удалить");
                Console.WriteLine("6. Выход");
                Console.Write("Ваш выбор: ");

                switch (Console.ReadLine())
                {
                    case "1": ShowAllStudents(); break;
                    case "2": ShowAllGroups(); break;

                    case "6": running = false; break;
                    default: Console.WriteLine("Неверный выбор."); break;
                }

                if (running)
                {
                    Console.WriteLine("\nНажмите любую клавишу...");
                    Console.ReadKey();
                }
            }
        }

        static void ShowAllStudents()
        {
            var students = student_repo.GetAll();
            if (students.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }

            foreach (var s in students)
                Console.WriteLine(s);
        }

        static void ShowAllGroups()
        {
            var groups = group_repo.GetAllGroup();
            if (groups.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }

            foreach (var g in groups)
                Console.WriteLine(g);
        }

        public Student FindStudentById(string id)
        {
            using(var connection - new SqlConnection(conn_str)){
                return connectionString.QueryFirstOrDefault<student_repo>(
                    "SELECT FirstName, LastName, Age, GroupId" +
                    "FROM Students" +
                    "WHERE StudentId = @Id;",
                    new { @Id = id }
                    );
            }
        }
        public void AddStudent(string first_name, string last_name, string age, string group_id)
        {
            var student = new Student
            {
                first_name = first_name,
                last_name = last_name,
                age = int.Parse(age),
                group_id = int.Parse(group_id)
            };
            using(var connection = new SqlConnection(conn_str))
            {
                string sql = "INSERT INTO Students(FirstName, LastName, Age, GroupId)" +
                    "VALUES(@FirstName, @LastName, @Age, @GroupId)";
                connection.Execute(sql, new { @FirstName = first_name, @LastName = last_name, @Age = age, GroupId = group_id });
            }
        }
    }
}