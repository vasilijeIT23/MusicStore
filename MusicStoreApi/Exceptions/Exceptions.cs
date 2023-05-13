namespace MovieStoreApi.Exceptions
{
    public class InvalidInputValueException : Exception
    {
        public InvalidInputValueException() : base(String.Format("Input request values not valid.")) { }
    }

    public class EntityDoesntExistException : Exception
    {
        public EntityDoesntExistException(object o) : base(String.Format("Entity with given id doesnt exist. " + o)) { }
    }

    public class MovieDoesntExistException : Exception
    {
        public MovieDoesntExistException(object o) : base(String.Format("Entity with given id doesnt exist. " + o)) { }
    }

    public class RequirementsNotSatisfiedException : Exception
    {
        public RequirementsNotSatisfiedException(object o) : base(String.Format("Entity doesnt satisfy requirements needed for this action to be succesfully completed. " + o)) { }
    }
}
