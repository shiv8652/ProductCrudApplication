# ProductCrudApplication 
  # An Application built with Dot NET Core supporting full CRUD operations for managing Products and Categories. The project uses a relational database (MySQL), and exposes well-documented endpoints via Swagger.
# Folder Structure
- Properties
  - launchSettings.json
- Controllers
  - CategoryController.cs
  - ProductController.cs
- Data
  - AppDbContext.cs
- DTO
  - ProductDTO.cs
  - ProductListDTOClass.cs
- Migrations
  - 20250602200614_INIT.cs
  - AppDbContextModelSnapshot.cs
- Model
  - Category.cs
  - Product.cs
- Repository
  - CategoryRepository.cs
  - ICategoryRepository.cs
  - IProductRepository.cs
  - ProductRepository.cs
- Service
  - CategoryServiceImpl.cs
  - ICategoryService.cs
  - IProductService.cs
  - ProductServiceImpl.cs
- gitattributes
- .gitignore
- appsettings.json
- Program.cs
- README.md

# Project Setup & Execution Instructions
# Requirements
1) .NET SDK (8)
2) MySQL Server
3) Visual Studio or Visual Studio Code

#  Setup Process
# Installation
1) Clone the Repository from GitHub
   1) git clone https://github.com/shiv8652/ProductCrudApplication.git
# Configuration
1) Configure Your Database Connection
   1) Locate the ConnectionStrings section in appsettings.json file.
   2) Update the value with your actual database connection string.
2) Apply Database Migrations
3) Run the Application




