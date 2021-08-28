using System;

namespace MusicPlayer.Core.Models
{
    public abstract class BaseMusicPlayerModel
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Author { get; set; }
        public virtual string ImageSource { get; set; }
        public virtual DateTime RecentlyPlay { get; set; }
        public virtual DateTime AddedDate { get; set; }
        public virtual TimeSpan Duration { get; set; }
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
