Identity.Api is a full OpenID connect provider using OpenIddict
centralized identity and authorization backend designed to serve multiple applications. It exposes endpoints for:
- Application registration and management
- User management within each application
- Role assignment per application
- Authentication and token issuance (currently custom, not OpenIddict)
- Administrative dashboards (Apps, Users, Roles)
It was built to support a multi‑tenant, app‑centric identity model, where apps are first‑class citizens and users exist inside apps, not globally.

2. Domain Model (App‑Centric Architecture)
🏛 App
Represents a client application registered in the identity platform.
Fields
- Id (GUID)
- Name
- Description
- ClientId (string)
- ClientSecret (hashed)
- RedirectUris (list)
- PostLogoutRedirectUris (list)
- IsActive (bool)
- CreatedAt
Relationships
- One App → Many AppUsers
- One App → Many AppRoles

👤 AppUser
Represents a user inside a specific app.
Fields
- Id (GUID)
- AppId (FK)
- Email
- PasswordHash
- DisplayName
- IsActive
- CreatedAt
Relationships
- One AppUser → Many AppUserRoles

🎭 AppRole
Represents a role defined by an app.
Fields
- Id (GUID)
- AppId (FK)
- Name
- Description

🔗 AppUserRole
Join table linking AppUsers to AppRoles.
Fields
- AppUserId
- AppRoleId

3. Authentication Model (Pre‑OpenIddict)
Your current system uses a custom token issuance flow:
- /auth/login
- Validates user credentials
- Issues a JWT with:
- sub = AppUserId
- app = AppId
- roles = list of role names
- exp
- /auth/refresh
- Issues new access tokens using a refresh token
This flow works but is not compatible with WASM because:
- WASM cannot safely store secrets
- WASM cannot perform confidential client flows
- Your current system does not support PKCE
- Your current system does not support OAuth2/OIDC standards
This is why we’re moving to OpenIddict.

4. API Endpoints (Current)
🔐 AuthController
- POST /auth/login
- POST /auth/refresh
- POST /auth/logout
🏛 AppsController
- GET /apps
- GET /apps/{id}
- POST /apps
- PUT /apps/{id}
- DELETE /apps/{id}
👤 AppUsersController
- GET /apps/{appId}/users
- GET /apps/{appId}/users/{id}
- POST /apps/{appId}/users
- PUT /apps/{appId}/users/{id}
- DELETE /apps/{appId}/users/{id}
🎭 AppRolesController
- GET /apps/{appId}/roles
- POST /apps/{appId}/roles
- DELETE /apps/{appId}/roles/{id}
🔗 AppUserRolesController
- POST /apps/{appId}/users/{userId}/roles/{roleId}
- DELETE /apps/{appId}/users/{userId}/roles/{roleId}

5. Error Handling
You implemented a unified error response model:
{
  "error": "ValidationError",
  "message": "Email already exists",
  "details": { ... }
}


And a Blazor toast pipeline that normalizes all errors.

6. Client Integration (Blazor Admin Panel)
Your Admin PWA uses:
- A centralized ApiClient wrapper
- Automatic token refresh
- Global toast error handling
- MudBlazor UI
- Dialogs for:
- App creation/edit
- User creation/edit
- Role assignment

7. What’s Missing for WASM Compatibility
Your current Identity API cannot support WASM securely because:
- It requires a client secret
- It does not support PKCE
- It does not implement OAuth2/OIDC authorization code flow
- It does not expose .well-known/openid-configuration
- It does not issue OIDC‑compliant tokens
- It does not support OpenIddict’s application registration model
- It does not support external login providers (optional)
This is exactly why we’re going to rebuild a minimal OpenIddict server and then layer your app‑centric model on top.

🎯 You Now Have a Clean, Canonical Description of Your Identity API
This is the reference we’ll use when:
- Building the minimal OpenIddict server
- Verifying WASM authentication works
- Reintroducing:
- Apps
- AppUsers
- AppRoles
- AppUserRoles
- Integrating your Admin PWA
- Migrating your existing domain model into OpenIddict’s application/user/role abstractions
