# Week 4: Web Service Authentication and Storage

Two projects work together: this ASP.NET Core REST API and the sibling
`../Web_Serice_Authentication_and_Storage_App` .NET MAUI app.
The displayed student name is **Richard Burns**. The assignment login is
**Burns01** / **Password1**, with exact, case-sensitive matching on the server.

## Run

1. In this API directory, run `dotnet run --launch-profile http`.
2. Open the companion MAUI project in your IDE and run its Debug configuration
   on Mac Catalyst, an iOS simulator, or an Android emulator.
3. Log in, fill all three item fields, and select Save. Stored items appear below
   the form. Scroll the page to view the list; Refresh reloads it from the API.

The API listens on `http://localhost:5027`. `MauiProgram.cs` configures localhost
for Apple simulators/Mac/Windows and `10.0.2.2` for the Android emulator. A physical
phone requires a reachable server URL in that file; localhost on a phone is the
phone itself. Local HTTP support is enabled for Android Debug and Apple local
networking. Basic authentication must use HTTPS outside local development;
configure a trusted HTTPS server URL before deployment.

## Swagger UI

With the API running in Development, open http://localhost:5027/swagger.
Expand an endpoint and select **Try it out**, then **Execute**. For login, select
**Authorize** and enter `Burns01` / `Password1` first. Item endpoints do not need
authorization. The OpenAPI document is at `/openapi/v1.json`.

## Structure and behavior

- API: `Authentication/BasicAuthenticationHandler.cs` authenticates the login
  endpoint using the Basic Authorization header and returns 401 with a Basic
  challenge for invalid or missing credentials.
- `GET /api/auth/login` verifies credentials. `GET /api/items` lists items and
  `POST /api/items` inserts one. Item endpoints allow anonymous access as required.
- `DataAccess/ItemRepository.cs` stores items in SQLite at `App_Data/items.db3`.
  It creates the directory and table automatically; records survive API restarts.
  Set the `DatabasePath` configuration value to override the path.
- Item IDs are strings and must be unique. Missing/blank fields produce 400;
  duplicate IDs produce 409 without replacing an existing item.
- MAUI `ViewModels` own field state, validation, messages, and commands. Views use
  compiled bindings; code-behind only sets binding contexts and loads data when
  the data entry page appears. `Services/NavigationService.cs` handles navigation.
- MAUI `DataAccess/ApiService.cs` handles asynchronous HTTP authentication, saves,
  and retrieval through `IApiService`. No database or password validation lives in
  the mobile app. Credentials are attached only to the login request.
- Cancel clears both login fields and messages. Controls are disabled during
  requests to prevent overlapping operations. Successful login clears the
  password and opens the data entry page. Save validates all fields, posts the
  item, then retrieves the entire list. If refresh fails after a successful save,
  the message distinguishes that from a failed save.

## Verification

Run `dotnet build`, then `python3 tests/api_smoke.py`. The smoke test starts its
own API instance with a temporary SQLite database and checks valid/invalid and
malformed Basic authentication, anonymous item access, required fields,
duplicate IDs, retrieval, and persistence across a server restart.

`Web_Service_Authentication_and_Storage.http` also contains manual API requests.
For UI acceptance, check invalid login, Cancel, valid login, each missing item
field, successful Save, duplicate ID, Refresh, and stored items after restarting
the API. Stop the API to check connection failure messages.

Run `dotnet run --project tests/ViewModels/Tests.csproj` to test the real view-model
logic with fake HTTP/navigation services and a minimal command adapter, without
a device runtime. This covers login/Cancel, navigation, connection failures,
required fields, save/reload, duplicate handling, and recovery after refresh fails.
