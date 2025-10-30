# FxConverter - Full-Stack Currency Conversion Application

A modern, full-stack currency conversion application built with .NET Core Web API and Angular, featuring real-time exchange rates and conversion history tracking.

## 🚀 Features

### Backend (.NET Core Web API)
- **Clean Architecture** implementation with separation of concerns
- **RESTful API** with comprehensive endpoints
- **Entity Framework Core** with SQL Server database
- **Open Exchange Rates API** integration for real-time rates
- **AutoMapper** for DTO mapping
- **Repository and Service patterns** for maintainable code
- **Comprehensive unit testing** with xUnit and Moq
- **Swagger/OpenAPI** documentation
- **CORS** support for frontend integration

### Frontend (Angular)
- **Angular 20** with standalone components
- **Angular Material** for modern UI components
- **Responsive design** with mobile-friendly layout
- **Real-time form validation**
- **Currency conversion** with live exchange rates
- **Conversion history** with pagination and sorting
- **Error handling** with user-friendly messages
- **Loading states** and progress indicators

## 🏗️ Architecture

### Backend Architecture
```
FxConverter.API/          # Web API layer
├── Controllers/          # API controllers
├── Program.cs            # Application configuration

FxConverter.Application/  # Application layer
├── DTOs/                 # Data transfer objects
├── Interfaces/           # Service contracts
├── Services/             # Business logic
└── Mappings/             # AutoMapper profiles

FxConverter.Infrastructure/ # Infrastructure layer
├── Data/                 # Entity Framework context
├── Repositories/         # Data access implementations
├── Services/             # External API services
└── DependencyInjection.cs

FxConverter.Domain/       # Domain layer
└── Entities/             # Domain models

FxConverter.Tests/        # Unit tests
├── Services/             # Service tests
└── Repositories/         # Repository tests
```

### Frontend Architecture
```
src/app/
├── components/           # Angular components
│   ├── currency-conversion/
│   └── conversion-history/
├── services/             # Angular services
├── models/               # TypeScript interfaces
└── environments/         # Environment configuration
```

## 🛠️ Technology Stack

### Backend
- **.NET 9.0** - Latest .NET framework
- **ASP.NET Core Web API** - RESTful API framework
- **Entity Framework Core 9.0** - ORM for database operations
- **SQL Server** - Database (LocalDB for development)
- **AutoMapper 12.0** - Object-to-object mapping
- **Refit 8.0** - HTTP client for external API calls
- **xUnit** - Unit testing framework
- **Moq** - Mocking framework
- **AutoFixture** - Test data generation
- **Swashbuckle.AspNetCore** - API documentation

### Frontend
- **Angular 20** - Frontend framework
- **Angular Material** - UI component library
- **TypeScript** - Type-safe JavaScript
- **RxJS** - Reactive programming
- **Angular Forms** - Form handling and validation

### External APIs
- **Open Exchange Rates API** - Real-time currency exchange rates
- **API Key**: 041f13216f7840fbbeb5d9828390ce51

## 📋 Prerequisites

- **.NET 9.0 SDK**
- **Node.js 18+** and npm
- **SQL Server** (LocalDB or full instance)
- **Visual Studio 2022** or **VS Code** (recommended)

## 🚀 Getting Started

### Backend Setup

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd FxConverter_App
   ```

2. **Navigate to backend directory**
   ```bash
   cd Backend
   ```

3. **Restore packages**
   ```bash
   dotnet restore
   ```

4. **Update connection string** (if needed)
   - Edit `Backend/FxConverter.API/appsettings.json`
   - Update the `DefaultConnection` string for your SQL Server instance

5. **Run the application**
   ```bash
   dotnet run --project FxConverter.API
   ```

6. **Access the API**
   - API: `https://localhost:7000`
   - Swagger UI: `https://localhost:7000/swagger`

### Frontend Setup

1. **Navigate to frontend directory**
   ```bash
   cd Frontend/fx-converter
   ```

2. **Install dependencies**
   ```bash
   npm install
   ```

3. **Update API URL** (if needed)
   - Edit `src/environments/environment.ts`
   - Update the `apiUrl` to match your backend URL

4. **Run the development server**
   ```bash
   ng serve
   ```

5. **Access the application**
   - Frontend: `http://localhost:4200`

## 🧪 Running Tests

### Backend Tests
```bash
cd Backend
dotnet test
```

### Frontend Tests
```bash
cd Frontend/fx-converter
ng test
```

