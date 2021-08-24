namespace MusicPlayer.Core.Models
{
    public abstract class BaseModel
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        /// <summary>
        /// Start from zero. P.S for collections
        /// </summary>
        /// <returns></returns>
        public virtual int GetId()
        {
            return Id - 1;
        }
    }
}
