namespace Entities.Exceptions
{
    public sealed class IdParametersBadRequestExceptions : BadRequestException
    {
        public IdParametersBadRequestExceptions()
            :base("Parameter ids is null")
        { }
    }
}
