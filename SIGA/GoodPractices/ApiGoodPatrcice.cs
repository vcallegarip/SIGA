using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Siga
{
    //  Web Api Rules:
    ///
    //  Please read all rules and follow these guidelines when creating Web APIs. These are generally based on RESTful standards. Some conventions are specific to Interject
    ///
    //      1) Web Apis should organized so that there are many controllers with few methods. We should avoid few controllers that contain many methods. There is a limited exception to this: when a critical Parent/Child relationship between objects requires a sub-Route. eg. api/Client/{clientId}/Users
    //          Get()                       get collection of objects       DB equivalient      (the object type is known by Controller name)
    //          Post(ComplexObject dto)     create one object               C - Create
    //          Get(int Id)                 get one object by Id            R - Read
    //          Put(ComplexObject dto)      update one object               U - Update
    //          Delete(int Id)              delete one object               D - Delete
    //
    //
    //      2) Controllers and controller routes are ALWAYS named by a singular noun(object) not a verb(function). 
    //           -wrong:     public class SendEmailController : ApiController   <-- 'SendEmail' is a verb, and is not restful
    //           -wrong:     public class EmailsController : ApiController      <-- 'Emails' is a PLURAL noun and should be singluar. 'Email'
    //           -right:     public class EmailController : ApiController       <-- 'Email' is a noun and represents the object being acted upon
    //  
    //      3) Controller methods are named by HTTP VERBS (And with a suffix only if using a sub-route)
    //           -wrong:     public ComplexObject LookupData(string id)         <-- wrong because it does not use GET as its name
    //           -wrong:     public ComplexObject GetData(string id)            <-- although it starts with 'Get', top level Controller objects should use the simple name of 'Get', which is the proper restful name
    //
    //           -right:     public ComplexObject Get(string id)                <-- this is the normal, simple Method name
    //
    //                       [HttpGet]                                          <-- required, see #4) below
    //                       [Route("api/ComplexObject/ChildObject")]           <-- because there is a sub-route in the routing, the naming is different
    //           -right:     public ComplexObject GetChildObject(string id)     <-- the Get Method includes the SUFFIX of the sub-route 'ChildObject' ....'Get' + 'ChildObject'  = 'GetChildObject' <-- the Parent object uses the simple 'Get' method
    //  
    //      4) Controller methods must be marked with a matching HTTP VERB ATTRIBUTE when the method name is anything other then the core HTTP Verb names...     
    //         An attribute is NOT needed if the method name is exactly "Get" "Post" "Put" or "Delete". The attribute IS needed any other time. [HttpGet]    [HttpPost]    [HttpPut]    [HttpDelete]
    //
    //          -wrong:      public ComplexObject GetChildObject(string id)     <-- this is wrong because the method name is more then just 'Get' and there is no [HttpGet] attribute
    //
    //                       [HttpPost]
    //          -wrong:      public ComplexObject GetChildObject(string id)     <-- this is wrong because the method name is a 'Get' but the attribute is a [HttpPost]... this will respond to POST commands, but appears misleading by naming convention
    //  
    //                       [HttpGet]
    //          -wrong:      public ComplexObject Get(string id)                <-- this is wrong because the method name is a simple 'Get' so the extra attribute [HttpGet] is not needed
    //  
    //          -right:      public ComplexObject Get(string id)                <-- this is right because the method name is a simple 'Get' and needs no further attribute
    //
    //                       [HttpGet]
    //          -right:      public ComplexObject GetChildObject(string id)     <-- this is right because the method name is longer then a simple 'Get' so the extra attribute [HttpGet] is added for clarity
    //  
    //      5) Asp.Net supports a default auto mapping between the keyword 'id' and a Url Segment. We do not allow usage of this auto mapping at Interject, because an explicit naming is clearer.
    //         Asp.Net allows this url:'api/ComplexObject/aBcXyZ' to map the 'aBcXyZ' string to the 'id' variable in this method: 'public ComplexObject Get(string id)'
    //         This means that within the method, the variable 'id' would contain the value 'aBcXyZ'. BUT if the variable is named ANYTHING ELSE, such as 'myId', the mapping will not occur. This can lead to confusion, and so it should be avoided.
    //  
    //          -wrong:      public void Get(string id) // 'api/ControllerName/2'       where '2' is the 'id'   <-- this is wrong because the mapping of the url segment '2' to the 'id' variable is implicit and unclear (done automatically by ASP.net)
    //  
    //                       [Route("api/ComplexObject/{id}")] 
    //          -right:      public void Get(string id) // 'api/ControllerName/2'       where '2' is the 'id'   <-- this is right because the mapping of the url segment '2' to the 'id' variable is explicit and clear in the Route definition
    //  
    //          -right:      public void Get(string id) // 'api/ControllerName?id=2'    where '2' is the 'id'   <-- this is right because the value of the 'id' variable is clearly coming from the query string parameter
    //  
    //      6) API controllers should return strongly typed objects. Anonymous objects are harder to trace during maintenance.
    //           -wrong:     public object Get(string id)               <-- wrong because an object could be anything, and it makes the code obscure
    //           -wrong:     public Tuple  Get(string id)               <-- wrong because a Tuple could be anything, and it makes the code obscure
    //           -right:     public string Get(string id)               <-- right because it returns a known type
    //           -right:     public List<ComplexObject> Get(string id)  <-- right because it returns a known type (a collection of ComplexObject is a strongly typed object)
    //  
    //      7) API controllers should indicate their url either as a Route[] or as a Comment
    ///
    //           -wrong:     public ComplexObject Get(string id)                <-- wrong because the route isn't documented
    //
    //           -right:     // /api/ComplexObject?id=5
    //                       public ComplexObject Get(string id)                <-- right because the url is documented in a comment
    //
    //                       [HttpGet]
    //                       [Route("api/ComplexObject/{id}/Children")]         <-- right because the url is documented in a [Route()] attribute
    //           -right:     public List<ComplexObject> GetChildren(string id)  
    //  
    //      8) GET calls should pass one parameter in a query string, and many parameters in a .js object. This is unique to GET calls because of how data is passed in the URL
    //         Generally, 3 or less parameters should be directly in the url. 4 or more should be in a .js object.
    //
    //           -wrong:     $.ajax({
    //                          type: 'GET'
    //                          , url: '/api/BestPracticeSample?id=aBcXyZ&code=debugmode&flag=false&t=sample&encryption=pCY^sKM`EQ\9GvgR'       <-- too many parameters are in the query string. A .js object should be used instead.
    //                       })
    //  
    //           -right:     var myData = {
    //                          id:aBcXyZ,
    //                          &code:'debugmode',
    //                          flag:false,
    //                          t:'sample',
    //                          encryption:'pCY^sKM`EQ\9GvgR'
    //                       }  
    //                       $.ajax({
    //                          type: 'GET'
    //                          , url: '/api/BestPracticeSample'         <-- url is simple.          $.ajax will place .js objects into the URL as Query String Parameters
    //                          , data: myData                  <-- complex data is here    $.ajax will place .js objects into the URL as Query String Parameters
    //                       })
    //  
    //           -wrong:     var myData = {                     <-- placing single value into an object will work, but it is less clear with only a little data is sent
    //                          id:aBcXyZ
    //                       }  
    //                       $.ajax({
    //                          type: 'GET'
    //                          , url: '/api/BestPracticeSample'         
    //                          , data: myData                  
    //                       })
    //  
    //           -right:     $.ajax({
    //                          type: 'GET'
    //                          , url: '/api/BestPracticeSample?id=aBcXyZ'       <-- single parameter is placed directly into query string
    //                       })
    //  
    //      9) Always explicitly indicate JSON as the body for PUT and POST commands (any which use a body).
    //
    //          By Default,  objects are URL encoded in the body which passes this:
    //               Name=Bob&Count=5&IsActive=true&Children%5B0%5D%5BName%5D=Mary&Children%5B0%5D%5BCount%5D=4545&Children%5B0%5D%5BIsActive%5D=true&Children%5B0%5D%5BChildren%5D=&Children%5B1%5D%5BName%5D=Larry&Children%5B1%5D%5BCount%5D=327&Children%5B1%5D%5BIsActive%5D=false&Children%5B1%5D%5BChildren%5D=
    //          and which looks like this:
    //               Name                        Bob
    //               Count                       5
    //               IsActive                    true
    //               Children[0][Name]           Mary
    //               Children[0][Count]          4545
    //               Children[0][IsActive]       true
    //               Children[0][Children]
    //               Children[1][Name]           Larry
    //               Children[1][Count]          327
    //               Children[1][IsActive]       false
    //               Children[1][Children]
    //
    //
    //          BUT This does NOT serialize correctly to an object in the API because of the collection of objects.
    //          JSON handles this correctly by passing a true object
    //               {'Name':'Bob','Count':5,'IsActive':true,'Children':[{'Name':'Mary','Count':4545,'IsActive':true,'Children':null},{'Name':'Larry','Count':327,'IsActive':false,'Children':null}]}
    //          and which looks like this:
    //               {
    //                   'Name': 'Bob',
    //                   'Count': 5,
    //                   'IsActive': true,
    //                   'Children': [
    //                       {
    //                           'Name': 'Mary',
    //                           'Count': 4545,
    //                           'IsActive': true,
    //                           'Children': null
    //                       },
    //                       {
    //                           'Name': 'Larry',
    //                           'Count': 327,
    //                           'IsActive': false,
    //                           'Children': null
    //                       }
    //                   ]
    //               }
    //  
    //  
    //  
    //           -wrong:     $.ajax({
    //                          type: 'PUT' // or 'POST'
    //                          , url: '/api/BestPracticeSample'
    //                          , data: complexObject // <-- plain JS object uses default contentType: 'application/x-www-form-urlencoded; charset=UTF-8'
    //                      })
    //
    //  
    //           -right:    $.ajax({
    //                          type: 'PUT' // or 'POST'
    //                          , url: '/api/BestPracticeSample'
    //                          , data: JSON.stringify(complexObject) //           <--  1 of 2 ...force JSON by formatting the object as JSON in the first place.
    //                          , contentType: 'application/json;charset=utf-8' // <--  2 of 2 ...force JSON by indicating the JSON content type instead of URL ENCODED BODY
    //                      })
    //  
    //  
    //  
    //  
    //


    // NOTE!! quote: "By default, the ASP.NET Web API framework takes simple parameter types from the route and complex types from the request body"    ... http://www.asp.net/web-api/overview/creating-web-apis/creating-a-web-api-that-supports-crud-operations

    public class BestPracticeSampleController : ApiController
    {
        // /api/BestPracticeSample    ...standard GET [collection]
        public IEnumerable<ComplexObject> Get()
        {
            return ComplexObject.All;

            //this javascript
            //
            //          $.ajax({
            //              type: 'GET' // always passes data in URL
            //              , url: '/api/BestPracticeSample'
            //          })
            //              .done(function (returnData) {
            //                  $('#BestPracticeSampleResult1').html(JSON.stringify(returnData));
            //              })
            //              .fail(function (xhr, result, status) {
            //                  alert(JSON.stringify(xhr));;
            //              });
            //
            //
            //produced this call, which hit this controller
            //
            //
            //          GET http://localhost:49278/api/BestPracticeSample HTTP/1.1
            //          Host: localhost:49278
            //          Connection: keep-alive
            //          Accept: */*
            //          X-Requested-With: XMLHttpRequest
            //          User-Agent: Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36
            //          Referer: http://localhost:49278/ApiSample
            //          Accept-Encoding: gzip, deflate, sdch
            //          Accept-Language: en-US,en;q=0.8





        }

        // /api/BestPracticeSample?id=Bob    ...standard GET [single default parameter]
        public ComplexObject Get(string id) // NOTE!! compare to AlternateSample... GetWithNamedParameter(string myVariableName) ...the difference between 'string id' and 'string myVariableName'
        {
            return ComplexObject.All.FirstOrDefault(o => o.Name == id);

            //this javascript
            //      
            //           var right_Url_1 = '/api/BestPracticeSample?id=Bob'                                          //<-- preferred because it's both simple and intuitive
            //           var wrong_Url_2 = '/api/BestPracticeSample' //AND PASS DATA IN AJAX: , data: {id: 'Bob'}    //<-- not preferred, it is not simple. It is more explicit, but not any more explicit than #1
            //           var wrong_Url_3 = '/api/BestPracticeSample/Bob'                                             //<-- not preferred UNLESS explicitly naming route [Route('api/ComplexObject/{id}')]. ...Though it is simple, it is not intuitive without knowing the convention. 
            //                                                                                                       //    This can cause confusion for other developers. Avoid This Alternate. Although this works fine, the automatic mapping of an id to the Url string 
            //                                                                                                       //    introduces a one-off way of building the url only when the parameter is called 'id'.... for consistency, I would prefer that all URLs are built using 
            //                                                                                                       //    the same convention so that the name of the parameter doesn't alter the process. There may be exceptions for certain SEO scenarios. 
            //           
            //           $.ajax({
            //               type: 'GET' // always passes data in URL
            //               , url: '/api/BestPracticeSample?id=Bob'
            //           })
            //               .done(function (returnData) {
            //                   $('#BestPracticeSampleResult2').html(JSON.stringify(returnData));
            //               })
            //               .fail(function (xhr, result, status) {
            //                   alert(JSON.stringify(xhr));
            //               });
            //
            //
            //produced this call, which hit this controller
            //
            //          GET http://localhost:49278/api/BestPracticeSample?id=Bob HTTP/1.1
            //          Host: localhost:49278
            //          Connection: keep-alive
            //          Accept: */*
            //          X-Requested-With: XMLHttpRequest
            //          User-Agent: Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36
            //          Referer: http://localhost:49278/ApiSample
            //          Accept-Encoding: gzip, deflate, sdch
            //          Accept-Language: en-US,en;q=0.8


        }

        // /api/BestPracticeSample?name=Bob&count=5    ...GET with multiple parameters
        public ComplexObject Get(string name, int count)
        {

            return ComplexObject.All.FirstOrDefault(o => o.Name == name && o.Count == count);


            //this javascript
            //
            //          var theData = {
            //              name: 'Bob'
            //              , count: 5
            //          }
            //          
            //          $.ajax({
            //              type: 'GET' // always passes data in URL
            //              , url: '/api/BestPracticeSample'
            //              , data: theData // because this is a GET, this data is moved to the URL even though this looks as if it will go in a body
            //          })
            //              .done(function (returnData) {
            //                  $('#BestPracticeSampleResult3').html(JSON.stringify(returnData));
            //              })
            //              .fail(function (xhr, result, status) {
            //                  alert(JSON.stringify(xhr));
            //              });
            //
            //
            //
            //produced this call, which hit this controller
            //
            //          GET http://localhost:49278/api/BestPracticeSample?name=Bob&count=5 HTTP/1.1
            //          Host: localhost:49278
            //          Connection: keep-alive
            //          Accept: */*
            //          X-Requested-With: XMLHttpRequest
            //          User-Agent: Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36
            //          Referer: http://localhost:49278/ApiSample
            //          Accept-Encoding: gzip, deflate, sdch
            //          Accept-Language: en-US,en;q=0.8



        }

        // /api/BestPracticeSample    ...standard POST
        public ComplexObject Post(ComplexObject dto)
        {
            // NOTE! There is a duplication conflict between these two Api signatures: since both are nullable, they can service the same request
            //          public ComplexObject Post([FromBody] string value)
            //          public ComplexObject Post([FromBody]ComplexObject dto)

            var result = new ComplexObject();
            result.Name = dto.Name;
            result.Count = dto.Count;
            result.IsActive = dto.IsActive;
            ComplexObject.All.Add(result);
            return result;

            //this javascript
            //
            //          var complexObject = { //< -- create new object
            //              Name: 'Fred',
            //              Count: 1122,
            //              IsActive: false,
            //              Children: [{ Name: 'Freddy Jr', Count: 1, IsActive: true, Children: null }]
            //          }
            //          
            //          $.ajax({
            //              type: 'POST' // For POST or PUT, use JSON instead of the default Url-Encoded body
            //              , url: '/api/BestPracticeSample'
            //              , data: JSON.stringify(complexObject) //           <--  1 of 2 ...force JSON format
            //              , contentType: 'application/json;charset=utf-8' // <--  2 of 2 ...force JSON header
            //          })
            //                .done(function (returnData) {
            //                    $('#BestPracticeSampleResult4').html(JSON.stringify(returnData));
            //                })
            //                .fail(function (xhr, result, status) {
            //                    alert(JSON.stringify(xhr));
            //                });
            //
            //
            //produced this call, which hit this controller
            //
            //          POST http://localhost:49278/api/BestPracticeSample HTTP/1.1
            //          Host: localhost:49278
            //          Connection: keep-alive
            //          Content-Length: 121
            //          Accept: */*
            //          Origin: http://localhost:49278
            //          X-Requested-With: XMLHttpRequest
            //          User-Agent: Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36
            //          Content-Type: application/json;charset=UTF-8
            //          Referer: http://localhost:49278/ApiSample
            //          Accept-Encoding: gzip, deflate
            //          Accept-Language: en-US,en;q=0.8
            //          
            //body-->   {"Name":"Fred","Count":1122,"IsActive":false,"Children":[{"Name":"Freddy Jr","Count":1,"IsActive":true,"Children":null}]}
            //(JSON is best practice)
            //
            //

        }

        // /api/BestPracticeSample    ...standard PUT
        public ComplexObject Put(string id, ComplexObject dto)
        {
            var result = ComplexObject.All.FirstOrDefault(o => o.Name == id);
            result.Name = dto.Name;
            result.Count = dto.Count;
            result.IsActive = dto.IsActive;
            return result;


            //this javascript
            //
            //          var complexObject = {
            //              Name: 'Bob',
            //              Count: 55155, //< -- updated value
            //              IsActive: true,
            //              Children: [
            //                  { 'Name': 'Mary', 'Count': 4545, 'IsActive': true, 'Children': null },
            //                  { 'Name': 'Larry', 'Count': 327, 'IsActive': false, 'Children': null }]
            //          }
            //          
            //          // for PUT, the URL needs to identify the item to update, and then pass the new item via the body
            //          var identifier = 'Bob'
            //          var myUrl = '/api/BestPracticeSample?id=' + identifier; // expected to create: /api/BestPracticeSample?id=Bob      // WCN Specific fix for Netscaler Url rewrite issue 'BuildRelativeUrl'
            //          
            //          $.ajax({
            //              type: 'PUT' // WEB API CAN ACCEPT primitive Types via the URL even for POSTs
            //                          // For POST or PUT, use JSON instead of the default Url-Encoded body
            //              , url:  myUrl // constructed above
            //              , data: JSON.stringify(complexObject) //           <--  1 of 2 ...force JSON format
            //              , contentType: 'application/json;charset=utf-8' // <--  2 of 2 ...force JSON header
            //          })
            //              .done(function (returnData) {
            //                  $('#BestPracticeSampleResult5').html(JSON.stringify(returnData));
            //              })
            //              .fail(function (xhr, result, status) {
            //                  alert(JSON.stringify(xhr));
            //              });
            //
            //
            //
            //produced this call, which hit this controller
            //
            //          PUT http://localhost:49278/api/BestPracticeSample?id=Bob HTTP/1.1
            //          Host: localhost:49278
            //          Connection: keep-alive
            //          Content-Length: 180
            //          Accept: */*
            //          Origin: http://localhost:49278
            //          X-Requested-With: XMLHttpRequest
            //          User-Agent: Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36
            //          Content-Type: application/json;charset=UTF-8
            //          Referer: http://localhost:49278/ApiSample
            //          Accept-Encoding: gzip, deflate, sdch
            //          Accept-Language: en-US,en;q=0.8
            //          
            //body-->   {"Name":"Bob","Count":55155,"IsActive":true,"Children":[{"Name":"Mary","Count":4545,"IsActive":true,"Children":null},{"Name":"Larry","Count":327,"IsActive":false,"Children":null}]}            
            //(JSON is best practice)
            //
            //

        }

    }

    public class AlternateSampleController : ApiController
    {
        // /api/AlternateSample?myVariableName=Bob     ...standard GET [single custom-named parameter]
        [HttpGet]
        public ComplexObject GetWithNamedParameter(string myVariableName) // NOTE!! compare to BestPracticeSample... Get(string id) ...the difference between 'string id' and 'string myVariableName'
        {
            return ComplexObject.All.FirstOrDefault(o => o.Name == myVariableName);

            //this javascript
            //      
            //           var right_Url_1 = '/api/AlternateSample?myVariableName=Bob'       //<-- preferred because it's both simple and intuitive
            //           var wrong_Url_2 = '/api/AlternateSample/Bob'                      //<-- avoid this UNLESS explicitly naming route [Route('api/ComplexObject/{myVariableName}')]. Do NOT use implicit auto-mapping to the 'Id' keyword variable name
            //           
            //           $.ajax({
            //               type: 'GET' // always passes data in URL
            //               , url: '/api/AlternateSample?myVariableName=Bob'
            //           })
            //               .done(function (returnData) {
            //                   $('#AlternateSampleResult1').html(JSON.stringify(returnData));
            //               })
            //               .fail(function (xhr, result, status) {
            //                   alert(JSON.stringify(xhr));
            //               });
            //
            //
            //
            //produced this call, which hit this controller
            //
            //          GET http://localhost:49278/api/AlternateSample?myVariableName=Bob HTTP/1.1
            //          Host: localhost:49278
            //          Connection: keep-alive
            //          Accept: */*
            //          X-Requested-With: XMLHttpRequest
            //          User-Agent: Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36
            //          Referer: http://localhost:49278/ApiSample
            //          Accept-Encoding: gzip, deflate, sdch
            //          Accept-Language: en-US,en;q=0.8


        }

        // POST /api/AlternateSample    ...using single string !!!!!! THIS CAN BE AN ODD ONE !!!!!!! on the javascript side
        public ComplexObject Post([FromBody] string username) // [FromBody] is needed because of using single primitive type, compared to URI as in PostFromUrl(string name, int count, bool isActive)
        {
            // NOTE! uses [FromBody] to specify that the string is in the body and not the URL

            var result = new ComplexObject();
            result.Name = username != null ? username : "null username";
            ComplexObject.All.Add(result);
            return result;


            //BEST WAY... this javascript
            //
            //          var theData = 'Jim'; // use simple javascript string, and pass as JSON
            //          
            //          $.ajax({
            //              type: 'POST' // For POST or PUT, use JSON instead of the default Url-Encoded body
            //              , url: '/api/AlternateSample'
            //              , data: JSON.stringify(theData) //                 <--  1 of 2 ...force JSON format
            //              , contentType: 'application/json;charset=utf-8' // <--  2 of 2 ...force JSON header
            //          })
            //              .done(function (returnData) {
            //                  $('#AlternateSampleResult3').html(JSON.stringify(returnData));
            //              })
            //              .fail(function (xhr, result, status) {
            //                  alert(JSON.stringify(xhr));
            //              });
            //
            //
            //produced this call, which hit this controller
            //
            //          POST http://localhost:49278/api/AlternateSample HTTP/1.1
            //          Host: localhost:49278
            //          Connection: keep-alive
            //          Content-Length: 5
            //          Accept: */*
            //          Origin: http://localhost:49278
            //          X-Requested-With: XMLHttpRequest
            //          User-Agent: Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36
            //          Content-Type: application/json;charset=UTF-8
            //          Referer: http://localhost:49278/ApiSample
            //          Accept-Encoding: gzip, deflate
            //          Accept-Language: en-US,en;q=0.8
            //          
            //body-->   "Jim"            
            //(JSON is best practice)
            //          
            //
            //
            //
            //
            //ANOTHER WAY (worse)... this javascript
            //
            //          // send oddly formatted object to resolve to Microsoft's handling of single primitive strings in the Url-Encoded body
            //          var theData = {
            //              // NOTE order of params must match WebApi...        public bool GetIsUsernameAndEmailAvailable(string PrimaryEmail)
            //              // NOTE sending single primitive types (not complex object), the JSON object should have empty leading identifier...
            //              //This will work { '' : ''myString'' } results in body of ' =myString '                            ...Binds to     Post([FromBody]string stringName)
            //              //This WONT work { stringName : 'myString' }   results in body of   ' stringName=myString '      ...This will NOT bind in ASP.netWebAPI because of how it decifer between Model binding and Formatters binding
            //              '': 'Joe' //single string
            //          }
            //          
            //          $.ajax({
            //              type: 'POST' // NOT PREFERRED... This will Pass via the body as URL encoded string
            //              , url: '/api/AlternateSample'
            //              , data: theData //pass the JS object
            //          })
            //              .done(function (returnData) {
            //                  $('#AlternateSampleResult3').html(JSON.stringify(returnData));
            //              })
            //              .fail(function (xhr, result, status) {
            //                  alert(JSON.stringify(xhr));
            //              });
            //
            //produced this call, which hit this controller
            //
            //          POST http://localhost:49278/api/AlternateSample HTTP/1.1
            //          Host: localhost:49278
            //          Connection: keep-alive
            //          Content-Length: 4
            //          Accept: */*
            //          Origin: http://localhost:49278
            //          X-Requested-With: XMLHttpRequest
            //          User-Agent: Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36
            //          Content-Type: application/x-www-form-urlencoded; charset=UTF-8
            //          Referer: http://localhost:49278/ApiSample
            //          Accept-Encoding: gzip, deflate
            //          Accept-Language: en-US,en;q=0.8
            //
            //body-->   =Joe
            //(Url-Encoded, which is worse)
            //
            //

        }

        // GET /api/AlternateSample?object=InQuery    ...using a complex object instead of primitive types
        public ComplexObject Get([FromUri]ComplexObject dto)
        {
            //NOTE [FromUri] in the method signature

            //NOTE these bindings can easily be in conflict with each other, resulting in duplicate matches:
            //          public ComplexObject Get()                          ...a dto is nullable, thus the same as no parameter, so these can overlap
            //          public ComplexObject Get([FromUri]ComplexObject dto)

            return ComplexObject.All.FirstOrDefault(
                o => o.Name == dto.Name
                && o.Count == dto.Count
                && o.IsActive == dto.IsActive);


            //this javascript
            //
            //          var complexObject = {
            //              Name: 'Bob'
            //              , Count: 5
            //              , IsActive: true
            //          }
            //          
            //          $.ajax({
            //              type: 'GET' // always passes data in URL
            //              , url: '/api/AlternateSample'
            //              , data: complexObject // because this is a GET, this data is moved to the URL even though this looks as if it will go in a body
            //          })
            //              .done(function (returnData) {
            //                  $('#AlternateSampleResult2').html(JSON.stringify(returnData));
            //              })
            //              .fail(function (xhr, result, status) {
            //                  alert(JSON.stringify(xhr));
            //              });
            //
            //
            //produced this call, which hit this controller
            //
            //          GET http://localhost:49278/api/AlternateSample?Name=Bob&Count=5&IsActive=true HTTP/1.1
            //          Host: localhost:49278
            //          Connection: keep-alive
            //          Accept: */*
            //          X-Requested-With: XMLHttpRequest
            //          User-Agent: Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36
            //          Referer: http://localhost:49278/ApiSample
            //          Accept-Encoding: gzip, deflate, sdch
            //          Accept-Language: en-US,en;q=0.8


        }

        // POST /api/AlternateSample?name=Mike&isActive=false&count=989    ...using only primitive types
        [HttpPost]
        public ComplexObject PostFromUrl(string name, int count, bool isActive)
        {
            var result = new ComplexObject();
            result.Name = name;
            result.Count = count;
            result.IsActive = isActive;
            ComplexObject.All.Add(result);
            return result;

            //this javascript
            //
            //          var complexObject = {
            //              Name: 'Mike' ,
            //              Count: 989 ,
            //              IsActive: false ,
            //              // !!!!!!!!!!!! ALERT !!!!!!!!!!!!!!!!!!
            //              // !!!!!!!!!!!! ALERT !!!!!!!!!!!!!!!!!!
            //              // !! posting as a Url-Encoded body BREAKS when there are child collections !!
            //              // will be null -->    Children: [
            //              // will be null -->        { 'Name': 'Mary', 'Count': 4545, 'IsActive': true, 'Children': null },
            //              // will be null -->        { 'Name': 'Larry', 'Count': 327, 'IsActive': false, 'Children': null }]
            //          }
            //          
            //          
            //          $.ajax({
            //              type: 'POST' // WEB API CAN ACCEPT primitive Types via the URL even for POSTs
            //              , url: '/api/AlternateSample?' + $.param(complexObject) // NOT RECOMMENDED, but possible...  Add the object to the URL, instead of the body// only do this when needing to support links or bookmarks that will POST
            //              //, data: theData // DO NOT PASS DATA HERE (body)
            //          })
            //              .done(function (returnData) {
            //                  $('#AlternateSampleResult4').html(JSON.stringify(returnData));
            //              })
            //              .fail(function (xhr, result, status) {
            //                  alert(JSON.stringify(xhr));
            //              });
            //
            //
            //produced this call, which hit this controller
            //
            //          POST http://localhost:49278/api/AlternateSample?Name=Mike&Count=989&IsActive=false HTTP/1.1
            //          Host: localhost:49278
            //          Connection: keep-alive
            //          Content-Length: 0
            //          Accept: */*
            //          Origin: http://localhost:49278
            //          X-Requested-With: XMLHttpRequest
            //          User-Agent: Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36
            //          Referer: http://localhost:49278/ApiSample
            //          Accept-Encoding: gzip, deflate
            //          Accept-Language: en-US,en;q=0.8
            //          
            //
            //(no body)
            //

        }

    }

    public class RestfulUrlSampleController : ApiController
    {
        // GET nested properies with RESTful route
        [Route("api/RestfulUrlSample/{name}/Children")] // route contains named string as ID to idetify the parent object before asking for nested objects. ALWAYS use the same controller name (this can be overridden, but NEVER should as it is a maintenance problem)
        public IEnumerable<ComplexObject> Get(string name)
        {

            var parent = ComplexObject.All.FirstOrDefault(o => o.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            return parent != null ? parent.Children : null;

            //this javascript
            //
            //          var name = 'bob';
            //          var apiUrl = 'api/RestfulUrlSample/' + name + '/Children'; // produces 'api/RestfulUrlSample/bob/Children'
            //          
            //          $.ajax({
            //              type: 'GET' // always passes data in URL
            //              , url: apiUrl
            //          })
            //              .done(function (returnData) {
            //                  $('#RestfulUrlSampleResult1').html(JSON.stringify(returnData));
            //              })
            //              .fail(function (xhr, result, status) {
            //                  alert(JSON.stringify(xhr));
            //              });
            //
            //
            //
            //produced this call, which hit this controller
            //
            //          GET http://localhost:49278/api/RestfulUrlSample/bob/Children HTTP/1.1
            //          Host: localhost:49278
            //          Connection: keep-alive
            //          Accept: */*
            //          X-Requested-With: XMLHttpRequest
            //          User-Agent: Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36
            //          Referer: http://localhost:49278/ApiSample
            //          Accept-Encoding: gzip, deflate, sdch
            //          Accept-Language: en-US,en;q=0.8




        }
    }

    public class DefaultUrlEncodedBodySampleController : ApiController
    {
        // POST /api/DefaultUrlEncodedBodySample    ...using default FORM URL ENCODED DATA instead of best practice use of JSON 
        public ComplexObject Post(ComplexObject dto)
        {
            // NOTE! There is a duplication conflict between these two Api signatures: since both are nullable, they can service the same request
            //          public ComplexObject Post([FromBody] string value)
            //          public ComplexObject Post([FromBody]ComplexObject dto)

            var result = new ComplexObject();
            result.Name = dto.Name;
            result.Count = dto.Count;
            result.IsActive = dto.IsActive;
            ComplexObject.All.Add(result);
            return result;

            //this javascript
            //
            //          var complexObject = {
            //              Name: 'Dan',
            //              Count: 544,
            //              IsActive: true,
            //              // !!!!!!!!!!!! ALERT !!!!!!!!!!!!!!!!!!
            //              // !!!!!!!!!!!! ALERT !!!!!!!!!!!!!!!!!!
            //              // !! posting as a Url-Encoded body BREAKS when there are child collections !!
            //              // will break -->    Children: [
            //              // will break -->        { 'Name': 'Mary', 'Count': 4545, 'IsActive': true, 'Children': null },
            //              // will break -->        { 'Name': 'Larry', 'Count': 327, 'IsActive': false, 'Children': null }]
            //          }
            //          
            //            $.ajax({
            //                type: 'POST' // Pass via the body as URL encoded string
            //                , url: '/api/DefaultUrlEncodedBodySample'
            //                , data: complexObject
            //          })
            //              .done(function (returnData) {
            //                  $('#DefaultUrlEncodedBodySampleResult2').html(JSON.stringify(returnData));
            //              })
            //              .fail(function (xhr, result, status) {
            //                  alert(JSON.stringify(xhr));
            //              });
            //
            //
            //produced this call, which hit this controller
            //
            //          POST http://localhost:49278/api/DefaultUrlEncodedBodySample HTTP/1.1
            //          Host: localhost:49278
            //          Connection: keep-alive
            //          Content-Length: 32
            //          Accept: */*
            //          Origin: http://localhost:49278
            //          X-Requested-With: XMLHttpRequest
            //          User-Agent: Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36
            //          Content-Type: application/x-www-form-urlencoded; charset=UTF-8
            //          Referer: http://localhost:49278/ApiSample
            //          Accept-Encoding: gzip, deflate
            //          Accept-Language: en-US,en;q=0.8
            //          
            //body-->   Name=Dan&Count=544&IsActive=true
            //(Url-Encoded, which is worse)
        }
    }

    public class HeaderSampleController : ApiController
    {
        // GET /api/HeaderSample    ...by passing complex object through a header instead of through the URL of a GET... for cases where the URL might exceed the length limit
        public ComplexObject Get()
        {

            //get JSON from header
            string[] headerJson = (string[])this.Request.Headers.GetValues("theObject"); // {"Name":"Joe","Count":456,"IsActive":false}

            //TODO: needs error handling and checks for null, etc

            // Convert json into Interface of IProfileDTO ( and IProfileElement )
            var dto = Newtonsoft.Json.JsonConvert.DeserializeObject<ComplexObject>(headerJson[0]);

            return ComplexObject.All.FirstOrDefault(
                o => o.Name == dto.Name
                && o.Count == dto.Count
                && o.IsActive == dto.IsActive);


            //this javascript
            //
            //          var complexObject = {
            //              Name: 'Bob' ,
            //              Count: 5 ,
            //              IsActive: true ,
            //              Children: [
            //                  { 'Name': 'Mary', 'Count': 4545, 'IsActive': true, 'Children': null },
            //                  { 'Name': 'Larry', 'Count': 327, 'IsActive': false, 'Children': null }]
            //          }
            //          
            //          $.ajax({
            //              type: 'Get' // always passes data in URL, except in this case where the data is too long (potentially) so we are stuffing it in a header, THOUGH NOT STANDARD.
            //              , url: '/api/HeaderSample'
            //              , headers: { 'theObject': JSON.stringify(complexObject) }
            //          })
            //          .done(function (returnData) {
            //              $('#HeaderSampleResult1').html(JSON.stringify(returnData));
            //          })
            //          .fail(function (xhr, result, status) {
            //              alert(JSON.stringify(xhr));
            //          });
            //
            //
            //produced this call, which hit this controller
            //
            //          GET http://localhost:49278/api/HeaderSample HTTP/1.1
            //          Host: localhost:49278
            //          Connection: keep-alive
            //          Accept: */*
            //          X-Requested-With: XMLHttpRequest
            //          User-Agent: Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36
            //data-->   theObject: {"Name":"Bob","Count":5,"IsActive":true,"Children":[{"Name":"Mary","Count":4545,"IsActive":true,"Children":null},{"Name":"Larry","Count":327,"IsActive":false,"Children":null}]}
            //          Referer: http://localhost:49278/ApiSample
            //          Accept-Encoding: gzip, deflate, sdch
            //          Accept-Language: en-US,en;q=0.8




        }

    }


    public class FormSampleController : ApiController
    {
        // POST /api/FormSample    ...using a html FORM as the contents of the POST, using complex type
        public ComplexObject Post(ComplexObject dto)
        {

            var result = new ComplexObject();
            result.Name = dto.Name;
            result.Count = dto.Count;
            result.IsActive = dto.IsActive;
            ComplexObject.All.Add(result);
            return result;

            //in html ...NOTE form items must have a NAME assigned that matches the comples object property names
            //          <form id="sampleForm">
            //              <input type="text" name="Name" value="Tom"/>    
            //              <input type="text" name="Count" value="3000" />
            //              <input type="hidden" name="IsActive" value="true"/>
            //              <input type="checkbox" name="ignored" checked="checked" /> <!-- does not result to 'true' in Web Api when checked -->
            //          </form>
            //
            //
            //this javascript 
            //
            //          var form = $('#sampleForm').serialize();
            //          
            //          // NOTE !!!!! passing child collections may not be supported.... Url-Encoded body does not serialize at the API level if child collections are present
            //          
            //          $.ajax({
            //              type: 'POST' // Pass via the body as URL encoded string
            //              , url: '/api/FormSample'
            //              , data: form // Note! expects [FromBody] in Api controller when using form
            //              //this is default... , contentType: 'application/x-www-form-urlencoded; charset=UTF-8'
            //          })
            //          .done(function (returnData) {
            //              $('#FormSampleResult1').html(JSON.stringify(returnData));
            //          })
            //          .fail(function (xhr, result, status) {
            //              alert(JSON.stringify(xhr));
            //          });
            //
            //produced this call, which hit this controller
            //
            //          POST http://localhost:49278/api/FormSample HTTP/1.1
            //          Host: localhost:49278
            //          Connection: keep-alive
            //          Content-Length: 44
            //          Accept: */*
            //          Origin: http://localhost:49278
            //          X-Requested-With: XMLHttpRequest
            //          User-Agent: Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36
            //          Content-Type: application/x-www-form-urlencoded; charset=UTF-8
            //          Referer: http://localhost:49278/ApiSample
            //          Accept-Encoding: gzip, deflate
            //          Accept-Language: en-US,en;q=0.8
            //          
            //body-->   Name=Tom&Count=3000&IsActive=true&ignored=on
            //(Url-Encoded, which is worse)
            //

        }

    }

    public class ComplexObject
    {
        // STATIC data members and methods

        private static List<ComplexObject> _all = null; // this is a singleton code pattern ...used for ensuring non-null items which only initiate once for the lifespan of the app... private property with public accessor that creates, both static
        public static List<ComplexObject> All
        {
            get
            {
                if (_all == null)
                {
                    PopulateSamples();
                }

                return _all;
            }
        }

        private static void PopulateSamples()
        {
            _all = new List<ComplexObject>();

            ComplexObject sample = new ComplexObject();
            sample.Name = "Bob";
            sample.Count = 5;
            sample.IsActive = true;
            _all.Add(sample);

            ComplexObject child1 = new ComplexObject();
            child1.Name = "Mary";
            child1.Count = 4545;
            child1.IsActive = true;
            _all.Add(child1);

            ComplexObject child2 = new ComplexObject();
            child2.Name = "Larry";
            child2.Count = 327;
            child2.IsActive = false;
            _all.Add(child2);

            List<ComplexObject> children = new List<ComplexObject>();
            children.Add(child1);
            children.Add(child2);
            sample.Children = children;
        }


        // INSTANCE data members and methods

        public string Name { get; set; }
        public int Count { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<ComplexObject> Children { get; set; }

    }

}



/*
@{
    ViewBag.Title = "Api Sample calls";
}

<h2>Api Sample calls</h2>

<style>
    p {
        border-top: 1px solid green;
        padding-bottom: 20px;
    }
</style>

<p>
    BestPracticeSample Get()
    <input type="button" value="test" onclick="
        $.ajax({
            type: 'GET' // always passes data in URL
            , url: '/api/BestPracticeSample'
        })
            .done(function (returnData) {
                $('#BestPracticeSampleResult1').html(JSON.stringify(returnData));
            })
            .fail(function (xhr, result, status) {
                alert(JSON.stringify(xhr));;
            });
    " />
    <span id="BestPracticeSampleResult1"></span>
</p>

<p>
    BestPracticeSample Get(string id)
    <input type="button" value="test" onclick="
        var right_Url_1 = '/api/BestPracticeSample?id=Bob'                                          //<-- preferred because it's both simple and intuitive
        var wrong_Url_2 = '/api/BestPracticeSample' //AND PASS DATA IN AJAX: , data: {id: 'Bob'}    //<-- not preferred, it is not simple. It is more explicit, but not any more explicit than #1
        var wrong_Url_3 = '/api/BestPracticeSample/Bob'                                             //<-- not preferred UNLESS explicitly naming route [Route('api/ComplexObject/{id}')]. ...Though it is simple, it is not intuitive without knowing the convention. 
                                                                                                    //    This can cause confusion for other developers. Avoid This Alternate. Although this works fine, the automatic mapping of an id to the Url string 
                                                                                                    //    introduces a one-off way of building the url only when the parameter is called 'id'.... for consistency, I would prefer that all URLs are built using 
                                                                                                    //    the same convention so that the name of the parameter doesn't alter the process. There may be exceptions for certain SEO scenarios. 

        $.ajax({
            type: 'GET' // always passes data in URL
            , url: '/api/BestPracticeSample?id=Bob'
        })
            .done(function (returnData) {
                $('#BestPracticeSampleResult2').html(JSON.stringify(returnData));
            })
            .fail(function (xhr, result, status) {
                alert(JSON.stringify(xhr));
            });
    " />
    <span id="BestPracticeSampleResult2"></span>
</p>

<p>
    BestPracticeSample Get(string name, int count)
    <input type="button" value="test" onclick="
        var theData = {
            name: 'Bob'
            , count: 5
        }

        $.ajax({
            type: 'GET' // always passes data in URL
            , url: '/api/BestPracticeSample'
            , data: theData // because this is a GET, this data is moved to the URL even though this looks as if it will go in a body
        })
            .done(function (returnData) {
                $('#BestPracticeSampleResult3').html(JSON.stringify(returnData));
            })
            .fail(function (xhr, result, status) {
                alert(JSON.stringify(xhr));
            });
    " />
    <span id="BestPracticeSampleResult3"></span>
    <br />
    <small> (may return null if you already tested the BestPracticeSample Put to update the test data 'Count')</small>
</p>

<p>
    BestPracticeSample Post(ComplexObject dto)
    <input type="button" value="test" onclick="
        var complexObject = { //< -- create new object
            Name: 'Fred',
            Count: 1122,
            IsActive: false,
            Children: [{ Name: 'Freddy Jr', Count: 1, IsActive: true, Children: null }]
        }

        $.ajax({
            type: 'POST' // For POST or PUT, use JSON instead of the default Url-Encoded body
            , url: '/api/BestPracticeSample'
            , data: JSON.stringify(complexObject) //           <--  1 of 2 ...force JSON format
            , contentType: 'application/json;charset=utf-8' // <--  2 of 2 ...force JSON header
        })
              .done(function (returnData) {
                  $('#BestPracticeSampleResult4').html(JSON.stringify(returnData));
              })
              .fail(function (xhr, result, status) {
                  alert(JSON.stringify(xhr));
              });
    " />
    <span id="BestPracticeSampleResult4"></span>
</p>

<p>
    BestPracticeSample Put(string id, ComplexObject dto)
    <input type="button" value="test" onclick="
        var complexObject = {
            Name: 'Bob',
            Count: 55155, //< -- updated value
            IsActive: true,
            Children: [
                { 'Name': 'Mary', 'Count': 4545, 'IsActive': true, 'Children': null },
                { 'Name': 'Larry', 'Count': 327, 'IsActive': false, 'Children': null }]
        }

        // for PUT, the URL needs to identify the item to update, and then pass the new item via the body
        var identifier = 'Bob'
        var myUrl = '/api/BestPracticeSample?id=' + identifier; // expected to create: /api/BestPracticeSample?id=Bob      // WCN Specific fix for Netscaler Url rewrite issue 'BuildRelativeUrl'

        $.ajax({
            type: 'PUT' // WEB API CAN ACCEPT primitive Types via the URL even for POSTs
                        // For POST or PUT, use JSON instead of the default Url-Encoded body
            , url:  myUrl // constructed above
            , data: JSON.stringify(complexObject) //           <--  1 of 2 ...force JSON format
            , contentType: 'application/json;charset=utf-8' // <--  2 of 2 ...force JSON header
        })
            .done(function (returnData) {
                $('#BestPracticeSampleResult5').html(JSON.stringify(returnData));
            })
            .fail(function (xhr, result, status) {
                alert(JSON.stringify(xhr));
            });
    " />
    <span id="BestPracticeSampleResult5"></span>
</p>

<p>
    AlternateSample GetWithNamedParameter(string myVariableName)
    <input type="button" value="test" onclick="
        var right_Url_1 = '/api/AlternateSample?myVariableName=Bob'       //<-- preferred because it's both simple and intuitive
        var wrong_Url_2 = '/api/AlternateSample/Bob'                      //<-- avoid this UNLESS explicitly naming route [Route('api/ComplexObject/{myVariableName}')]. Do NOT use implicit auto-mapping to the 'Id' keyword variable name

        $.ajax({
            type: 'GET' // always passes data in URL
            , url: '/api/AlternateSample?myVariableName=Bob'
        })
            .done(function (returnData) {
                $('#AlternateSampleResult1').html(JSON.stringify(returnData));
            })
            .fail(function (xhr, result, status) {
                alert(JSON.stringify(xhr));
            });
    " />
    <span id="AlternateSampleResult1"></span>
</p>

<p>
    AlternateSample Post([FromBody] string username) (passing as JSON string ...best practice)
    <input type="button" value="test" onclick="

        var theData = 'Jim'; // use simple javascript string, and pass as JSON

        $.ajax({
            type: 'POST' // For POST or PUT, use JSON instead of the default Url-Encoded body
            , url: '/api/AlternateSample'
            , data: JSON.stringify(theData) //                 <--  1 of 2 ...force JSON format
            , contentType: 'application/json;charset=utf-8' // <--  2 of 2 ...force JSON header
        })
            .done(function (returnData) {
                $('#AlternateSampleResult3').html(JSON.stringify(returnData));
            })
            .fail(function (xhr, result, status) {
                alert(JSON.stringify(xhr));
            });
    " />
    <br />
    AlternateSample Post([FromBody] string username) (passing as URL-ENCODED string ...NOT preferred)
    <input type="button" value="test" onclick="

        // send oddly formatted object to resolve to Microsoft's handling of single primitive strings in the Url-Encoded body
        var theData = {
            // NOTE order of params must match WebApi...        public bool GetIsUsernameAndEmailAvailable(string PrimaryEmail)
            // NOTE sending single primitive types (not complex object), the JSON object should have empty leading identifier...
            //This will work { '' : ''myString'' } results in body of ' =myString '                            ...Binds to     Post([FromBody]string stringName)
            //This WONT work { stringName : 'myString' }   results in body of   ' stringName=myString '      ...This will NOT bind in ASP.netWebAPI because of how it decifer between Model binding and Formatters binding
            '': 'Joe' //single string
        }

        $.ajax({
            type: 'POST' // NOT PREFERRED... This will Pass via the body as URL encoded string
            , url: '/api/AlternateSample'
            , data: theData //pass the JS object
        })
            .done(function (returnData) {
                $('#AlternateSampleResult3').html(JSON.stringify(returnData));
            })
            .fail(function (xhr, result, status) {
                alert(JSON.stringify(xhr));
            });
    " />
    <br />
    <span id="AlternateSampleResult3"></span>
</p>

<p>
    AlternateSample Get([FromUri]ComplexObject dto)
    <input type="button" value="test" onclick="
        var complexObject = {
            Name: 'Bob'
            , Count: 5
            , IsActive: true
        }

        $.ajax({
            type: 'GET' // always passes data in URL
            , url: '/api/AlternateSample'
            , data: complexObject // because this is a GET, this data is moved to the URL even though this looks as if it will go in a body
        })
            .done(function (returnData) {
                $('#AlternateSampleResult2').html(JSON.stringify(returnData));
            })
            .fail(function (xhr, result, status) {
                alert(JSON.stringify(xhr));
            });
    " />
    <span id="AlternateSampleResult2"></span>
    <br />
    <small> (may return null if you already tested the BestPracticeSample Put to update the test data 'Count')</small>
</p>

<p>
    AlternateSample PostFromUrl(string name, int count, bool isActive)
    <input type="button" value="test" onclick="
        var complexObject = {
            Name: 'Mike' ,
            Count: 989 ,
            IsActive: false ,
            // !!!!!!!!!!!! ALERT !!!!!!!!!!!!!!!!!!
            // !!!!!!!!!!!! ALERT !!!!!!!!!!!!!!!!!!
            // !! posting as a Url-Encoded body BREAKS when there are child collections !!
            // will be null -->    Children: [
            // will be null -->        { 'Name': 'Mary', 'Count': 4545, 'IsActive': true, 'Children': null },
            // will be null -->        { 'Name': 'Larry', 'Count': 327, 'IsActive': false, 'Children': null }]
        }


        $.ajax({
            type: 'POST' // WEB API CAN ACCEPT primitive Types via the URL even for POSTs
            , url: '/api/AlternateSample?' + $.param(complexObject) // NOT RECOMMENDED, but possible...  Add the object to the URL, instead of the body// only do this when needing to support links or bookmarks that will POST
            //, data: theData // DO NOT PASS DATA HERE (body)
        })
            .done(function (returnData) {
                $('#AlternateSampleResult4').html(JSON.stringify(returnData));
            })
            .fail(function (xhr, result, status) {
                alert(JSON.stringify(xhr));
            });
    " />
    <span id="AlternateSampleResult4"></span>
    <br />
    Not Recommended unless posting from a URL only
</p>

<p>
    RestfulUrlSample Get(string name) .. uses a RESTful route: "api/RestfulUrlSample/{name}/Children"
    <input type="button" value="test" onclick="
            var name = 'bob';
            var apiUrl = 'api/RestfulUrlSample/' + name + '/Children'; // produces 'api/RestfulUrlSample/bob/Children'

            $.ajax({
                type: 'GET' // always passes data in URL
                , url: apiUrl
            })
                .done(function (returnData) {
                    $('#RestfulUrlSampleResult1').html(JSON.stringify(returnData));
                })
                .fail(function (xhr, result, status) {
                    alert(JSON.stringify(xhr));
                });
    " />
    <span id="RestfulUrlSampleResult1"></span>
</p>

<p>
    DefaultUrlEncodedBodySample Post(ComplexObject dto)
    <input type="button" value="test" onclick="
        var complexObject = {
            Name: 'Dan',
            Count: 544,
            IsActive: true,
            // !!!!!!!!!!!! ALERT !!!!!!!!!!!!!!!!!!
            // !!!!!!!!!!!! ALERT !!!!!!!!!!!!!!!!!!
            // !! posting as a Url-Encoded body BREAKS when there are child collections !!
            // will break -->    Children: [
            // will break -->        { 'Name': 'Mary', 'Count': 4545, 'IsActive': true, 'Children': null },
            // will break -->        { 'Name': 'Larry', 'Count': 327, 'IsActive': false, 'Children': null }]
        }

          $.ajax({
              type: 'POST' // Pass via the body as URL encoded string
              , url: '/api/DefaultUrlEncodedBodySample'
              , data: complexObject
        })
            .done(function (returnData) {
                $('#DefaultUrlEncodedBodySampleResult2').html(JSON.stringify(returnData));
            })
            .fail(function (xhr, result, status) {
                alert(JSON.stringify(xhr));
            });
    " />
    <span id="DefaultUrlEncodedBodySampleResult2"></span>
</p>

<p>
    HeaderSample Get()
    <input type="button" value="test" onclick="
            var complexObject = {
                Name: 'Bob' ,
                Count: 5 ,
                IsActive: true ,
                Children: [
                    { 'Name': 'Mary', 'Count': 4545, 'IsActive': true, 'Children': null },
                    { 'Name': 'Larry', 'Count': 327, 'IsActive': false, 'Children': null }]
            }

            $.ajax({
                type: 'Get' // always passes data in URL, except in this case where the data is too long (potentially) so we are stuffing it in a header, THOUGH NOT STANDARD.
                , url: '/api/HeaderSample'
                , headers: { 'theObject': JSON.stringify(complexObject) }
            })
            .done(function (returnData) {
                $('#HeaderSampleResult1').html(JSON.stringify(returnData));
            })
            .fail(function (xhr, result, status) {
                alert(JSON.stringify(xhr));
            });
    " />
    <span id="HeaderSampleResult1"></span>
    <br />
    Not Recommended unless URL is too large for a standard URL, and a POST is also not an option
</p>

<p>
    <form id="sampleForm">
        FormSample Post(ComplexObject dto)
        <input type="text" name="Name" value="Tom" />
        <input type="text" name="Count" value="3000" />
        <input type="hidden" name="IsActive" value="true" />
        <input type="checkbox" name="ignored" checked="checked" /> <!-- does not result to 'true' in Web Api when checked -->
    </form>
    <input type="button" value="test" onclick="
            var form = $('#sampleForm').serialize();

            // NOTE !!!!! passing child collections may not be supported.... Url-Encoded body does not serialize at the API level if child collections are present

            $.ajax({
                type: 'POST' // Pass via the body as URL encoded string
                , url: '/api/FormSample'
                , data: form // Note! expects [FromBody] in Api controller when using form
                //this is default... , contentType: 'application/x-www-form-urlencoded; charset=UTF-8'
            })
            .done(function (returnData) {
                $('#FormSampleResult1').html(JSON.stringify(returnData));
            })
            .fail(function (xhr, result, status) {
                alert(JSON.stringify(xhr));
            });
    " />
    <span id="FormSampleResult1"></span>
</p>




*/