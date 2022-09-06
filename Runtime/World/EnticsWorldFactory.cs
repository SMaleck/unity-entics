namespace EntiCS.World
{
    public class EnticsWorldFactory
    {
        public static IEnticsWorld Create()
        {
            return new EnticsWorld();
        }
    }
}
