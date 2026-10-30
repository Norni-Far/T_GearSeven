namespace AudioService
{
    public interface ISoundService
    {
        void PlayTap();
        void PlayMove();
        void PlayBasketPlace();
        void PlayCounterPlace();
        void PlayBasketClosed();
        void PlayLevelComplete();
        void StopAll();
    }
}
