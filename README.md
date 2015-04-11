# Hmac.WebApi

HMAC authentication uses a secret key for each consumer which both consumer and server both know to hmac hash a message, HMAC256 should be used. Most of cases, hashed password of consumer is used as secret key.

The message normally is built from data in the HTTP request, or even customized data which is added into HTTP header, message might include:

1. Timestamp: time that request is sent (UTC or GMT time)
2. HTTP verb: GET, POST, PUT, DELETE.
3. post data and query string,
4. URL

Under the hood, HMAC authentication would be:

Consumer sends a HTTP request to web server, after building the signature (output of hmac hash), the template of HTTP request:
 
    User-Agent: {agent}   
    Host: {host}   
    Timestamp: {timestamp}
    Authentication: {username}:{signature}

Example for GET request:

    GET /webapi.hmac/api/values
    
    User-Agent: Fiddler    
    Host: localhost    
    Timestamp: Thursday, August 02, 2012 3:30:32 PM 
    Authentication: cuongle:LohrhqqoDy6PhLrHAXi7dUVACyJZilQtlDzNbLqzXlw=

The message to hash to get signature:

    GET\n
    Thursday, August 02, 2012 3:30:32 PM\n
    /webapi.hmac/api/values\n

Example for POST request with querystring (signature below is not correct, just an example)

    POST /webapi.hmac/api/values?key2=value2
    
    User-Agent: Fiddler    
    Host: localhost    
    Content-Type: application/x-www-form-urlencoded
    Timestamp: Thursday, August 02, 2012 3:30:32 PM 
    Authentication: cuongle:LohrhqqoDy6PhLrHAXi7dUVACyJZilQtlDzNbLqzXlw=
    
    key1=value1&key3=value3

The message to hash to get signature

    GET\n
    Thursday, August 02, 2012 3:30:32 PM\n
    /webapi.hmac/api/values\n
    key1=value1&key2=value2&key3=value3

Please note that form data and query string should be in order, so the code on server get querystring and form data to build correct message.

When HTTP request comes to server, an authentication action filter is implemented to parse the request to get information: HTTP verb, timestamp, uri, form data and query string, then based on these to build signature (use hmac hash) with secret key (hashed password) on the server.

The secret key is got from database with username on the request.

Then server code compares the signature on the request with the signature built, if equal, authentication is passed, otherwise, it failed.

The code to build signature:

    private static string ComputeHash(string hashedPassword, string message)
    {
        var key = Encoding.UTF8.GetBytes(hashedPassword.ToUpper());
        string hashString;

        using (var hmac = new HMACSHA256(key))
        {
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
            hashString = Convert.ToBase64String(hash);
        }

        return hashString;
    }

So, how to prevent relay attack?

Add constraint for the timestamp, something like: 

    servertime - X minutes|seconds  <= timestamp <= servertime + X minutes|seconds 

(servertime: time of request comming to server)

And, cache the signature of request in memory (use MemoryCache, should keep in limit of time). If the next request comes with the same signature with previous request, it will be rejected.
