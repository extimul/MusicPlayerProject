namespace MusicPlayer.Core.Models
{
    public abstract class BaseModel
    {
        public virtual int Id { get; set; }
        public virtual int GetId()
        {
            return Id - 1;
        }
    }
}
