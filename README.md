# Neat

Badges:
[![Build status](https://ci.appveyor.com/api/projects/status/sd5xwhs08fnj3by0?svg=true)](https://ci.appveyor.com/project/mbonig/neat)

1. Pull this repo
2. Build in VS2013+ (report any build errors as an Issue)
3. Host the Neat.Web.Api project
4. Create new 'neat' database on your local MongoDB instance


test: 
```
http://localhost/neat
```
(replace 'localhost' as needed)

This should return an empty array:

```
[]
```

If this is what you get, congrats! You're all setup. If not, report an issue.

Next steps:

* Make changes to the Neat.Model.NeatExample class. (Don't forget to rebuild!)  For example:

```
public class NeatExample : BaseModel
{
	public string Name { get; set; }
	public DateTime CreateDate { get; set; }
	public int SomeNumber { get; set; }
	public string ETag { get; set; }
	public MoreNeatStuff More { get; set; }
}
```
Use your favorite REST client to POST to /neat:
```
{

}
```

(There are <a href="https://chrome.google.com/webstore/detail/postman/fhbjgbiflinjbdggehcddcbncdddomop?utm_source=chrome-ntp-icon" target="_blank">POSTMAN<a> collections stored in the Neat.Test.Postman directory.)

It's ok we've got nothing there yet...

You should get back a 204 No Content, that's good! The record saved (if it had not, you would have gotten an error).

Go query again:  http://localhost/neat
```
[
  {
    "name": null,
    "createDate": "0001-01-01T00:00:00Z",
    "someNumber": 0,
    "eTag": null,
    "more": null,
    "id": "a301f1eb-25cc-432e-9be7-1c06bf2368ef"
  }
]
```


* Add a new Model class and Controller class, reference the NeatExample and NeatController. Hit those with similar API calls. 
Try creating a Customer Model and Controller then hitting http://localhost/customer.
