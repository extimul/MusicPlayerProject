namespace MusicPlayerProject.Core.Managers.Audio
{
    public interface IAudioManager
    {
        void PlayPauseAudio();
        void NextTrack();
        void PreviousTrack();
        void DecreaseVolume();
        void IncreaseVolume();
        bool HasTracksInPlayList();
        void Mute();
    }
}
