using System;
using System.Collections.Generic;

namespace Работа_с_БД_из_кода___Задание.Models;

public partial class СетеваяСлужба
{
    public int IdСлужбы { get; set; }

    public string НазваниеСлужбы { get; set; } = null!;

    public int Порт { get; set; }

    public virtual ICollection<СетевоеУстройство> IdУстройстваs { get; set; } = new List<СетевоеУстройство>();
}
