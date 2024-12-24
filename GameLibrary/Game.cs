using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary;

public class Game
{
    public int Id { get; set; } // Первинний ключ
    public string Name { get; set; } // Назва гри
    public string Developer { get; set; } // Студія/фірма-розробник
    public string Genre { get; set; } // Стиль гри
    public DateTime ReleaseDate { get; set; } // Дата релізу

    public string Mode { get; set; } // Режим гри: однокористувацький, багатокористувацький
    public int CopiesSold { get; set; } // Кількість проданих копій
}
