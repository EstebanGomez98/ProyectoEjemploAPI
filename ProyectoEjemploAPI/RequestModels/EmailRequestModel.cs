namespace ProyectoEjemploAPI.RequestModels
{
    public class EmailRequestModel
    {
        public string Destinatario { get; set; }
        public string Aasunto { get; set; }
        public string Mensaje { get; set; }
        public bool EsHtml { get; set; }
    }
}
