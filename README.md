# Clean architecture (Onion architecture)

![image](https://user-images.githubusercontent.com/15936780/145341846-6e486db8-cc89-48c7-b0b8-e76245e9a90c.png)

## Domain
- Entities
- Value Objects
- Enumerations
- Enterprise Logic
- Exception

## Applications
- Interfaces
- Models (DTO, ViewModels and etc)
- Application Logic
- Commands
- Queries
- Validators
- Exceptions

### CQRS
- Command Query Responsibility Segregation
- Separate reads (queries) and writes (commands)
- Can maximise performance, scalability, and simplicity
- Easy to add new features, just add a new query or command
- Easy to maintain, changes only affect one command or query

### CQRS + MediatR
- Define commands and queries as requests
- Application layer is just a series of requests/response objects
- Ability to attach additional behaviour before and/or after each request. Example: logging, validation, caching, and etc.

### Key points
- Using CQRS + MediatR simplifies your overall design
- MediatR simplifies cross cutting concerns
- Fluent Validation is useful for all validation scenarios
- AutoMapper simplifies mapping and projections
- Independent of infrastructure and data access concerns.

## Infrastructure
- Persistence
- Identity
- File System
- API Clients
- Blob storage


