using System.Net;

namespace PathPilot.Shared.Abstractions.Exceptions;

public sealed record ExceptionResponse(object Response, HttpStatusCode StatusCode);