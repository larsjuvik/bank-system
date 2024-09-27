# Security

Below follows some points that are relevant when considering this applications' security:

* This application will only function correctly over `https`. If this is not desirable, change the
  `options.Cookie.SecurePolicy` to your liking
* For demo purposes, any new account have the option to be created with admin privileges
* If you create a user and then restart the application, you may need to remove the authentication cookie
    * The user still has an authentication cookie, but all database-data for this user is lost on restart, which can cause errors
* The setting `options.Cookie.SecurePolicy` for the cookies could be more strict
  * Now it allows `http` and `https`, but the best would be to only allow `https`