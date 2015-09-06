using LiveSplit.ComponentUtil;
using System.Diagnostics;

namespace LiveSplit.Skyrim
{
    public class GameDataItem<T> where T : struct
    {
        public T Current { get; private set; }
        public T Previous { get; private set; }
        public bool HasChanged { get; private set; }
        public bool DerefSuccess { get; private set; }
        public bool PrevDerefSuccess { get; private set; }

        public delegate void ValueEventHandler(GameDataItem<T> sender, T newValue);
        public delegate void ValueChangingEventHandler(GameDataItem<T> sender, ref T newValue);
        public event ValueChangingEventHandler OnValueChanging;
        public event ValueEventHandler OnValueChanged;
        public event ValueEventHandler OnSetValue;

        private Process _game;

        public GameDataItem(Process game = null)
        {
            Previous = default(T);
            Current = default(T);
            HasChanged = false;
            DerefSuccess = true;
            PrevDerefSuccess = false;
            _game = game;
        }

        public void SetValue(T newValue, bool updatePreviousValue = true)
        {
            this.HasChanged = !newValue.Equals(this.Current);

            if (this.OnValueChanging != null && this.HasChanged)
                this.OnValueChanging(this, ref newValue);

            if (updatePreviousValue)
            {
                this.Previous = this.Current;
                this.PrevDerefSuccess = this.DerefSuccess;
            }

            this.Current = newValue;

            if (this.OnSetValue != null)
                this.OnSetValue(this, newValue);

            if (this.OnValueChanged != null && this.HasChanged)
                this.OnValueChanged(this, newValue);
        }

        public void SetValue(Process p, DeepPointer ptr, bool updatePreviousValue = true)
        {
            T value;
            this.DerefSuccess = ptr.Deref(p, out value);
            this.SetValue(value, updatePreviousValue);
        }

        public void SetValue(DeepPointer ptr, bool updatePreviousValue = true)
        {
            if (_game == null)
                throw new System.InvalidOperationException();

            SetValue(_game, ptr, updatePreviousValue);
        }

        public override string ToString()
        {
            return Current.ToString();
        }
    }
}
