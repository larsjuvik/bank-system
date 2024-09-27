# Security :closed_lock_with_key:

## Disclaimer

**This project is only a demo and is not intended for production use**.

It may contain security vulnerabilities and should not be deployed in any sensitive or production environments.

## Security concerns

Below follows some points that are relevant when considering this applications' security:

* For demo purposes, any new account have the option to be created with admin privileges
* If you create a user and then restart the application, you may need to remove the authentication cookie
    * The user still has an authentication cookie, but all database-data for this user is lost on restart, which can cause errors
* The setting `options.Cookie.SecurePolicy` for the cookies could be more strict
  * Now it allows `http` and `https`, but the best would be to only allow `https`
