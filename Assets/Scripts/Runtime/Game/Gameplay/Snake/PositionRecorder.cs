using UnityEngine;

namespace Runtime.Game.Gameplay.Snake
{
    public class PositionRecorder
    {
        private const int MaxCapacity = 1000;

        private readonly TimedPosition[] _buffer = new TimedPosition[MaxCapacity];
        private int _start = 0;
        private int _count = 0;

        public void AddNode(Vector3 position, float time)
        {
            int index = (_start + _count) % MaxCapacity;
            _buffer[index] = new TimedPosition(position, time);

            if (_count < MaxCapacity)
                _count++;
            else
                _start = (_start + 1) % MaxCapacity;
        }

        public Vector3 GetPositionAtTime(float targetTime)
        {
            if (_count == 0)
                return Vector3.zero;

            int firstIndex = _start;
            int lastIndex = (_start + _count - 1) % MaxCapacity;

            if (targetTime <= _buffer[firstIndex].Time)
                return _buffer[firstIndex].Position;

            for (int i = 1; i < _count; i++)
            {
                int prevIndex = (_start + i - 1) % MaxCapacity;
                int currIndex = (_start + i) % MaxCapacity;

                if (_buffer[currIndex].Time >= targetTime)
                {
                    var before = _buffer[prevIndex];
                    var after = _buffer[currIndex];
                    float t = Mathf.InverseLerp(before.Time, after.Time, targetTime);
                    return Vector3.Lerp(before.Position, after.Position, t);
                }
            }

            return _buffer[lastIndex].Position;
        }

        private struct TimedPosition
        {
            public Vector3 Position { get; }
            public float Time { get; }

            public TimedPosition(Vector3 pos, float time)
            {
                Position = pos;
                Time = time;
            }
        }
    }
}