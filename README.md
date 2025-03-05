# MiddlewareSandbox

## Overview
MiddlewareSandbox is an ASP.NET WebAPI project that demonstrates various middleware implementations, including request counting, query string processing, logging, and API key validation.

## Implemented Middleware

### 1. Request Counter Middleware
- Tracks the number of processed requests.
- If the request path is `/count`, it returns the total request count.

### 2. Custom Query Middleware
- Checks if the query string contains a `custom` parameter.
- If present, returns: `You've hit a custom middleware!`.
- Otherwise, the request proceeds as usual.

### 3. Request Logger Middleware
- Logs each incoming request to the console.
- Displays the HTTP method (GET, POST, etc.) and request path.

### 4. API Key Validation Middleware
- Checks for an `X-API-KEY` header in the request.
- If missing or incorrect, returns `403 Forbidden`.
- Only requests with the correct API key proceed further.

## Installation & Setup

### **1. Clone the repository**
```sh
git clone https://github.com/ipsolver/ASP_dev.git
cd pr1/MiddlewareSandbox
```

### **2. Build the project**
```sh
dotnet build
```

### **3. Run the project**
```sh
dotnet run
```

The API will start on `https://localhost:7140/` (or another available port).

## Testing the Middleware

### **1. Request Counter Middleware**
```sh
curl -X GET https://localhost:7140/count
```
Response:
```
The amount of processed requests is X
```

### **2. Custom Query Middleware**
```sh
curl -X GET https://localhost:7140/some-endpoint?custom=true
```
Response:
```
You've hit a custom middleware!
```

### **3. Request Logger Middleware**
- Check the terminal/console output to see logged requests.

### **4. API Key Validation Middleware**
#### Correct API Key:
```sh
curl -X GET https://localhost:7140/who -H "X-API-KEY: my-secret-api-key"
```
#### Missing or Incorrect API Key:
```sh
curl -X GET https://localhost:7140/who
```
Response:
```
Forbidden: Invalid or missing API key.
```

