namespace Algoritmic
{
    public interface IAlgorithm
    {
        /// <param name="time">Время для работы алгоритма в секундах</param>
        void Play(int time);
        void PlayLooping();
        void Stop();
    }
}