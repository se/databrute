# databrute
Create simple random generated data and send it as request

## How to use?

I'm using yarn (or npm) for scripting. If you don't have it, please install it first;

```sh
## If you don't have yarn
npm install -g yarn
```

### For building project;

```sh
yarn b
```

### For running it;

```sh
yarn go --count=100 --url=http://localhost:4020/people --formatted
```

### Explanation;

`--count` is your data count that you want to generate
`--formatted` will write it your console as formatted.
`--url` if you put this, it will post it to that url

### You will get random;

```json
{
  "userId": "12460bc6-d792-4a2e-a5b7-7d99147579ec",
  "isActive": false,
  "firstName": "Willis",
  "lastName": "Ortiz",
  "avatarUrl": "https://s3.amazonaws.com/uifaces/faces/twitter/benoitboucart/128.jpg",
  "age": 20,
  "hairColor": "black",
  "name": {
    "first": "Willis",
    "last": "Ortiz"
  },
  "userName": "Willis11",
  "company": "Nolan - Schoen",
  "email": "Willis.Ortiz1@gmail.com",
  "phone": "423-649-6846 x474",
  "address": "04591 Herzog Court, Dallasmouth, Bolivia",
  "gender": 0,
  "fullName": "Willis Ortiz",
  "id": 11958
}
```

### Adding as Global Command with dotnet-tool
```sh
dotnet tool install -g dotnet-databrute
```

And now, you can use all commands just typing `databrute --count=100 --url=http://localhost:4020/people --formatted`.

This project is using [Bogus](https://github.com/bchavez/Bogus) and [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json).

Feel free to contribute 👍