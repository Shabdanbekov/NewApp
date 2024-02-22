using NewApp;

using (ApplicationContext db = new())
{
    User tom = new() { FullName = "Tom", BirthDate = new DateTime(1988, 11, 15) };
    User alice = new() { FullName = "Alice", BirthDate = new DateTime(1995, 10, 20) };

    db.Users.Add(tom);
    db.Users.Add(alice);
    db.SaveChanges();
    Console.WriteLine("Объекты успешно сохранены");

    Console.WriteLine("Список объектов:");
    var users = db.Users.ToList();
    foreach (User u in users)
    {
      Console.WriteLine($"{u.Id}.{u.FullName} - {(u.BirthDate.HasValue ? u.BirthDate.Value.ToShortDateString() : "N/A")}");

    }

    Console.WriteLine("Выберите действие:");
    Console.WriteLine("1. Просмотр пользователей");
    Console.WriteLine("2. Добавить пользователя");
    Console.WriteLine("3. Обновить данные пользователя");
    Console.WriteLine("4. Удалить пользователя");
    Console.WriteLine("5. Выход");

    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.WriteLine("Список пользователей:");
            foreach (var user in db.Users)
            {      Console.WriteLine($"{user.Id}.{user.FullName} - {(user.BirthDate.HasValue ? user.BirthDate.Value.ToShortDateString() : "N/A")}");

            }
            break;
        case "2":
            Console.WriteLine("Введите полное имя пользователя:");
            string? fullName = Console.ReadLine();
            Console.WriteLine("Введите дату рождения пользователя в формате ГГГГ-ММ-ДД:");
            DateTime birthDate;
            if (DateTime.TryParse(Console.ReadLine(), out birthDate))
            {
                User newUser = new() { FullName = fullName, BirthDate = birthDate };
                db.Users.Add(newUser);
                db.SaveChanges();
                Console.WriteLine("Пользователь успешно добавлен.");
            }
            else
            {
                Console.WriteLine("Ошибка ввода даты рождения.");
            }
            break;
        case "3":
            Console.WriteLine("Введите Id пользователя, чьи данные вы хотите обновить:");
            int userIdToUpdate;
            if (int.TryParse(Console.ReadLine(), out userIdToUpdate))
            {
                User? userToUpdate = db.Users.FirstOrDefault(u => u.Id == userIdToUpdate);
                if (userToUpdate != null)
                {
                    Console.WriteLine("Введите новое полное имя пользователя:");
                    userToUpdate.FullName = Console.ReadLine();
                    Console.WriteLine("Введите новую дату рождения пользователя в формате ГГГГ-ММ-ДД:");
                    if (DateTime.TryParse(Console.ReadLine(), out birthDate))
                    {
                        userToUpdate.BirthDate = birthDate;
                        db.SaveChanges();
                        Console.WriteLine("Данные пользователя успешно обновлены.");
                    }
                    else
                    {
                        Console.WriteLine("Ошибка ввода даты рождения.");
                    }
                }
                else
                {
                    Console.WriteLine("Пользователь с указанным Id не найден.");
                }
            }
            else
            {
                Console.WriteLine("Ошибка ввода Id пользователя.");
            }
            break;
        case "4":
            Console.WriteLine("Введите Id пользователя, которого вы хотите удалить:");
            int userIdToDelete;
            if (int.TryParse(Console.ReadLine(), out userIdToDelete))
            {
                User? userToDelete = db.Users.FirstOrDefault(u => u.Id == userIdToDelete);
                if (userToDelete != null)
                {
                    db.Users.Remove(userToDelete);
                    db.SaveChanges();
                    Console.WriteLine("Пользователь успешно удален.");
                }
                else
                {
                    Console.WriteLine("Пользователь с указанным Id не найден.");
                }
            }
            else
            {
                Console.WriteLine("Ошибка ввода Id пользователя.");
            }
            break;
        case "5":
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Некорректный выбор. Пожалуйста, выберите действие от 1 до 5.");
            break;
    }
}
