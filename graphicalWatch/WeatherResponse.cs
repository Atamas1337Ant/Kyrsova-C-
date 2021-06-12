using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphicalWatch
{
    class WeatherResponse
    {
        public TemperatureInfo Main { get; set; } //Вызывет функцию класса в котором устанавливаем значение температуры
    }
}