## 📚 API Documentation

### Endpoints

#### Currency Conversion
- **POST** `/api/conversion` - Convert currency
- **GET** `/api/conversion/history?userId={userId}` - Get conversion history

#### Currency Management
- **GET** `/api/currency` - Get available currencies
- **GET** `/api/currency/rates/{base}` - Get exchange rates for base currency

### Example API Calls

#### Convert Currency
```bash
curl -X POST "https://localhost:7000/api/conversion" \
  -H "Content-Type: application/json" \
  -d '{
    "amount": 100,
    "fromCurrency": "USD",
    "toCurrency": "EUR"
  }'
```

#### Get Conversion History
```bash
curl -X GET "https://localhost:7000/api/conversion/history?userId=default-user"
```

## 🎯 Key Features Implementation

### Backend Features
- ✅ **Clean Architecture** with proper separation of concerns
- ✅ **Repository Pattern** for data access abstraction
- ✅ **Service Layer** for business logic
- ✅ **DTO Pattern** for data transfer
- ✅ **Dependency Injection** throughout the application
- ✅ **Entity Framework Core** with Code-First approach
- ✅ **External API Integration** with Refit
- ✅ **Caching** for exchange rates (30-minute cache)
- ✅ **Error Handling** with proper HTTP status codes
- ✅ **Unit Testing** with comprehensive coverage
- ✅ **Swagger Documentation** for API exploration

### Frontend Features
- ✅ **Angular Material** components for modern UI
- ✅ **Reactive Forms** with validation
- ✅ **Service Layer** for API communication
- ✅ **TypeScript Interfaces** for type safety
- ✅ **Error Handling** with user-friendly messages
- ✅ **Loading States** and progress indicators
- ✅ **Responsive Design** for mobile and desktop
- ✅ **Component Architecture** with standalone components

## 🔧 Configuration

### Backend Configuration
- **Database**: SQL Server LocalDB (configurable in appsettings.json)
- **CORS**: Configured for Angular app on localhost:4200
- **Caching**: 30-minute cache for exchange rates
- **API Key**: Open Exchange Rates API key configured

### Frontend Configuration
- **API URL**: Configurable in environment files
- **Material Theme**: Azure color palette
- **Responsive Breakpoints**: Mobile-first design

## 🚀 Deployment

### Backend Deployment
1. Update connection string for production database
2. Configure CORS for production frontend URL
3. Deploy to Azure App Service, AWS, or other cloud provider
4. Set up production database

### Frontend Deployment
1. Update API URL in production environment
2. Build for production: `ng build --configuration production`
3. Deploy to Azure Static Web Apps, Netlify, or other hosting service

## 📊 Performance Considerations

- **Caching**: Exchange rates cached for 30 minutes to reduce API calls
- **Database Indexing**: Proper indexes on frequently queried columns
- **Lazy Loading**: Angular components loaded on demand
- **Bundle Optimization**: Angular build optimizations enabled
- **HTTP Interceptors**: Centralized error handling and loading states

## 🔒 Security Considerations

- **Input Validation**: Comprehensive validation on all inputs
- **SQL Injection Prevention**: Entity Framework parameterized queries
- **CORS Configuration**: Restricted to specific origins
- **API Key Security**: External API key stored securely
- **Error Handling**: No sensitive information exposed in errors

## 🐛 Troubleshooting

### Common Issues

1. **Database Connection Issues**
   - Ensure SQL Server is running
   - Check connection string in appsettings.json
   - Verify LocalDB installation

2. **CORS Issues**
   - Check CORS configuration in Program.cs
   - Ensure frontend URL is allowed

3. **API Key Issues**
   - Verify Open Exchange Rates API key is valid
   - Check API rate limits

4. **Build Issues**
   - Ensure all dependencies are installed
   - Check .NET and Node.js versions

## 📈 Future Enhancements

- **Authentication & Authorization** with JWT tokens
- **User Management** with registration and login
- **Advanced Caching** with Redis
- **Rate Limiting** for API endpoints
- **Logging** with structured logging
- **Monitoring** with Application Insights
- **Docker** containerization
- **CI/CD** pipeline setup
- **Dark Mode** toggle
- **Export History** to CSV/PDF
- **Chart Visualization** for exchange rate trends

## 📝 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests for new functionality
5. Submit a pull request

## 📞 Support

For support and questions, please open an issue in the repository.

---

**Built with ❤️ using .NET Core and Angular**