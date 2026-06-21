using System;
using System.Collections.Generic;

namespace Работа_с_БД_из_кода___Задание.Models;

public partial class ЖурналИнцидентов
{
    public int IdОтчёта { get; set; }

    public int IdУстройства { get; set; }

    public string РезультатМониторинга { get; set; } = null!;

    public DateTime МеткаВремени { get; set; }

    public int? ВремяОтветаМс { get; set; }

    public string? ОписаниеИнцидента { get; set; }

    public virtual СетевоеУстройство IdУстройстваNavigation { get; set; } = null!;
}
