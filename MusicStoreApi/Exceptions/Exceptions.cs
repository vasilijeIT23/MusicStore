namespace MusicStoreApi.Exceptions
{
    public class InvalidInputValueException : Exception
    {
        public InvalidInputValueException() : base(String.Format("Input request values not valid.")) { }
    }

    public class EntityDoesntExistException : Exception
    {
        public EntityDoesntExistException() : base(String.Format("Entity with given id doesnt exist. ")) { }
    }

    public class MovieDoesntExistException : Exception
    {
        public MovieDoesntExistException() : base(String.Format("Entity with given id doesnt exist. ")) { }
    }

    public class RequirementsNotSatisfiedException : Exception
    {
        public RequirementsNotSatisfiedException() : base(String.Format("Entity doesnt satisfy requirements needed for this action to be succesfully completed. ")) { }
    }
}
