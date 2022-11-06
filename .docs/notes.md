# Azure AD

[OpenID Connect Aspnet](https://github.com/Azure-Samples/active-directory-aspnetcore-webapp-openidconnect-v2/blob/master/2-WebApp-graph-user/2-1-Call-MSGraph/README.md)

# Graph API

https://learn.microsoft.com/en-us/graph/api/overview?view=graph-rest-1.0
https://developer.microsoft.com/en-us/graph/graph-explorer

## OneDrive

https://learn.microsoft.com/en-us/training/modules/msgraph-dotnet-core-manage-files/8-exercise-upload-user-files
https://learn.microsoft.com/en-us/samples/microsoftgraph/aspnetcore-webhooks-sample/microsoft-graph-change-notifications-sample-for-aspnet-core/


```
ngrok http --region=eu --hostname=tagit.eu.ngrok.io https://localhost:5001
```

### Add Azure AD Client

```graphql
mutation addCreds($input: AddOAuthCredentialClientInput! ){
  addOAuthCredentialClient(input: $input) {
    credential {
      id
      name
    }
  }
}

{
  "input": {
    "name": "Azure",
    "client": {
      "flow": "CODE",
      "id": "xxx",
      "secret": {
        "value": "xxx"
      },
      "scopes": [
        "openid",
        "profile",
        "offline_access",
        "user.read",
        "mail.readwrite",
        "files.readwrite.all"
      ],
      "authority": "https://login.microsoftonline.com/common/v2.0/",
      "product": "AzureAD"
    }
  }
}
```
