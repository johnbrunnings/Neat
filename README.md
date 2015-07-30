# Neat

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

1. Make changes to the Neat.Model.NeatExample class. (Don't forget to rebuild!)  For example:

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

It's ok we've got nothing there yet...

You should get back a 204 No Content, that's good! The record saved (if it had not, you would have gotten an error).

Go query again:  /neat
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

2. Add a new Model class and Controller class, reference the NeatExample and NeatController.
