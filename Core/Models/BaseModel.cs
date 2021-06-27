namespace MusicPlayerProject.Core.Models
{
    public abstract class BaseModel
    {
        public int Id { get; set; }

        public abstract int GetId(); 
    }
}
