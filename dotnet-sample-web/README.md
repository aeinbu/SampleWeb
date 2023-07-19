## How to create bearer tokens to test the secret and secret-admin

```console
dotnet user-jwts create --name steve
dotnet user-jwts create --name adminsteve --scope secrets-api --role admin
```

When you run each of these commands, you will be presented with some info about the token created. Copy the string after `Token:` and use it in the requests-header:
```
Authorization: (Bearer) paste-token-here
```
(Hint: Replace `paste-token-here` with the copied token)

### Postman
In Postman you can add the token on the authorization page. Select "Bearer" and paste the token in there.

### curl
You can use the tokens with `curl`, like
```console
curl -i -H "Authorization: Bearer paste-toke-here" https://localhost:7087/secret

curl -i -H "Authorization: Bearer paste-toke-here" https://localhost:7087/secret-admin
```
(Hint: Replace `paste-token-here` with the copied token)
(Hint: Remeber to set the correct port numbers)

### Sources
https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/security?view=aspnetcore-7.0