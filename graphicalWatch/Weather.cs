using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace graphicalWatch
{
    class Weather
    {
        string url;
        string response;
        HttpWebRequest request; // Выполняет запрос к универсальному коду ресурса (URI)
        HttpWebResponse httpWebResponse; //доступ к HTTP
        WeatherResponse weatherResponse;

        public Weather()
        {
            initVariable();
        }
        
        public float getWeather()
        {
            return weatherResponse.Main.Temp; // Возвращаем погоду
        }

         private void initVariable()
        {
            url = "http://api.openweathermap.org/data/2.5/weather?q=Kyiv&units=metric&appid=db4deab5c32eb42fd3b5269112efef12";
            //Наш URL с сайта openweathermap

            request = (HttpWebRequest)WebRequest.Create(url);
            httpWebResponse = (HttpWebResponse)request.GetResponse(); // спользуюется для чтения данных, полученных от сервера.
            
            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))//класс StreamReader позволяет нам легко
                                                                                                     //считывать весь текст или отдельные строки из текстового файла.
            {
                response = streamReader.ReadToEnd(); //читаем до конца и записываем в переменную
            }
            weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response); //позволяет преобразовывать строки напрямую в структуры C#
        }

        
    }
}
