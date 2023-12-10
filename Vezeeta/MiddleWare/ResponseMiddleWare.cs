using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.ResponseShape;

namespace Vezeeta.MiddleWare
{
    public class ResponseMiddleWare
    {
        private readonly RequestDelegate _next;

        public ResponseMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
                
            }
            catch (FileNotFoundException ex)
            {
               
                await httpContext.Response.WriteAsJsonAsync(new ErrorResponse { Errors = new { message = ex.Message }, statusCode = Convert.ToInt32(Enums.StatusCode.NotFound) });
                
                httpContext.Response.StatusCode = Convert.ToInt32(Enums.StatusCode.NotFound);
            }

        
            catch (Exception ex)
            {
               
                await httpContext.Response.WriteAsJsonAsync(new ErrorResponse { Errors =new {message =  ex.Message }, statusCode = Convert.ToInt32(Enums.StatusCode.BadRequest) });
                httpContext.Response.StatusCode = Convert.ToInt32(Enums.StatusCode.BadRequest);

            }

        }
        }
    }

