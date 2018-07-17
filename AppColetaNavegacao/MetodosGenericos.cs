using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppColetaNavegacao.Models;
using System.Text;
using RabbitMQ.Client;
using AppColetaNavegacao.Others;

namespace AppColetaNavegacao
{
    public class MetodosGenericos
    {

        public void PublisherInformation(InformationNavigation obj)
        {
            //string message = JsonConvert.SerializeObject(obj);

            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "filaNavegacao",
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                var command = new InformationNavigation
                {
                    Descricao = obj.Descricao,
                    Data = obj.Data,
                    OPERACAO = obj.OPERACAO
                };

                string message = JsonConvert.SerializeObject(command);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                        routingKey: "filaNavegacao",
                                        basicProperties: null,
                                        body: body);
            }
        }

        public static string BuscarParametros(object obj)
        {
            string result = "";

            string[] propertyNames = obj.GetType().GetProperties().Select(p => p.Name).ToArray();

            for (int i = 0; i < propertyNames.Length; i++)
            {
                //Recupera valor do atributo
                if (!obj.GetType().GetProperty(propertyNames[i]).PropertyType.Namespace.ToString().Equals("System.Collections.Generic"))
                {
                    object propValue = obj.GetType().GetProperty(propertyNames[i]).GetValue(obj, null);

                    //concatena o atributo com o seu valor
                    result += propertyNames[i] + ": " + propValue + ", ";
                }
            }
            return result.Substring(0, result.Length - 2);
        }


        public void PublicaInformacaoNavegacao(object obj, OPERACAO operacao)
        {
            InformationNavigation objInformationNavigation = new InformationNavigation();
            objInformationNavigation.OPERACAO = operacao;
            objInformationNavigation.Data = DateTime.Now;
            objInformationNavigation.Descricao = BuscarParametros(obj);
            (new MetodosGenericos()).PublisherInformation(objInformationNavigation);
        }

        public static bool UsuarioAtivo(System.Security.Claims.ClaimsPrincipal user)
        {
            return user.Identity.IsAuthenticated;
        }
    }
}
