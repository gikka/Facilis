namespace Facilis.Domain.Entities
{
    public class Video
    {
        public int VideoId { get; set; }
        public string URL { get; set; }
        public int EventoId { get; set; }
        public virtual Evento Evento { get; set; }
    }
}
