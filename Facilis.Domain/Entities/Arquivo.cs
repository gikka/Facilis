namespace Facilis.Domain.Entities
{
    public class Arquivo
    {
        public int ArquivoId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public int EventoId { get; set; }
        public virtual Evento Evento { get; set; }
    }
}
