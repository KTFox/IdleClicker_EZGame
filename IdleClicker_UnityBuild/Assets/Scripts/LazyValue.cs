namespace IdleClicker
{
    public class LazyValue<T>
    {
        // Constructor
        public LazyValue(InitializerDelegate initializer)
        {
            this.initializer = initializer;
        }

        // Variables

        private T value;
        private bool hasInitialized = false;
        private InitializerDelegate initializer;

        public delegate T InitializerDelegate();

        // Properties

        public T Value
        {
            get
            {
                ForceInit();
                return value;
            }
            set
            {
                hasInitialized = true;
                this.value = value;
            }
        }


        // Methods

        public void ForceInit()
        {
            if (!hasInitialized)
            {
                value = initializer();
                hasInitialized = true;
            }
        }
    }
}
