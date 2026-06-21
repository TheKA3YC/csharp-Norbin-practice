using System;
using System.Collections.Generic;

namespace Работа_с_БД_из_кода___Задание.Models;

public partial class Пользователь
{
    public int IdПользователя { get; set; }

    public int IdАдминистратора { get; set; }

    public string Фио { get; set; } = null!;

    public string Роль { get; set; } = null!;

    public string Логин { get; set; } = null!;

    public string Пароль { get; set; } = null!;

    public virtual ГлавныйАдминистратор IdАдминистратораNavigation { get; set; } = null!;

    public virtual ICollection<СетевоеУстройство> СетевоеУстройствоs { get; set; } = new List<СетевоеУстройство>();
}
