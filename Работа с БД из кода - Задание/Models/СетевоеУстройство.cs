using System;
using System.Collections.Generic;

namespace Работа_с_БД_из_кода___Задание.Models;

public partial class СетевоеУстройство
{
    public int IdУстройства { get; set; }

    public int IdПользователя { get; set; }

    public string IpАдрес { get; set; } = null!;

    public string НазваниеУстройства { get; set; } = null!;

    public string Местоположение { get; set; } = null!;

    public string ИмяХоста { get; set; } = null!;

    public string ВерсияПрошивки { get; set; } = null!;

    public DateOnly? СрокГарантии { get; set; }

    public string? ТекущийСтатус { get; set; }

    public bool ФлагМониторинга { get; set; }

    public Guid GuidУстройства { get; set; }

    public decimal СтоимостьОбслуживания { get; set; }

    public virtual Пользователь IdПользователяNavigation { get; set; } = null!;

    public virtual ICollection<ЖурналИнцидентов> ЖурналИнцидентовs { get; set; } = new List<ЖурналИнцидентов>();

    public virtual ICollection<СетеваяСлужба> IdСлужбыs { get; set; } = new List<СетеваяСлужба>();
}
