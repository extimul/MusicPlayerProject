namespace MusicPlayer.Core.Models
{
    public abstract class BaseModel
    {
        public int Id { get; set; }
        public virtual int GetId()
        {
            return Id - 1;
        }
    }
}
