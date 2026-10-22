namespace Application
{
    public interface IInput
    {
        // Väntar inte: false om ingen tangent är nedtryckt
        bool TryRead(out Command command);

        // Väntar tills en giltig tangent trycks
        Command Read();

        // Släng tangenter som tryckts men inte lästs
        void Flush();
    }
}
