using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BO;
[Serializable]
public class DoesntExistException : Exception, ISerializable
{
    public DoesntExistException() : base() { }
    public DoesntExistException(string message) : base(message) { }
    public DoesntExistException(string message, Exception inner) : base(message, inner) { }
    protected DoesntExistException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}

[Serializable]
public class InvalidInputExeption : Exception, ISerializable
{
    public InvalidInputExeption() : base() { }
    public InvalidInputExeption(string message) : base(message) { }
    public InvalidInputExeption(string message, Exception inner) : base(message, inner) { }
    protected InvalidInputExeption(SerializationInfo info, StreamingContext context) : base(info, context) { }
}

[Serializable]
public class AlreadyExistExeption : Exception, ISerializable
{
    public AlreadyExistExeption() : base() { }
    public AlreadyExistExeption(string message) : base(message) { }
    public AlreadyExistExeption(string message, Exception inner) : base(message, inner) { }
    protected AlreadyExistExeption(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
[Serializable]
public class CantDeleteItem : Exception, ISerializable
{
    public CantDeleteItem() : base() { }
    public CantDeleteItem(string message) : base(message) { }
    public CantDeleteItem(string message, Exception inner) : base(message, inner) { }
    protected CantDeleteItem(SerializationInfo info, StreamingContext context) : base(info, context) { }
}

